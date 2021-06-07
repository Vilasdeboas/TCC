using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour {

    private BattleInfo battleInfo;
    private QuestionManager questionManager;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Unit playerUnit;
    public Unit enemyUnit;

    public Text dialogueText;
    public Text turnText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public Button option1;
    public Button option2;
    public Button option3;
    public Button option4;

    public BattleState state;

    private int correctAnswer;

    public LevelUpModal levelUpModal;
    private void Start() {
        state = BattleState.START;
        questionManager = FindObjectOfType<QuestionManager>();
        battleInfo = FindObjectOfType<BattleInfo>();
        StartCoroutine(SetupBattle());
    }

    private void Update() {
        if (TimerHandler.Ended()) {
            TimerHandler.ResetEndedStatus();
            if (state == BattleState.PLAYERTURN) {
                StartCoroutine(PlayerAttack(correctAnswer - 1));
            } else if(state == BattleState.ENEMYTURN) {
                StartCoroutine(EnemyAttack(correctAnswer - 1));
            }
        }
    }

    IEnumerator SetupBattle() {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName+" attacks!";

        playerUnit.SetInfo();
        enemyUnit.SetInfo();
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(SetupTurn());
    }
    private void EnemyTurn() {
        string turnTextPlaceholder = "DEFENSE TURN";
        dialogueText.text = turnTextPlaceholder;
        turnText.text = turnTextPlaceholder;
    }

    IEnumerator EnemyAttack(int option) {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);
        bool isDead = false;
        if(option != correctAnswer) {
            dialogueText.text = "Wrong answer! The enemy hits you!";
            DamagePopup.Create(playerUnit.transform.position + new Vector3(0, 1), "Hit", true);
            isDead = playerUnit.TakeDamage(enemyUnit.damage);
            playerHUD.SetHP(playerUnit);
        } else {
            dialogueText.text = "Correct answer! You take no damage!";
        }

        yield return new WaitForSeconds(2f);

        if(isDead) {
            state = BattleState.LOST;
            EndBattle();
        } else {
            SwapUnitsTurn();
            StartCoroutine(SetupTurn());
        }
    }

    private void SwapUnitsTurn() {
        if (state == BattleState.PLAYERTURN) {
            state = BattleState.ENEMYTURN;
        }
        else if (state == BattleState.ENEMYTURN) {
            state = BattleState.PLAYERTURN;
        }
    }

    private void EndBattle() {
        if(state == BattleState.WON) {
            dialogueText.text = "You won the battle!";
            playerUnit.calculatePlayerExperience(enemyUnit.experience, levelUpModal);
        }
        if(state == BattleState.LOST) {
            dialogueText.text = "You were defeated.";
        }
        playerUnit.UpdatePlayerPrefs();
    }

    IEnumerator SetupTurn() {
        changeActionButtonState(false);
        if (state == BattleState.PLAYERTURN) {
            PlayerTurn();
        }
        if(state == BattleState.ENEMYTURN) {
            EnemyTurn();
        }
        yield return new WaitForSeconds(1f);
        PrepareAnswerPhase();
        changeActionButtonState(true);
        TimerHandler.Run();
    }

    private void PlayerTurn() {
        string turnTextPlaceholder = "ATTACK TURN";
        dialogueText.text = turnTextPlaceholder;
        turnText.text = turnTextPlaceholder;
    }

    IEnumerator PlayerAttack(int option) {
        bool isDead = false;
        if(option == correctAnswer) {
            isDead = enemyUnit.TakeDamage(playerUnit.damage);
            DamagePopup.Create(enemyUnit.transform.position, "Hit!", true);
            enemyHUD.SetHP(enemyUnit);
            dialogueText.text = "Correct answer!";
        } else {
            DamagePopup.Create(enemyUnit.transform.position, "Whack...", false);
            dialogueText.text = "Wrong answer!";
        }
        yield return new WaitForSeconds(2f);
        if(isDead) {
            state = BattleState.WON;
            EndBattle();
        } else {
            SwapUnitsTurn();
            StartCoroutine(SetupTurn());
        }
    }

    void PrepareAnswerPhase() {
        QuestionObj questionObj = questionManager.GenerateQuestion(battleInfo.Chapter);
        correctAnswer = questionObj.Correct_answer;
        SetButtons(questionObj.Answers);
        dialogueText.text = questionObj.Question;
    }

    void SetButtons(string[] answers) {
        Button[] buttons = new Button[] { option1, option2, option3, option4 };
        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].GetComponentInChildren<Text>().text = answers[i];
        }
    }

    public void OnAttackButton(int option) {
        TimerHandler.Stop();
        if(state == BattleState.PLAYERTURN) {
            changeActionButtonState(false);
            StartCoroutine(PlayerAttack(option));
        }
        if(state == BattleState.ENEMYTURN) {
            changeActionButtonState(false);
            StartCoroutine(EnemyAttack(option));
        } else {
            return;
        }
    }

    void changeActionButtonState(bool actionButtonState) {
        option1.interactable = actionButtonState;
        option2.interactable = actionButtonState;
        option3.interactable = actionButtonState;
        option4.interactable = actionButtonState;
    }
}
