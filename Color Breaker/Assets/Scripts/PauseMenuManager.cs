using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{
    /*
        Manager responsible for handling all menu
        logic and time manipulation while gameplay 
        is paused or ended.
    */

    #region Gameplay UI
    
    [Header("Gameplay UI")]
    [Tooltip("Text field for displaying distance to the player")]
    public Text distanceText;
    [Tooltip("Panel that translates touch input into gameplay aciton")]
    public GameObject playPanel;
    [Tooltip("Button to stop gameplay")]
    public Button pauseButton;

    #endregion

    #region Pause and Endgame UI

    [Header("Pause and Endgame UI")]
    [Tooltip("Tinted panel that comes up to block gameplay action from occuring")]
    public GameObject pausePanel;
    [Tooltip("GameObject that cointains pause menu UI elements as children")]
    public GameObject mainPauseMenu;
    [Tooltip("GameObject that contains end of game UI elements as children")]
    public GameObject mainEndMenu;
    [Tooltip("Text displaying coins earned at end of game")]
    public Text coinsText;
    [Tooltip("Text dispaying if the player received a high score at the end of the game")]
    public Text highScoreText;
    [Tooltip("Text displaying if the player did not receive a high score at the end of the game")]
    public Text gameOverText;
    [Tooltip("Delay in displaying endgame UI after the player dies")]
    public float gameOverTime = 2.2f; // must be close to length of breaking sound or slightly longer
    private bool isPaused = false; // Is the game currently in a paused state (no gameplay occuring)


    [Header ("Music and Sound UI")]
    [Tooltip("Button where current sound state sprite is displayed")]
    public GameObject soundButton;
    [Tooltip("Sprite to display when sound is on")]
    public Sprite soundOnSprite;
    [Tooltip("Sprite to display when sound is off")]
    public Sprite soundOffSprite;
    [Tooltip("Button where current music state is displayed")]
    public GameObject musicButton;
    [Tooltip("Sprite to dispaly when music is on")]
    public Sprite musicOnSprite;
    [Tooltip("Sprite to dispaly when music is off")]
    public Sprite musicOffSprite;

    #endregion

    #region Particles and Audio

    [Header("High Score")]
    [Tooltip("Particles to play when the player reaches a new high score")]
    public ParticleSystem highScoreParticles;
    [Tooltip("Sound to play when the player reaches a new high score")]
    public AudioClip highscoreSound;
    private AudioSource highScoreAudioSource; // Audio source to play high score sound with
    private bool playEndParticles = false; // Do high score particles get played


    [Header("TapToStart")]
    [Tooltip("Text to display when tap to start is active")]
    public Text tapToStartText;
    [Tooltip("Particles to dispaly when tap to start is active")]
    public ParticleSystem tapToStartParticles;
    
    #endregion    

    // Set UI to gameplay state and update music and sound icons
    void Start()
    {
        ResumeGame(); // Resets UI to gameplay state
        ToggleTapToStart(false); // Turns off tap to start on load
        SetSoundImage();
        SetMusicImage();
        highScoreAudioSource = highScoreParticles.GetComponent<AudioSource>();
    }

    #region Menu Logic

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
        coinsText.text = "+ " + coins + " ¢";
        if (highScore)
        {
            highScoreText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            playEndParticles = true;

        } else
        {
            highScoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
            playEndParticles = false;
        }

        StartCoroutine(EndingCoroutine());
    }

    // Wait to display endgame UI elements
    IEnumerator EndingCoroutine()
    {
        yield return new WaitForSeconds(gameOverTime);

        pausePanel.SetActive(true);
        mainEndMenu.SetActive(true);
        if (playEndParticles)
        {
            PlayHighScoreEffects();
        }
    }

    // Play high score sound and particles
    private void PlayHighScoreEffects()
    {
        highScoreParticles.Play();
        highScoreAudioSource.clip = highscoreSound;
        if (PlayerPrefsController.GetSoundEffects() == 1)
        {
            highScoreAudioSource.Play();
        }
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

    #endregion

    #region Sound Effects and Music

    // Check to see if the sound effects need to be updated
    public void ToggleSoundEffects()
    {
        int newSound = PlayerPrefsController.GetSoundEffects() == 1 ? 0 : 1;
        PlayerPrefsController.SetSoundEffects(newSound);
        SetSoundImage();
    }

    // Update sound effects sprite
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

    // Check to see if the music needs to be updated
    public void ToggleMusic()
    {
        int newMusic = PlayerPrefsController.GetMusic() == 1 ? 0 : 1;
        PlayerPrefsController.SetMusic(newMusic);
        SetMusicImage();
    }

    // Update music sprite
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
