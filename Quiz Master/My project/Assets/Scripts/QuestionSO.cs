using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//adding an Atribute
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")] // this line creates the option for creating scriptable object on unity.
public class QuestionSO : ScriptableObject //using scrptableObjects cause we are creating scripts to contain data of objects
{
    [TextArea(2, 6)] // add the min/max amount of lines in order to type a question on the serialized field under it.
    [SerializeField]string question = "Enter a new question here";
    [SerializeField]string[] answers = new string[4];
    [SerializeField] int canswerIndex;

    //using Getter Method - Allow us to access the private variable question with a readOnly access
    //Getter Methods need to be public
    public string GetQuestion() 
    {
         return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCanswerIndex()
    {
        return canswerIndex;
    }
}
