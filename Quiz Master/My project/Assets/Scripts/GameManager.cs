using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //access to quizScript
    Quiz quiz;
    //acess to endScreen
    EndScreen endScreen;

    private void Awake()
    {
        //adding the objects in from unity to the variable of each particular script type.
        quiz = FindAnyObjectByType<Quiz>();
        endScreen = FindAnyObjectByType<EndScreen>();
    }

    void Start()
    {
        //using the setting of each of both objects via codeLine
        quiz.gameObject.SetActive(true); //changes the gameObject itself to active (same as toggle the small square o the top of the object)
        endScreen.gameObject.SetActive(false); //changes the gameObject itself to inactive (same as untoggle the small square o the top of the object)
    }

    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false); 
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
