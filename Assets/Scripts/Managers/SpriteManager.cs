using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private enum SPRITE_TYPE { IDLE, ATTACK, DEATH};

      private Dictionary<SPRITE_TYPE, string> types = new Dictionary<SPRITE_TYPE, string>()
        {
            { SPRITE_TYPE.IDLE, "_idle"},
            { SPRITE_TYPE.ATTACK, "_attack" },
            { SPRITE_TYPE.DEATH, "_death" }
        };

    private static SpriteManager Instance;

    private List<List<Sprite>> units;
    public List<Sprite> unit_1;
    public List<Sprite> unit_2;
    public List<Sprite> unit_3;
    public List<Sprite> unit_4;
    private static Dictionary<string, List<Sprite>> unit_dict;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Awake() {
        unit_dict = new Dictionary<string, List<Sprite>>();
        units = new List<List<Sprite>>();

        PopulateListOfUnits();
    }

    private void PopulateListOfUnits() {
        units.Add(unit_1);
        units.Add(unit_2);
        units.Add(unit_3);
        units.Add(unit_4);
        PopulateDictOfUnits(units);
    }

    private void PopulateDictOfUnits(List<List<Sprite>> list) {
        foreach(List<Sprite> unit in list) {
            unit_dict.Add(NameWithoutSuffix(unit[0].name), unit);
        }
    }

    private string NameWithoutSuffix(string full_name) {
        if(full_name.Contains(types[SPRITE_TYPE.IDLE])) {
            return full_name.Split(new[] { types[SPRITE_TYPE.IDLE] }, System.StringSplitOptions.None)[0];
        } 
        else if(full_name.Contains(types[SPRITE_TYPE.ATTACK])) {
            return full_name.Split(new[] { types[SPRITE_TYPE.ATTACK] }, System.StringSplitOptions.None)[0];
        } 
        else if(full_name.Contains(types[SPRITE_TYPE.DEATH])) {
            return full_name.Split(new[] { types[SPRITE_TYPE.DEATH] }, System.StringSplitOptions.None)[0];
        } 
        else {
            return full_name;
        }
    }

    public static List<Sprite> GetUnitSpriteList(string unit_name) {
        return unit_dict[unit_name];
    }
}
