using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool gamePaused = false;

    private GameObject pauseMenu;
    private GameObject gameUI;
    private GameObject gameOverMenu;
    private SceneLoader loader;

    private void Awake()
    {
        AudioListener.pause = false;
    }

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        gameUI = GameObject.Find("GameUI");
        gameOverMenu = GameObject.Find("GameOverMenu");
        loader = GameObject.Find("GameUI/SceneLoader").GetComponent<SceneLoader>();
        ResumeGame();
        HidePauseMenu();
        HideGameLost();
        gameUI.SetActive(true);

    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            FreezeGame();
            ShowPauseMenu();
        }

    }

    public void ResumeGame()
    {
        gamePaused = false;
        AudioListener.pause = false;
        Time.timeScale = 1;
        HidePauseMenu();
    }

    public void FreezeGame()
    {
        gamePaused = true;
        AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        if(loader != null)
        {
            loader.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void MainMenu()
    {
        if(loader != null)
        {
            loader.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.GetComponent<Canvas>().enabled = true;
            pauseMenu.GetComponent<CanvasScaler>().enabled = true;
        }
        else
        {
            print("Unable to find PauseMenu");
        }
    }

    public void HidePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.GetComponent<Canvas>().enabled = false;
            pauseMenu.GetComponent<CanvasScaler>().enabled = false;
        }
        else
        {
            print("Unable to find PauseMenu");
        }
    }

    public void ShowGameLost()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.GetComponent<Canvas>().enabled = true;
            gameOverMenu.GetComponent<CanvasScaler>().enabled = true;
        }
        else
        {
            print("Unable to find GameOverMenu");
        }
    }
    public void HideGameLost()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.GetComponent<Canvas>().enabled = false;
            gameOverMenu.GetComponent<CanvasScaler>().enabled = false;
        }
        else
        {
            print("Unable to find GameOverMenu");
        }
    }
}
