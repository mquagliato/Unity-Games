using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText; //this variable is for assingning the text that will be changed.
    ScoreKeeper scoreKeeper; //we Created a variable of the same type as the scorekeeper in order to access the ScoreKeeper script and use one of its functions

    void Awake()
    {
        scoreKeeper =  FindAnyObjectByType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou got a score of" + scoreKeeper.CalculateScore() + "%";
    }
}