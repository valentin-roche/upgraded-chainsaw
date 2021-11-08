using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveNumberUpdater : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI waveNumberTextField;
    public void SetWaveNumber(string num)
    {
        waveNumberTextField.text = num;
    }
}
