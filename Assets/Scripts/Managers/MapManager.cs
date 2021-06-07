using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public BattleInfo battleInfo;
    public List<CanvasGroup> mapList;
    public static MapManager Instance;

    private void Start() {
        ShowCurrentMap();
        /*if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }*/
    }

    private void ShowCurrentMap() {
        for (int i = 0; i < mapList.Count; i++) {
            if(i == MapWatcher.CurrentMapIndex) {
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
        if(MapWatcher.CurrentMapIndex + 1 >= mapList.Count) {
            return;
        } else {
            MapWatcher.CurrentMapIndex ++;
            ShowCurrentMap();
        }
    }

    public void PreviousMap() {
        if(MapWatcher.CurrentMapIndex <= 0) {
            return;
        } else {
            MapWatcher.CurrentMapIndex--;
            ShowCurrentMap();
        }
    }
}
