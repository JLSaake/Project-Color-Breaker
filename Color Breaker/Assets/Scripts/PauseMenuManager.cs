using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{
    [Tooltip("Text field for displaying distance to the player")]
    public Text distanceText;
    [Tooltip("Panel that translates touch input into gameplay aciton")]
    public GameObject playPanel;
    [Tooltip("Button to stop gameplay")]
    public Button pauseButton;
    [Tooltip("Tinted panel that comes up to block gameplay action from occuring")]
    public GameObject pausePanel;
    [Tooltip("GameObject that cointains pause menu UI elements as children")]
    public GameObject mainPauseMenu;
    [Tooltip("GameObject that contains end of game UI elements as children")]
    public GameObject mainEndMenu;
    public Text coinsText;

    private bool isPaused = false; // Is the game currently in a paused state (no gameplay occuring)

    

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
    public void EndGame(int coins)
    {
        TogglePause(true);
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        mainEndMenu.SetActive(true);
        coinsText.text = "+ " + coins + " ¢";
    }


    // Updates the UI element responsible for displaying distance to the player
    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance.ToString() + " meters";
    }
}
