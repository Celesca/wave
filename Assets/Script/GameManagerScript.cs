using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject RestartUI;
    public GameObject AugmentUI;

    private bool isGamePaused = false;

    Augment selectedAugment = null;

    private bool isAug1 = false;
    private bool isAug2 = false;
    private bool isAug3 = false;

    private Scene currentScene; // Declare the currentScene variable here


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene(); // Get the current scene
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
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

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public void ChooseAugmentUI(Augment aug1, Augment aug2, Augment aug3)
    {
        PauseGame();
        AugmentUI.SetActive(true);
        if (isAug1)
        {
            selectedAugment = aug1;
        }
        else if (isAug2)
        {
            selectedAugment = aug2;
        }
        else if (isAug3)
        {
            selectedAugment = aug3;
        }
    }

    public Augment GetSelectedAugment(Augment aug1, Augment aug2, Augment aug3)
    {
        ChooseAugmentUI(aug1, aug2, aug3);
        return selectedAugment;
    }

    public void Augment1()
    {
        Debug.Log("Augment 1 Clicked");
        isAug1 = true;
        AugmentUI.SetActive(false);
        ResumeGame();
    }

    public void Augment2()
    {
        Debug.Log("Augment 2 Clicked");
        isAug2 = true;
        AugmentUI.SetActive(false);
        ResumeGame();
    }

    public void Augment3()
    {
        Debug.Log("Augment 3 Clicked");
        isAug3 = true;
        AugmentUI.SetActive(false);
        ResumeGame();
    }
}