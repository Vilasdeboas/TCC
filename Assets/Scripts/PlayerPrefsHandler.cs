using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsHandler : MonoBehaviour
{
    public static void Save(string name, int hp, int current_hp, int damage, int level, int totalExperience, int requiredExperience, int actualExperience) {
        PlayerPrefs.SetString(PlayerData.PlayerStatus.NAME.ToString(), name);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.HP.ToString(), hp);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.CURRENT_HP.ToString(),current_hp);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.DAMAGE.ToString(), damage);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.LEVEL.ToString(), level);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.ACTUAL_EXPERIENCE.ToString(), actualExperience);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.TOTAL_EXPERIENCE.ToString(), totalExperience);
        PlayerPrefs.SetInt(PlayerData.PlayerStatus.REQUIRED_EXPERIENCE.ToString(), requiredExperience);
    }

    public static PlayerData Load() {
        return new PlayerData();
    }
}
