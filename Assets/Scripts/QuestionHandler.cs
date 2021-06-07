using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class QuestionHandler : MonoBehaviour {

    private static QuestionHandler Instance;

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
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

    public List<string[]> ReadCSVFileFromObject(TextAsset file, string splitter = "|") {
        string content = file.text.ToString();
        List<string> lines = new List<string>(content.Split('\n'));
        List<string[]> aux_array = new List<string[]>();
        lines.RemoveAt(0);
        for(int i = 0; i < lines.Count - 1; i++) {;
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
        string path = Application.dataPath + "/Resources/" + filepath + filename;
        string[] texture_list = Directory.GetFiles(path, "*.psd");
        System.Object[] list = Resources.LoadAll("LearningChapters/Chap1");
        List<Texture2D> aux_array = new List<Texture2D>();
        for(int i = 0; i < texture_list.Length; i++) {
            aux_array.Add(Resources.Load<Texture2D>(filepath + filename + "/" + (i+1)));
        }

        return aux_array;
    }
}
