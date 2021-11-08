using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNumberUpdater : MonoBehaviour
{
    [SerializeField]
    private Sprite[] numbers;
    [SerializeField]
    private Image firstNumberImage;
    [SerializeField]
    private Image secondNumberImage;


    public void SetWaveNumber(int num)
    {
        if(num < 10)
        {
            firstNumberImage.sprite = numbers[num];
        }
        else if(num == 10)
        {
            firstNumberImage.sprite = numbers[1];
            secondNumberImage.gameObject.SetActive(true);
            secondNumberImage.sprite = numbers[0];
        }
        else
        {
            if(!secondNumberImage.gameObject.active)
                secondNumberImage.gameObject.SetActive(true);
            firstNumberImage.sprite = numbers[num/10];
            secondNumberImage.sprite = numbers[num%10];
        }
    }
}
