using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject RestartUI;

    private Scene currentScene; // Declare the currentScene variable here

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GMS Start");
        currentScene = SceneManager.GetActiveScene(); // Get the current scene
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameOver()
    {
        // set gaveOverUI after die
        gameOverUI.SetActive(true);
    }

    public void RestartClickedUI()
    {
        // restart the game
        SceneManager.LoadScene(currentScene.buildIndex); // Load the current scene by its build index
    }

    public void RestartButton()
    {
        // go to dying state after click restart button and wait for dying state
        RestartUI.SetActive(true);
        StartCoroutine(WaitForRestartCanvasEnd());
    }

    public void MainMenuButton()
    {
        // go to main menu button after die
        SceneManager.LoadScene(currentScene.buildIndex); // wait for mainmenu scene
    }

    private IEnumerator WaitForRestartCanvasEnd()
    {
        // WaitForSeconds() <-- die animation lenght
        yield return new WaitForSeconds(0.64f);
        RestartClickedUI();
    }
}