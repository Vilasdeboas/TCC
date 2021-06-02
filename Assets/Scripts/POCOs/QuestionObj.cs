using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionObj {

    private string question;
    public string Question {
        get { return this.question; }
        private set { this.question = value; } 
    }

    public string[] answers;
    public string[] Answers {
        get { return this.answers; }
        private set { this.answers = value; }
    }

    public int correct_answer;
    public int Correct_answer {
        get { return this.correct_answer; }
        private set { this.correct_answer = value; }
    }

    public QuestionObj(string question, string[] answers, int correct_answer) {
        this.Question = question;
        this.Answers = answers;
        this.Correct_answer = correct_answer;
    }
}
