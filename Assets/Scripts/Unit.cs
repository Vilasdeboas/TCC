using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public enum Animation { IDLE, ATTACK, DIE};

    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int experience;
    public int actualExperience;
    public int requiredExperience;
    public int totalExperience;
    public bool isPlayer;
    private readonly string unitsPath = "Sprites/Units/";
    private BattleInfo battleInfo;
    private int originalUnitLevel;

    private List<Sprite> sprites = new List<Sprite>();

    public void SetInfo() {
        battleInfo = GameObject.Find("BattleInfo").GetComponent<BattleInfo>();
        if(isPlayer) {
            PlayerData playerData = PlayerPrefsHandler.Load();
            unitName = playerData.name;
            unitLevel = playerData.level;
            originalUnitLevel = unitLevel;
            damage = playerData.damage;
            maxHP = playerData.hp;
            currentHP = playerData.current_hp;
            actualExperience = playerData.actualExperience;
            requiredExperience = playerData.requiredExperience;
            totalExperience = playerData.totalExperience;
        } else {
            unitName = battleInfo.enemy.unitName;
            unitLevel = battleInfo.enemy.unitLevel;
            damage = battleInfo.enemy.damage;
            maxHP = battleInfo.enemy.maxHP;
            currentHP = battleInfo.enemy.currentHP;
            experience = new System.Random().Next(unitLevel, unitLevel + 2) * 10;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = GetSpriteByUnitName(unitName);
            StartAnimation(Animation.IDLE, unitName);
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

    private Sprite GetSpriteByUnitName(string name) {
        name = name.ToLower().Replace(" ", "_"); ;
        Sprite sprite = Resources.Load<Sprite>(unitsPath+ name + "/"+ name+ "_idle1");
        return sprite;
    }
    public void UpdatePlayerPrefs() {
        PlayerPrefsHandler.Save(unitName, maxHP, currentHP, damage, unitLevel, totalExperience, requiredExperience, actualExperience);
    }

    public void calculatePlayerExperience(int enemyExperience, LevelUpModal levelUpModal) {
        bool levelUpDone = false;
        int actual = actualExperience + enemyExperience;
        levelUpModal.PrepareAndShow(originalUnitLevel, totalExperience, actual, requiredExperience);
        while(!levelUpDone) { 
            StartCoroutine(levelUpModal.AnimateSlider(unitLevel, totalExperience, actual, requiredExperience));
            if(actual >= requiredExperience) {
                totalExperience += requiredExperience;
                actual -= requiredExperience;
                actualExperience = actual;
                unitLevel++;
                requiredExperience = unitLevel * 1;
            } else {
                levelUpDone = true;
            }
        }
    }

    private void StartAnimation(Animation option, string unitName) {
        GetSpriteListByAnimationType(option, unitName);
        switch(option) {
            case Animation.IDLE:
                IdleAnimationWrapper();
                break;
            case Animation.ATTACK:
                break;
            case Animation.DIE:
                break;
            default:
                break;
        }
    }

    private void GetSpriteListByAnimationType(Animation option, string unitName) {
        sprites.Clear();
        unitName = unitName.ToLower().Replace(" ", "_");
        string[] sprite_list = Directory.GetFiles("Assets/Resources/Sprites/Units/"+unitName, "*.psd");
        for (int i = 0; i < sprite_list.Length; i++) {
            sprites.Add(Resources.Load<Sprite>("Sprites/Units/"+unitName+"/"+unitName+"_idle" + (i + 1)));
        }
    }

    private void IdleAnimationWrapper() {
        StartCoroutine(IdleAnimation());
    }

    private IEnumerator IdleAnimation() {
        for(int i = 0; i < sprites.Count; i++) {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites[i];
            yield return new WaitForSeconds(0.5f);
        }
        IdleAnimationWrapper();
    }
}
