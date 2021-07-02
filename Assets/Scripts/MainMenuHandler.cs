using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

    public BattleInfo battleInfo;
    public LearningInfo learningInfo;
    public CanvasGroup storyModal;

    public void LoadBattleScene() {
        SceneManager.LoadScene("BattleScene");
    }



    public void OpenStoryModal() {
        storyModal.alpha = 1;
        storyModal.blocksRaycasts = true;
        storyModal.interactable = true;
    }

    public void CloseStoryModal() {
        storyModal.alpha = 0;
        storyModal.blocksRaycasts = false;
        storyModal.interactable = false;
    }

    public void LoadLearningScene(int chapter) {
        learningInfo.SetChapter(chapter);
        SceneManager.LoadScene("LearningScene");
    }

    public void OnBattleButton(int enemy_id) {
        battleInfo.SetUnitsInfo(enemy_id);
        LoadBattleScene();
    }
}
