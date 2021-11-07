using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Fade
{
    public float fadeDuration;
    public AnimationCurve fadeCurve;
}

[System.Serializable]
public struct Music
{
    public AudioClip intro;
    public AudioClip loop;
}



public class SoundManager : MonoBehaviour
{
    // singleton
    private static SoundManager _instance = null;

    // membres pour la gestion des fade
    private float fadeTimeLeft = 0;
    private AnimationCurve fadeGradient;
    private float introRemaining = 0;

    [SerializeField]
    private Fade fadeIn;
    [SerializeField]
    private Fade fadeOut;

    // reference vers la source musique
    AudioSource musicAudioSource;

    // reference vers la musique courante
    private Music sceneMusic;

    // reference vers les differentes musiques
    [SerializeField]
    private Music menuMusic;
    [SerializeField]
    private Music backgroundMusic;

    // reference vers la source bruitages
    AudioSource fxAudioSource;

    public static SoundManager Instance() { 
        if (_instance != null)
        {
            Debug.LogError("Sound manager does not exist");
        }
        return _instance;
    }

    private void SetFade(Fade fade)
    {
        this.fadeTimeLeft = fade.fadeDuration;
        this.fadeGradient = fade.fadeCurve;
    }

    public void PlaySound(AudioClip fx) {
        fxAudioSource.PlayOneShot(fx);
    }

    private void PlayMusic(Music music)
    {
        SetFade(fadeIn);
        introRemaining = music.intro.length * 0.001f;
        musicAudioSource.clip = music.intro;
        musicAudioSource.Play();
    }

    private void PlayLoop(Music music) {
        musicAudioSource.clip = music.loop;
    }

    private void StopMusic()
    {
        SetFade(fadeOut);
    }

    public void PlayMenuMusic(Music menuMusic)
    {
        sceneMusic = menuMusic;
        PlayMusic(menuMusic);
    }

    public void StopMenuMusic()
    {
        StopMusic();
    }

    public void PlayBGM(Music bgm)
    {
        sceneMusic = bgm;
        PlayMusic(bgm);
    }

    public void StopBGM()
    {
        StopMusic();
    }

    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        // Apply fade to music
        if(fadeTimeLeft >= 0)
        {
            musicAudioSource.volume = fadeGradient.Evaluate(fadeTimeLeft);
            fadeTimeLeft -= Time.deltaTime;
        }

        // Trigger loop after intro duration is over
        if (introRemaining >= 0)
        {
            introRemaining -= Time.deltaTime;
        } else
        {
            PlayLoop(sceneMusic);
        }
        
    }
}
