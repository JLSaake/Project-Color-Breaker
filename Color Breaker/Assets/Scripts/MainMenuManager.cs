using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    /*
        Manager responsible for controlling main menu UI elements.
    */

    #region UI Gameobjects

    [Header("UI GameObjects")]
    [Tooltip("Title screen gameobject with UI children")]
    public GameObject mainStartMenu;
    [Tooltip("Color selection screen gameobject with UI children")]
    public GameObject colorSelectionMenu;
    [Tooltip("Settings selection screen gameobject with UI children")]
    public GameObject settingsMenu;
    [Tooltip("Credits selection screen gameobject with UI children")]
    public GameObject creditsMenu;
    [Tooltip("Button for selecting primary color menu")]
    public GameObject primaryColorButton;
    [Tooltip("Button for selecting secondary color menu")]
    public GameObject secondaryColorButton;
    [Tooltip("Gameobject with UI children for primary color menu")]
    public GameObject colorMenu1;
    [Tooltip("Gameobject with UI children for secondary color menu")]
    public GameObject colorMenu2;
    [Tooltip("Window for asking the player if they wish to reset their high score")]
    public GameObject resetScoreWindow;

    [Header("Text Objects")]
    [Tooltip("Text for displaying player's total coins")]
    public Text coinsText;
    [Tooltip("Text for displaying player's current high score")]
    public Text highScoreText;

    [Header("Color Costs")]
    [Tooltip("Cost for each color, NEEDS TO BE THE SAME LENGTH AS COLOR MENUS")]
    public int[] costOfEachColor; // NEEDS TO BE SAME LEN AS COLOR MENUS

    #endregion

    #region Helper variables

    private Color fadedGray = new Color(.7f, .7f, .7f); // Color of unselected primary or secondary color button
    private bool needCoins = false; // Does the player's coin total need to be displayed
    private const string GAMESCENE = "GameScene"; // Scene where gameplay takes place

    #endregion

    // Set color costs and load game data
    void Start()
    {
        PlayerData.SetColorCosts(costOfEachColor);
        SaveSystem.LoadGame();
        OpenMainMenu();
        SkyboxManager.UpdateSkybox();
    }

    // Update is called once per frame
    void Update()
    {
        if (needCoins)
        {
            coinsText.text = PlayerData.GetCoins() + " ¢";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainStartMenu.activeInHierarchy == true) // Player in title menu and hits back key on android
            {
                Application.Quit(); // If pushing ios, check to see if this needs to be removed
            }
        }
    }

    #region Menu Opening and Closing

    // Wrapper function for loading the gameplay scene
    public void StartGame()
    {
        SceneManager.LoadScene(GAMESCENE, LoadSceneMode.Single);
    }

    // Transitions menu to color selection screen
    public void OpenColorSelection()
    {
        mainStartMenu.SetActive(false);
        colorSelectionMenu.SetActive(true);
        OpenColorMenu1();
        needCoins = true;
    }

    // Opens the main menu, used to close all other menus
    public void OpenMainMenu()
    {
        mainStartMenu.SetActive(true);
        colorSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        colorMenu1.SetActive(false);
        colorMenu2.SetActive(false);
        needCoins = false;
        highScoreText.text = "High Score: " + PlayerData.GetHighScore() + " meters";
        creditsMenu.SetActive(false);
        CloseResetScoreWindow();
    }

    // Saves game and returns to main menu
    public void BackToMainMenu()
    {
        SaveSystem.SaveGame();
        OpenMainMenu();
        needCoins = false;
    }

    // Opens primary color menu
    public void OpenColorMenu1()
    {
        colorMenu1.SetActive(true);
        colorMenu2.SetActive(false);
        primaryColorButton.GetComponent<Image>().color = Color.white;
        secondaryColorButton.GetComponent<Image>().color = fadedGray;
    }

    // Opens secondary color menu
    public void OpenColorMenu2()
    {
        colorMenu1.SetActive(false);
        colorMenu2.SetActive(true);
        primaryColorButton.GetComponent<Image>().color = fadedGray;
        secondaryColorButton.GetComponent<Image>().color = Color.white;
    }

    // Opens settings menu
    public void OpenSettingsMenu()
    {
        mainStartMenu.SetActive(false);
        settingsMenu.SetActive(true);
        creditsMenu.SetActive(false);        
    }

    // Opens credits menu from settings menu
    public void OpenCreditsMenu()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(true);        
    }

    // Opens window to ask player if they wish to reset their high score
    public void OpenResetScoreWindow()
    {
        resetScoreWindow.SetActive(true);
    }

    // Closes window asking player if they wish to reset their high score
    public void CloseResetScoreWindow()
    {
        resetScoreWindow.SetActive(false);
    }

    // Resets the player's current high score and closes the window
    public void ResetHighScore()
    {
        CloseResetScoreWindow();
        PlayerData.SetHighScore(0);
        SaveSystem.SaveGame();
    }

    #endregion
}
