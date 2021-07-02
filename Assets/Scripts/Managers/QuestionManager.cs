using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;
    public FileHandler fileHandler;
    //private string filepath = "Assets/Resources/";
    //private string filename = "questions.csv";
    private List<string[]> questions_list;
    private Dictionary<int, List<string[]>> questions_dict;
    private int num_of_chapters = 0;
    private int[] repeated_questions;
    private string splitter = "|";

    private void Start() {
        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Awake() {
        questions_dict = new Dictionary<int, List<string[]>>();
        //questions_list = fileHandler.ReadCSV(filename, filepath, "|");
        questions_list = fileHandler.ReadCSVFileFromObject(ResourceManager.GetQuestionList(), splitter);
        PrepareQuestions();
        repeated_questions = new int[] { -1, -1, -1 };
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

    private List<string[]> GetListByChapter(int chapter) {
        return this.questions_dict[chapter];
    }

    private string[] SelectRandomQuestion(List<string[]> all_questions) {

        bool is_repeated = true;
        int random_pos = 0;

        while(is_repeated) {
            random_pos = new System.Random().Next(0, all_questions.Count);
            is_repeated = CheckIfRepeatedQuestion(random_pos);
        }

        return all_questions[random_pos];
    }

    private string[] ShuffleAnswers(string[] answers) {
        System.Random random = new System.Random();

        for(int i = answers.Length - 1; i > 0; i--) {
            int random_index = random.Next(0, i + 1);
            string temp = answers[i];
            answers[i] = answers[random_index];
            answers[random_index] = temp;
        }

        return answers;
    }

    private int GetNewCorrectAnswerPosition(string original_answer, string[] new_answers) {
        int correct_answer = 0;
        for (int i = 0; i < new_answers.Length; i++) {
            if (original_answer == new_answers[i]) {
                correct_answer = i;
                break;
            }
        }
        return correct_answer;
    }

    public QuestionObj GenerateQuestion(int chapter) {
        List<string[]> all_questions = GetListByChapter(chapter);
        string[] new_question_line = SelectRandomQuestion(all_questions);

        string question = new_question_line[0];
        string[] shuffled_answers = ShuffleAnswers(new string[] {
            new_question_line[1], new_question_line[2], new_question_line[3], new_question_line[4]
        });

        int original_answer_pos = Int32.Parse(new_question_line[5]) + 1;
        string original_correct_answer = new_question_line[original_answer_pos];

        int correct_answer_pos = GetNewCorrectAnswerPosition(original_correct_answer, shuffled_answers);

        return new QuestionObj(question, shuffled_answers, correct_answer_pos);
    }

    private bool CheckIfRepeatedQuestion(int new_pos) {

        bool is_repeated = Array.Exists(this.repeated_questions, el => el == new_pos); // Verifica se a questão já foi utilizada
        bool new_space = Array.Exists(this.repeated_questions, el => el == -1); // Verifica se ainda existem espaços novos (-1)

        //SE for repetido e ainda tiver (ou não) espaço no vetor ENTÃO retorne false
        if(is_repeated && (new_space || !new_space)) {
            //Debug.Log("Repetead  - New Space/!New Space");
            return true;
        }
        //SE NÃO for repetido e ainda tiver espaço no vetor ENTÃO atualize o vetor de repetidos e retorne false
        else if(!is_repeated && new_space) {
            //Debug.Log("!Repetead - New Space");
            UpdateRepeatedQuestions(new_pos);
            return false;
        }
        //SE NÃO for repetido e NÃO tiver espaço no vetor ENTÃO atualize o vetor de repetidos e retorne true
        else if(!is_repeated && !new_space) {
            //Debug.Log("!Repetead - !New Space");
            ResetAndRepeat(new_pos);
            return false;
        } else {
            return true;
        }
    }

    private void ResetAndRepeat(int new_pos) {
        this.repeated_questions = new[] { new_pos, -1, -1 };
    }

    private void UpdateRepeatedQuestions(int new_pos) {
        for(int i = 0; i < this.repeated_questions.Length; i++) {
            if(this.repeated_questions[i] == -1) {
                this.repeated_questions[i] = new_pos;
                return;
            }
        }
    }
}
