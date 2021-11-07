using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleMenu : MonoBehaviour
{
    public void LoadPlay()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadHowToPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void HowToPlayToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
