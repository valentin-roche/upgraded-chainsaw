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
            GameObject.FindGameObjectWithTag("Ass").GetComponent<ShootingAssController>().paused = pauseToggle;
            canvas.gameObject.SetActive(pauseToggle); 
        }
    }
    public void Continue()
    {
        pauseToggle = false;
        GameObject.FindGameObjectWithTag("Ass").GetComponent<ShootingAssController>().paused = pauseToggle;
        Time.timeScale = 1;
        canvas.gameObject.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
