using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestionsAndAnswers
{
    public string questions;
    public string[] answers;
    public int correctAnswer;

    public QuestionsAndAnswers(string questions, string[] answers, int correctAnswer)
    {
        this.questions = questions;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
    }
}
