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
    private Scene currentScene;

    private int aug1Value;
    private int aug2Value;
    private int aug3Value;

    AugmentController augControl;
    public AugmentImg augmentImgComponent;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene(); // Get the current scene
        augControl = FindObjectOfType<AugmentController>();

        /*
        augmentImgComponent = FindObjectOfType<AugmentImg>();

        if (augmentImgComponent == null)
        {
            Debug.LogError("AugmentImg component not found!");
        }
        */
    }

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

    // called from AugmentController
    public void GetSelectedAugment(int aug1, int aug2, int aug3)
    {
        AugmentUI.SetActive(true);
        Debug.Log($"In GameManagerScript send to AugmentImg: {aug1}, {aug2}, {aug3}");

        augmentImgComponent.setAllImage(aug1, aug2, aug3);
        this.aug1Value = aug1;
        this.aug2Value = aug2;
        this.aug3Value = aug3;
    }

    /* call augment */

    public void Augment1()
    {
        augControl.PerformAction(aug1Value);
        AugmentUI.SetActive(false);
    }

    public void Augment2()
    {
        augControl.PerformAction(aug2Value);
        AugmentUI.SetActive(false);
    }

    public void Augment3()
    {
        augControl.PerformAction(aug3Value);
        AugmentUI.SetActive(false);
    }
}