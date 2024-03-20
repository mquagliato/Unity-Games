using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    
    //getter method to protect the variables
    public int GetCorrectAnswers() 
    {
        return correctAnswers; 
    }

    //setter method that increments the correct answers
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    //Never forget that methods that return any type of value, MUST be the same type of the valuer returned.
    public int GetQuestionSeen()
    {
        return questionsSeen; // the methos is of type INTERGER because the return codeline returns an INTERGER Value.
    }

   
    public void IncrementQuestionSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        //the float in brackets make casts the variable in front to a float type.
        //the Mathf.RoundToInt rounds the float division to the closest int value.
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

}
