using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{

    // reference vers la source musique
    AudioSource musicAudioSource = new AudioSource();
    // reference vers la source bruitages
    AudioSource fxAudioSource = new AudioSource();

    // parametre de la musique de la scene et reference vers la musique courante
    [SerializeField]
    private AudioClip sceneMusic;


    public void StopMusic() {
        musicAudioSource.Stop();
    }

    public void SetMusicVolume(int value)
    {
        musicAudioSource.volume = value;
    }

    public void SetFXVolume(int value)
    {
        fxAudioSource.volume = value;
    }

    public void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.clip = sceneMusic;
        musicAudioSource.Play();
    }
}
