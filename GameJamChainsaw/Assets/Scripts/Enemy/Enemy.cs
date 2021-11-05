using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors
{
    Red,
    Green,
    Blue,
    Pink
}

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public int health;
    public float speed;
    public Colors color;
}
