using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{

    private bool isPaused = false;
    public GameObject playPanel;
    public Button pauseButton;

    public GameObject pausePanel;
    public GameObject mainPauseMenu;

    public GameObject mainEndMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void TogglePause(bool paused)
    {
        isPaused = paused;

        if (isPaused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }


// TODO: If no settings menu in pause menu, then go ahead and make into one function with bool variable
    public void PauseGame()
    {
        TogglePause(true);
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        mainPauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseButton.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        mainPauseMenu.SetActive(false);
        mainEndMenu.SetActive(false);
        TogglePause(false);
    }

    public void LoadLevel(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    // Screen popup for the end of the game
    public void EndGame()
    {
        TogglePause(true);
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        mainEndMenu.SetActive(true);
    }
}
