using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors
{
    White,
    Yellow,
    Pink,
    Blue,
    YellowPink,
    YellowBlue,
    PinkBlue,
    PinkBlueYellow
}

[System.Serializable]
public struct CodeRGB
{
    public int r;
    public int g;
    public int b;
}

[System.Serializable]
public struct SpriteColor
{
    public Colors color;
    public CodeRGB rgbCode;
}

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public int id;              // Important, un id différent par monstre
    public int health;
    public float speed;
    public SpriteColor color;
}
