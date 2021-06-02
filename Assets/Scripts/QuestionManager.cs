using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private string filepath = "Assets/Resources/";
    private string filename = "questions_new.csv";
    private QuestionHandler questionHandler;
    private List<string[]> questions_list;
    private Dictionary<int, List<string[]>> questions_dict;
    private int num_of_chapters = 0;

    private void Awake() {
        questionHandler = new QuestionHandler();
        questions_dict = new Dictionary<int, List<string[]>>();
        questions_list = questionHandler.ReadCSV(filename, filepath, "|");
        PrepareQuestions();
    }

    private void PrepareQuestions() {
        GetNumberOfChapters(questions_list);
        ReadListIntoDict(questions_list);
    }
    private void ReadListIntoDict(List<string[]> list) {
        int length = list.Count;
        int chapter_pos = 6;
        for(int i = 0; i < length; i++) {
            this.questions_dict[Int32.Parse(list[i][chapter_pos])].Add(list[i]);
        }
    }

    private void GetNumberOfChapters(List<string[]> list) {
        int length = list.Count;
        int chapter_pos = 6;
        for(int i = 0; i < length; i++) {
            if(Int32.Parse (list[i][chapter_pos]) > this.num_of_chapters) {
                this.num_of_chapters = Int32.Parse(list[i][chapter_pos]);
                this.questions_dict.Add(this.num_of_chapters, new List<string[]>());
            }
        }
    }

    public List<string[]> getListByChapter(int chapter) {
        return this.questions_dict[chapter];
    }
}
