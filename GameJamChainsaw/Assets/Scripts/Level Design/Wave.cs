using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Enemies
{
    public GameObject enemyPrefab;
    public int count;
    public float startTimeBetweenSpawns;
    public float timeBetweenSpawns;
    public int leftToSpawn;
    public int leftToKill;
}

[CreateAssetMenu(fileName = "New wave", menuName = "Wave", order = 1)]
public class Wave : ScriptableObject
{
    [Header("Enemy types and numbers")]
    public Enemies[] enemiesList;
}
