using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject RestartUI;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GMS Start");
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
        SceneManager.LoadScene("Train");
    }
    
    public void RestartButton()
    {
        // go to ^&G%*&@#(R*%# after click restart button and wait for ^&G%*&@#(R*%#
        RestartUI.SetActive(true);
        StartCoroutine(WaitForRestartCanvasEnd());
        
    }

    public void MainMenuButton()
    {
        // go to main menu button after die
        SceneManager.LoadScene("Train");
    }
    
    private IEnumerator WaitForRestartCanvasEnd()
    {
        // WaitForSeconds() <-- die animation lenght
        yield return new WaitForSeconds(0.64f);
        RestartClickedUI();
    }

}
