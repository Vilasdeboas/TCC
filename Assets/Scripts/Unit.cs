using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public bool isPlayer;
    private BattleInfo battleInfo;

    public void SetInfo() {
        battleInfo = GameObject.Find("BattleInfo").GetComponent<BattleInfo>();
        if(isPlayer) {
            unitName = battleInfo.player.unitName;
            unitLevel = battleInfo.player.unitLevel;
            damage = battleInfo.player.damage;
            maxHP = battleInfo.player.maxHP;
            currentHP = battleInfo.player.currentHP;
        } else {
            unitName = battleInfo.enemy.unitName;
            unitLevel = battleInfo.enemy.unitLevel;
            damage = battleInfo.enemy.damage;
            maxHP = battleInfo.enemy.maxHP;
            currentHP = battleInfo.enemy.currentHP;
        }
    }

    public bool TakeDamage(int damage) {
        AudioManager.Play("hit");
        currentHP -= damage;
        if(currentHP <= 0) {
            return true;
        }
        return false;
    }
}
