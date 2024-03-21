using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    //organizing the variable by groups
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();// we use this sintax due to instantiate the list and be able to safely remove the serializedField

    //organizing the variable by groups
    [Header("Answer")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField]  Sprite defaultAnswerSprite; //have a look later when this is used. maybe it is because we need to use buttons
    [SerializeField]  Sprite correctAnswerSprite; //have a look later when this is used. maybe it is because we need to use buttons

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgessBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        // if statement for loading the next question. it get a public bool on the timer script
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

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
        scoreText.text = "Score " + scoreKeeper.CalculateScore() + "%";

    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCanswerIndex())
        {
            questionText.text = "Correct";
            //changing the image to correct answer sprite - uses the using UnityEngine.UI;
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCanswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, ther correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    //sets up a new question and allow the player to click on a new button
    void GetNextQuestion()
    {
        //checking if whether there are still questions in our list
        if (questions.Count > 0)
        { 
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestions();
            SetButtonState(true);
            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);// chooses and random position from the list, starting on the index 0
        currentQuestion = questions[index];// assign the question from the list to the current question displayed

        if (questions.Contains(currentQuestion))// checks if the question really contains the value of the current question
        { 
            questions.Remove(currentQuestion);//removes the current question from the list once used
        }
    }

    private void DisplayQuestions()
    {
        questionText.text = currentQuestion.GetQuestion();

        //making a for loop in order to populate the question and answer field
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
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
