using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager Instance;

    //Learning System Section
    private List<List<Texture2D>> chapters;
    public List<Texture2D> chapter1;
    public List<Texture2D> chapter2;
    public List<Texture2D> chapter3;
    public List<Texture2D> chapter4;
    public List<Texture2D> chapter5;
    public List<Texture2D> chapter6;
    public List<Texture2D> chapter7;
    public List<Texture2D> chapter8;
    public List<Texture2D> chapter9;
    public List<Texture2D> chapter10;
    public List<Texture2D> chapter11;
    public List<Texture2D> chapter12;

    //Unit Sprites Section
    public List<Sprite[]> test;

    //Question System Section
    public TextAsset public_questions;
    private static TextAsset questions;

    //Unit List Section
    public TextAsset public_units;
    private static TextAsset units;

    public static Dictionary<int, List<Texture2D>> chapters_dict;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Awake() {
        chapters_dict = new Dictionary<int, List<Texture2D>>();
        chapters = new List<List<Texture2D>>();
        PopulateListOfChapters();
        questions = public_questions;
        units = public_units;
    }

    private void PopulateListOfChapters() {
        chapters.Add(chapter1);
        chapters.Add(chapter2);
        chapters.Add(chapter3);
        chapters.Add(chapter4);
        chapters.Add(chapter5);
        chapters.Add(chapter6);
        chapters.Add(chapter7);
        chapters.Add(chapter8);
        chapters.Add(chapter9);
        chapters.Add(chapter10);
        chapters.Add(chapter11);
        chapters.Add(chapter12);

        PopulateDictOfChapters(chapters);
    }

    private void PopulateDictOfChapters(List<List<Texture2D>> list) {
        for (int i = 0; i < list.Count; i++) {
            chapters_dict.Add(i+1, list[i]);
        }
    }

    public static List<Texture2D> GetChapterList(int chapter) {
        return chapters_dict[chapter];
    }

    public static TextAsset GetQuestionList() {
        return questions;
    }

    public static TextAsset GetUnitList() {
        return units;
    }
}
