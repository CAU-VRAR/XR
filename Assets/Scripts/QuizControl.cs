using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizControl : MonoBehaviour
{
    private readonly List<string> _questionTextList = new()
    {
        "7 + 5 = ?",
        "15 - 9 = ?",
        "6 * 4 = ?",
        "18 / 3 = ?",
        "9 + 6 - 4 = ?"
    };

    private readonly List<(string A1, string A2, string A3)> _answerTextList = new()
    {
        ("12", "13", "14"),
        ("4", "5", "6"),
        ("22", "24", "26"),
        ("5", "6", "7"),
        ("11", "12", "13")
    };

    private readonly List<int> _answerNumberList = new() { 1, 3, 2, 2, 1 };

    public int nowQuizIndex = 0;
    
    public TextMeshPro questionText;
    public TextMeshPro answerText1;
    public TextMeshPro answerText2;
    public TextMeshPro answerText3;
    
    public UnityEvent onCorrect;
    public UnityEvent onWrong;
    
    // Start is called before the first frame update
    void Start()
    {
        questionText = transform.Find("QuestionText").GetComponent<TextMeshPro>();
        answerText1 = transform.Find("Answer1").Find("Text").GetComponent<TextMeshPro>();
        answerText2 = transform.Find("Answer2").Find("Text").GetComponent<TextMeshPro>();
        answerText3 = transform.Find("Answer3").Find("Text").GetComponent<TextMeshPro>();
        
        GameManager.instance.onStartQuiz.AddListener(SetAndActiveQuiz);
        GameManager.instance.onEndQuiz.AddListener(DisableThis);
        
        gameObject.SetActive(false);
    }

    void SetAndActiveQuiz()
    {
        gameObject.SetActive(true);
        questionText.text = _questionTextList[nowQuizIndex];
        answerText1.text = _answerTextList[nowQuizIndex].A1;
        answerText2.text = _answerTextList[nowQuizIndex].A2;
        answerText3.text = _answerTextList[nowQuizIndex].A3;
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
        nowQuizIndex++;
    }

    public void CheckAnswer(int answer)
    {
        if (_answerNumberList[nowQuizIndex] == answer)
        {
            onCorrect.Invoke();
        }
        else
        {
            onWrong.Invoke();
        }
    }
}
