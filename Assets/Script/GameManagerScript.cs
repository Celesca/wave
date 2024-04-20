using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject RestartUI;
    public GameObject AugmentUI;

    private bool isGamePaused = false;
    private bool isUIAnimationPaused = false;

    int selectedAugment = -1;
    private bool isAug1 = false;
    private bool isAug2 = false;
    private bool isAug3 = false;

    private int aug1Value;
    private int aug2Value;
    private int aug3Value;

    private Scene currentScene; // Declare the currentScene variable here

    AugmentImg augImg;

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

    private void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public void ChooseAugmentUI(int aug1, int aug2, int aug3)
    {
        aug1Value = aug1;
        aug2Value = aug2;
        aug3Value = aug3;

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

    public int GetSelectedAugment(int aug1, int aug2, int aug3)
    {
        AugmentUI.SetActive(true);
        augImg.setAllImage(aug1, aug2, aug3);

        ResetAugmentSelection();
        ChooseAugmentUI(aug1, aug2, aug3);

        if (selectedAugment == -1)
        {
            Debug.LogWarning("No augment was selected.");
        }

        return selectedAugment;
    }

    private void ResetAugmentSelection()
    {
        selectedAugment = -1;
        isAug1 = false;
        isAug2 = false;
        isAug3 = false;
    }

    public void Augment1()
    {
        ResetAugmentSelection();
        isAug1 = true;
        AugmentUI.SetActive(false);
        selectedAugment = aug1Value;
    }

    public void Augment2()
    {
        ResetAugmentSelection();
        isAug2 = true;
        AugmentUI.SetActive(false);
        selectedAugment = aug2Value;
    }

    public void Augment3()
    {
        ResetAugmentSelection();
        isAug3 = true;
        AugmentUI.SetActive(false);
        selectedAugment = aug3Value;
    }


}