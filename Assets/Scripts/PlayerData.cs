using UnityEngine;

public class PlayerData {

    public enum PlayerStatus { NAME, HP, CURRENT_HP, DAMAGE, LEVEL, TOTAL_EXPERIENCE, REQUIRED_EXPERIENCE, ACTUAL_EXPERIENCE };

    public string name;
    public int hp;
    public int current_hp;
    public int damage;
    public int level;
    public int totalExperience;
    public int requiredExperience;
    public int actualExperience;

    public PlayerData() {
        this.name = PlayerPrefs.GetString(PlayerStatus.NAME.ToString());
        this.hp = PlayerPrefs.GetInt(PlayerStatus.HP.ToString());
        this.current_hp  = PlayerPrefs.GetInt(PlayerStatus.CURRENT_HP.ToString());
        this.damage = PlayerPrefs.GetInt(PlayerStatus.DAMAGE.ToString());
        this.level = PlayerPrefs.GetInt(PlayerStatus.LEVEL.ToString());
        this.actualExperience = PlayerPrefs.GetInt(PlayerStatus.ACTUAL_EXPERIENCE.ToString());
        this.requiredExperience = PlayerPrefs.GetInt(PlayerStatus.REQUIRED_EXPERIENCE.ToString());
        this.totalExperience = PlayerPrefs.GetInt(PlayerStatus.TOTAL_EXPERIENCE.ToString());
    }
}
