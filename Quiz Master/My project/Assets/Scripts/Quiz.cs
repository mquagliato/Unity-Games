using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    //organizing the variable by groups
    [Header("Questions")]
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;

    //organizing the variable by groups
    [Header("Answer")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField]  Sprite defaultAnswerSprite; //have a look later when this is used. maybe it is because we need to use buttons
    [SerializeField]  Sprite correctAnswerSprite; //have a look later when this is used. maybe it is because we need to use buttons

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestions();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        // if statement for loading the next question. it get a public bool on the timer script
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);//we pass the index -1 to not accidentaly pass a correct index that matches the answer index. This way you can satisfy the method index and fall in the "else" statement of the method 
            SetButtonState(false);
        }
    }

    public void OnAwnserSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();    
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == question.GetCanswerIndex())
        {
            questionText.text = "Correct";
            //changing the image to correct answer sprite - uses the using UnityEngine.UI;
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = question.GetCanswerIndex();
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, ther correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    //sets up a new question and allow the player to click on a new button
    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestions();
    }

    private void DisplayQuestions()
    {
        questionText.text = question.GetQuestion();

        //making a for loop in order to populate the question and answer field
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        Button button;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            button = answerButtons[i].GetComponent<Button>();
            //tells the state you the button is currently in
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite()
    {
        Image bImage;

        for (int i = 0;i < answerButtons.Length; i++)
        {
            bImage = answerButtons[i].GetComponent <Image>();
            bImage.sprite = defaultAnswerSprite;
        }
    }
}
