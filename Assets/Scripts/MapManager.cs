using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public BattleInfo battleInfo;
    public List<CanvasGroup> mapList;
    private int currentMapIndex = 0;

    private void Start() {
        ShowCurrentMap();
    }

    private void ShowCurrentMap() {
        for (int i = 0; i < mapList.Count; i++) {
            if(i == currentMapIndex) {
                mapList[i].alpha = 1;
                mapList[i].interactable = true;
                mapList[i].blocksRaycasts = true;
                battleInfo.Chapter = i + 1; //Sets Chapter for Battle
            } else {
                mapList[i].alpha = 0;
                mapList[i].interactable = false;
                mapList[i].blocksRaycasts = false;
            }
        }
    }

    public void NextMap() {
        if(currentMapIndex + 1 >= mapList.Count) {
            return;
        } else {
            currentMapIndex++;
            ShowCurrentMap();
        }
    }

    public void PreviousMap() {
        if(currentMapIndex <= 0) {
            return;
        } else {
            currentMapIndex--;
            ShowCurrentMap();
        }
    }
}
