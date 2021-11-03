using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestButtonController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI testText;

    [SerializeField]
    private string textToChangeTo;


    public void ChangeText()
    {
        if(testText != null)
        {
            testText.text = textToChangeTo;
        }
        else
        {
            print("T'as oublie de setup le tmpro dans le bouton bg");
        }
    }
}
