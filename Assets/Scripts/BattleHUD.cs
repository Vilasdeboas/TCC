using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text hpText;
    public void SetHUD(Unit unit) {
        nameText.text = unit.unitName;
        levelText.text = "LVL " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = unit.currentHP.ToString()+"/"+unit.maxHP.ToString();
    }

    public void SetHP(Unit unit) {
        hpSlider.value = unit.currentHP;
        SetHUD(unit);
    }
}
