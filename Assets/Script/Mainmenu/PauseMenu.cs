using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject pauseMenuUi;
    public GameObject optionMenuUi;

    void Update()
    {
        if (optionMenuUi.active == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionMenuUi.SetActive(false);
                pauseMenuUi.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
        gameIsPause = false;
        Debug.Log("RETURN");
    }
}
