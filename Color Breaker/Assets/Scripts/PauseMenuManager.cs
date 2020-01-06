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
    public Text highScoreText;
    public Text gameOverText;
    public float gameOverTime = 2.2f; // must be close to length of breaking sound or slightly longer

    [Header("TapToStart")]
    public Text tapToStartText;
    public ParticleSystem tapToStartParticles;

    public GameObject soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public GameObject musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private bool isPaused = false; // Is the game currently in a paused state (no gameplay occuring)

    

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame(); // Resets UI to gameplay state
        ToggleTapToStart(false); // Turns off tap to start on load
        SetSoundImage();
        SetMusicImage();
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
    public void EndGame(int coins, bool highScore)
    {
        pauseButton.gameObject.SetActive(false);

/*
        TogglePause(true);
        pausePanel.SetActive(true);
        mainEndMenu.SetActive(true);
*/
        coinsText.text = "+ " + coins + " ¢";
        if (highScore)
        {
            highScoreText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
        } else
        {
            highScoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
        }

        StartCoroutine(EndingCoroutine());
    }

    IEnumerator EndingCoroutine()
    {
        yield return new WaitForSeconds(gameOverTime);

        TogglePause(true);
        pausePanel.SetActive(true);
        mainEndMenu.SetActive(true);
    }


    // Updates the UI element responsible for displaying distance to the player
    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance.ToString() + " meters";
    }

    public void ToggleTapToStart(bool turnOn)
    {
        tapToStartParticles.gameObject.SetActive(turnOn);
        tapToStartText.gameObject.SetActive(turnOn);
    }

    #region Sound Effects and Music

    public void ToggleSoundEffects()
    {
        int newSound = PlayerPrefsController.GetSoundEffects() == 1 ? 0 : 1;
        PlayerPrefsController.SetSoundEffects(newSound);
        SetSoundImage();
    }

    private void SetSoundImage()
    {
        if (PlayerPrefsController.GetSoundEffects() == 1)
        {
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        } else 
        {
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }

    public void ToggleMusic()
    {
        int newMusic = PlayerPrefsController.GetMusic() == 1 ? 0 : 1;
        PlayerPrefsController.SetMusic(newMusic);
        SetMusicImage();
    }

    private void SetMusicImage()
    {
        if (PlayerPrefsController.GetMusic() == 1)
        {
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        } else 
        {
            musicButton.GetComponent<Image>().sprite = musicOffSprite;
        }
    }


    #endregion
}
