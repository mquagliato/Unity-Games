using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField]  Sprite defaultAnswerSprite; //have a look later when this is used. maybe it is because we need to use buttons
    [SerializeField]  Sprite correctAnswerSprite; //have a look later when this is used. maybe it is because we need to use buttons

    private void Start()
    {
        DisplayQuestions();
    }
  
    public void OnAwnserSelected(int index)
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

        SetButtonState(false);
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
