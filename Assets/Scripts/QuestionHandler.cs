using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class QuestionHandler {

    private string resources_filepath = "Assets/Resources/";
    public string question;
    public string[] answers;
    public int correct_answer;

    //private string file_path = "Assets/Resources/";
    private string file_name = "questions.csv";

    private List<string[]> options = new List<string[]>();

    public int SetInfo(int last_pos) {
        string filepath = "Assets/Resources/";
        options = ReadCSV(file_name, filepath);
        int pos = new System.Random().Next(0, options.Count);
        if(last_pos != -1) {
            while(last_pos == pos) {
                pos = new System.Random().Next(0, options.Count);
            }
        }
        last_pos = pos;

        string[] answersOldArray = new string[] { options[pos][1], options[pos][2], options[pos][3], options[pos][4] };

        string[] answersNewArray = ShuffleAnswersArray(new string[] { options[pos][1], options[pos][2], options[pos][3], options[pos][4] });

        question = options[pos][0];
        answers = answersNewArray;
        correct_answer = GetNewCorrectAnswerPosition(answersOldArray[Int32.Parse(options[pos][5])], answersNewArray);

        return last_pos;
    }

    public string[] ShuffleAnswersArray(string[] array) {
        System.Random random = new System.Random();

        for(int i = array.Length - 1; i > 0; i--) {
            int randomIndex = random.Next(0, i + 1);
            string temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        return array;
    }

    public int GetNewCorrectAnswerPosition(string correctAnswer, string[] array) {
        int position = 0;
        for(int i = 0; i < array.Length; i++) {
            if(array[i] == correctAnswer) {
                position = i;
            }
        }
        return position;
    }

    public List<string[]> ReadCSV(string filename, string filepath, string splitter = ",") {
        string path = filepath + filename;
        string content = new StreamReader(path).ReadToEnd();
        List<string> lines = new List<string>(content.Split('\n'));
        List<string[]> aux_array = new List<string[]>();
        lines.RemoveAt(0);
        for(int i = 0; i < lines.Count-1; i++) {
            aux_array.Add(lines[i].Split(Convert.ToChar(splitter[0])));
        }

        return aux_array;
    }

    public List<string> ReadTextFile(string filename, string filepath) {
        string path = filepath + filename;
        string content = new StreamReader(path).ReadToEnd();
        List<string> lines = new List<string>(content.Split('\n'));
        List<string> aux_array = new List<string>();
        //lines.RemoveAt(0);
        for(int i = 0; i < lines.Count; i++) {
            aux_array.Add(lines[i]);
            Debug.Log(lines[i]);
        }

        return aux_array;
    }

    public List<Texture2D> ReadTextureFile(string filename, string filepath) {
        string path = this.resources_filepath + filepath + filename;
        string[] texture_list = Directory.GetFiles(path, "*.psd");
        List<Texture2D> aux_array = new List<Texture2D>();
        for(int i = 0; i < texture_list.Length; i++) {
            aux_array.Add(Resources.Load<Texture2D>(filepath + filename + "/" + (i+1)));
        }

        return aux_array;
    }
}
