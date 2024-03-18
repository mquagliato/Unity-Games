using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;

    //variable for switching between completeQuestion to ShowCorrectAnswer. It is public cause other script will ne to access it and the game is small.F or a bigger game use getter method instead.
    public bool isAnsweringQuestion = false;

    void Update()
    {
        UpdateTimer();
    }


    //decreasing the time
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue <= 0)
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }

        }
        else
        {
            if (timerValue <= 0)
            {
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
            }
        }
        
    }
}
