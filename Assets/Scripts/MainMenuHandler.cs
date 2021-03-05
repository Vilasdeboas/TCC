using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

    public BattleInfo battleInfo;

    public void LoadBattleScene() {
        SceneManager.LoadScene("BattleScene");
    }

    public void OnBattleButton(int enemy_id) {
        battleInfo.SetUnitsInfo(enemy_id);
        LoadBattleScene();
    }
}
