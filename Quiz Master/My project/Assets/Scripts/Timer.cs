using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeToCompleteQuestion = 15f;
    float timeToShowCorrectAnswer = 5f;
    float timerValue;
    public float fillFraction;
    public bool loadNextQuestion;
    
    //variable for switching between completeQuestion to ShowCorrectAnswer. It is public cause other script will ne to access it and the game is small.F or a bigger game use getter method instead.
    public bool isAnsweringQuestion;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    //decreasing the time
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }

        }
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        }
        
    }
}
