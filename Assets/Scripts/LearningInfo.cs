using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningInfo : MonoBehaviour
{
    public static LearningInfo Instance;
    public static int chapter = 0;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void SetChapter(int value) {
        chapter = value;
    }

    public static int GetChapter() {
        return chapter;
    }
}
