using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors
{
    White,
    Red,
    Green,
    Blue,
    Pink
}

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public int id;              // Important, un id diff�rent par monstre
    public int health;
    public float speed;
    public Colors color;
}