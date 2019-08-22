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

    public Text distanceText;
    

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame(); // Resets UI to gameplay state
    }

    // Getter for use in GameManager
    public bool GetIsPaused()
    {
        return isPaused;
    }

    // Wrapper function for dealing with time
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

    // For stopping gameplay and presenting options to the player
    public void PauseGame()
    {
        TogglePause(true);
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        mainPauseMenu.SetActive(true);
    }

    // For clearing non-gameplay UI elements
    public void ResumeGame()
    {
        pauseButton.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        mainPauseMenu.SetActive(false);
        mainEndMenu.SetActive(false);
        TogglePause(false);
    }

    // Wrapper function to load Unity scene
    public void LoadLevel(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    // Screen popup for the end of the game upon player's death
    public void EndGame()
    {
        TogglePause(true);
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        mainEndMenu.SetActive(true);
    }


    // Updates the UI element responsible for displaying distance to the player
    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance.ToString() + " meters";
    }
}
