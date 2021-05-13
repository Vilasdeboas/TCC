using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpModal : MonoBehaviour {
    private static GameObject gameObjectRef;
    public Slider levelUpBar;
    public Text requiredExperienceText;
    public Text actualExperienceText;
    public Text totalExperienceText;
    public Text levelUpText;
    public Button closeButton;

    private static bool isOpen;

    void Start() {
        gameObjectRef = gameObject;
        Close();
    }

    public void PrepareAndShow(int level, int totalExperience, int actualExperience, int requiredExperience) {
        SetupModal(level, totalExperience, actualExperience, requiredExperience);
        Open();
    }

    public static void Open() {
        gameObjectRef.GetComponent<CanvasGroup>().alpha = 1;
        gameObjectRef.GetComponent<CanvasGroup>().interactable = true;
        isOpen = true;
    }

    public static void Close() {
        gameObjectRef.GetComponent<CanvasGroup>().alpha = 0;
        gameObjectRef.GetComponent<CanvasGroup>().interactable = false;
        isOpen = false;
    }

    public void EndBattle() {
        Close();
        Destroy(GameObject.Find("BattleInfo"));
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeButtonStatus(bool buttonStatus) {
        closeButton.interactable = buttonStatus;
    }

    public void SetupModal(int level, int totalExperience, int actualExperience, int requiredExperience) {
        levelUpBar.maxValue = requiredExperience;
        levelUpBar.minValue = 0;
        levelUpBar.value = actualExperience;
        levelUpText.text = "LEVEL " + level + "!";
        requiredExperienceText.text = (totalExperience + requiredExperience).ToString();
        totalExperienceText.text = totalExperience.ToString();
        actualExperienceText.text = actualExperience.ToString();
    }

    public IEnumerator AnimateSlider(int level, int totalExperience, int actualExperience, int requiredExperience) {
        if(!isOpen) {
            Open();
        }
        int expDifference = System.Math.Abs(actualExperience - requiredExperience);
        SetupModal(level, totalExperience, actualExperience, requiredExperience);
        int times = actualExperience / 10;
        int x = 0;
        //SetupModal(level, totalExperience, 0, requiredExperience);
        for(int i = 0; i < times; i++) {
            Debug.Log("PORQUE DEUS");
            /*if(levelUpBar.value >= levelUpBar.maxValue) {
                levelUpBar.value = levelUpBar.maxValue;
                break;
            }*/
            SetupModal(level, totalExperience, x, requiredExperience);
            x += times;
            yield return new WaitForSeconds(1f);
        }
    }
}
