using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    [SerializeField] private string _info = string.Empty;
    public string info { get { return _info; } }

    [SerializeField] private bool _isCorrect = false;
    public bool isCorrect { get { return _isCorrect; } }

}

public class Question : MonoBehaviour
{
    public enum AnswerType { Multi, Single }

    [SerializeField] private string _info = string.Empty;
    public string info { get { return _info; } }

    [SerializeField] Answer[] _answers = null;
    public Answer[] Answers { get { return _answers; } }

    // Parameters

    [SerializeField] private bool _useTimer = false;
    public bool UseTimer { get { return _useTimer; } }

    [SerializeField] private int _timer = 0;
    public int Timer { get { return _timer; } }

    [SerializeField] private AnswerType _answerType = 0;
    public AnswerType answerType { get { return _answerType; } }

    [SerializeField] private double _addScore = 10;
    public double AddScore { get { return _addScore; } }

    public List<int> GetCorrectAnswers ()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
            if(Answers[i].isCorrect)
            {
                CorrectAnswers.Add(i);
            }
        }
        return CorrectAnswers;
    }



}
