using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class  BattleInfo : MonoBehaviour
{
    private string filepath = "Assets/Resources/";
    public UnitInfo player;
    public UnitInfo enemy;

    public int Chapter {
        get { return chapter; }
        set { chapter = value; }
    }
    public int chapter;

    public static BattleInfo Instance;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void onClickSave() {
        PlayerPrefsHandler.Save("Player", 10, 10, 1, 1, 0, 1, 0);
    }

    public void SetUnitsInfo(int enemy_id) {
        enemy = new UnitInfo();
        List<string[]> enemiesList = new List<string[]>();

        enemiesList = new QuestionHandler().ReadCSV("enemy_units.csv", this.filepath);

        string[] enemy_info = GetEnemyFromList(enemiesList, enemy_id);

        enemy.unitName = enemy_info[1];
        enemy.unitLevel = Int32.Parse(enemy_info[2]);
        enemy.damage = Int32.Parse(enemy_info[3]); ;
        enemy.maxHP = Int32.Parse(enemy_info[4]); ;
        enemy.currentHP = enemy.maxHP;
    }

    private string[] GetEnemyFromList(List<string[]> enemiesList, int enemy_id) {
        string[] enemy = {};
        for(int i = 0; i < enemiesList.Count; i++) {
            if (Int32.Parse(enemiesList[i][0]) == enemy_id) {
                enemy = enemiesList[i];
            }
        }
        return enemy;
    } 
}

public class UnitInfo {
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
}
