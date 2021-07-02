using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LearningSceneSystem : MonoBehaviour
{
    //private string filepath = "LearningChapters/";
    //private string filename = "Chap";
    private List<Texture2D> lesson_lines;
    //public Text maintext;
    public RawImage main_image;
    private int position = 0;
    //public QuestionHandler fileHandler;

    private void Start() {
        //fileHandler = FindObjectOfType<QuestionHandler>();
        //lesson_lines = fileHandler.ReadTextureFile(filename+LearningInfo.GetChapter(), filepath);
        lesson_lines = ResourceManager.GetChapterList(LearningInfo.GetChapter());
        PopulateScreen();
    }
    private void PopulateScreen() {
        main_image.texture = lesson_lines[position];
    }

    public void Next() {
        if(position + 1 >= lesson_lines.Count) {
            return;
        } else {
            position++;
            PopulateScreen();
        }
    }

    public void Previous() {
        if(position <= 0) {
            return;
        } else {
            position--;
            PopulateScreen();
        }
    }

    public void Return() {
        Destroy(GameObject.Find("BattleInfo"));
        SceneManager.LoadScene("MainMenu");
    }
}
