using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Music
{
    public string name;
    public AudioClip intro;
    public AudioClip loop;
}

[CreateAssetMenu(fileName = "New Musics", menuName = "Musics", order = 1)]
public class Musics : ScriptableObject
{
    [Header("Game musics")]
    public Music[] musicList;
}
