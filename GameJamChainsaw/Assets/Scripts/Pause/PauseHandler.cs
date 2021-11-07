using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    public Canvas canvas;
    private bool pauseToggle = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            pauseToggle = !pauseToggle;
            print(pauseToggle);
            canvas.gameObject.SetActive(pauseToggle); //MARCHE PAS
        }
    }
    public void Continue()
    {
        pauseToggle = false;
        Time.timeScale = 1;
        canvas.gameObject.SetActive(false);
    }
    public void Retry()
    {
        //retry logic. On recharge la même scène?
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
