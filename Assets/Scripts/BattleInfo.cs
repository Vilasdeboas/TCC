using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class  BattleInfo : MonoBehaviour
{
    public UnitInfo player;
    public UnitInfo enemy;
    public static BattleInfo Instance;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void SetUnitsInfo(int enemy_id) {
        player = new UnitInfo();
        enemy = new UnitInfo();

        List<string[]> enemiesList = new List<string[]>();

        enemiesList = new QuestionHandler().ReadFile("enemy_units.csv");

        string[] enemy_info = GetEnemyFromList(enemiesList, enemy_id);

        player.unitName = "Player";
        player.unitLevel = 2;
        player.damage = 50;
        player.maxHP = 10;
        player.currentHP = player.maxHP;

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
