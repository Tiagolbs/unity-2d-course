using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Master/New Question", order = 0)]
public class QuestionSO : ScriptableObject 
{
    [TextArea(2, 6)]
    [SerializeField] private string question = "Enter new question text here";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
}


