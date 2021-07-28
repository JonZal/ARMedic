using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Question[] _questions = null;
    public Question[] Questions { get { return _questions; } }

    private List<int> _finishedQuestions = new List<int>(); // so we don't repeat questions
    private int currentQuestion = 0;
    private double score;

    void Display()
    {

    }
}
