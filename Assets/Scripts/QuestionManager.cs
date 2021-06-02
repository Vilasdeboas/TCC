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
    private int last_question_pos = -1;

    private void Awake() {
        questionHandler = new QuestionHandler();
        questions_dict = new Dictionary<int, List<string[]>>();
        questions_list = questionHandler.ReadCSV(filename, filepath, "|");
        PrepareQuestions();
        QuestionObj question = GenerateQuestion(1);
        Debug.Log("Question: "+question.Question);
        Debug.Log("Answer 1: " + question.Answers[0]);
        Debug.Log("Answer 2: " + question.Answers[1]);
        Debug.Log("Answer 3: " + question.Answers[2]);
        Debug.Log("Answer 4: " + question.Answers[3]);
        Debug.Log("Correct Answer: " + question.Correct_answer);
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
        int random_pos = new System.Random().Next(0, all_questions.Count);
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
                Debug.Log(original_answer);
                Debug.Log(new_answers[i]);
                correct_answer = i;
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

        string original_correct_answer = new_question_line[Int32.Parse(new_question_line[5])];

        int correct_answer_pos = GetNewCorrectAnswerPosition(original_correct_answer, shuffled_answers);

        return new QuestionObj(question, shuffled_answers, correct_answer_pos);
    }
}
