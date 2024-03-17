using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField]QuestionSO question;
    [SerializeField]TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI.text = question.GetQuestion();
    }
}
