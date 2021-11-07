using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationWingController : MonoBehaviour
{
    public Color firstColor;
    public Color secondColor;
    public Color thirdColor;
    public float startTimeBetweenTwoColors;
    private float timeBetweenTwoColors;
    public SpriteRenderer sprite;

    private int actualColorToAimFor = 2;

    void Start()
    {
        timeBetweenTwoColors = startTimeBetweenTwoColors;
    }

    void Update()
    {
        switch(actualColorToAimFor)
        {
            case 2:
                LerpColor(secondColor);
                break;
            case 3:
                LerpColor(thirdColor);
                break;
            case 1:
                LerpColor(firstColor);
                break;
        }
    }

    private void LerpColor(Color color)
    {
        sprite.material.SetColor("_Color", Color.Lerp(sprite.material.color, color, Time.deltaTime / timeBetweenTwoColors));
        sprite.material.SetColor("_EmissionColor", Color.Lerp(sprite.material.color, color, Time.deltaTime / timeBetweenTwoColors) * 2f);
        //sprite.material.color = Color.Lerp(sprite.material.color, color, Time.deltaTime / timeBetweenTwoColors);
        timeBetweenTwoColors -= Time.deltaTime;
        if (timeBetweenTwoColors <= 0)
        {
            timeBetweenTwoColors = startTimeBetweenTwoColors;
            //sprite.material.color = color;
            sprite.material.SetColor("_Color", Color.Lerp(sprite.material.color, color, Time.deltaTime / timeBetweenTwoColors));
            sprite.material.SetColor("_EmissionColor", Color.Lerp(sprite.material.color, color, Time.deltaTime / timeBetweenTwoColors) * 2f);
            switch (actualColorToAimFor)
            {
                case 2:
                    actualColorToAimFor = 3;
                    break;
                case 3:
                    actualColorToAimFor = 1;
                    break;
                case 1:
                    actualColorToAimFor = 2;
                    break;
            }
        }
    }
}
