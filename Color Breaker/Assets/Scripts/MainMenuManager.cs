using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject mainStartMenu;
    public GameObject colorSelectionMenu;
    public GameObject settingsMenu;
    public GameObject primaryColorButton;
    public GameObject secondaryColorButton;
    public GameObject colorMenu1;
    public GameObject colorMenu2;
    public int[] costOfEachColor; // NEEDS TO BE SAME LEN AS COLOR MENUS
    public Text coinsText;
    public Text highScoreText;
    private Color fadedGray = new Color(.7f, .7f, .7f);
    private bool needCoins = false;



    private const string GAMESCENE = "GameScene"; // Scene where gameplay takes place

    // Start is called before the first frame update
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

    public void OpenMainMenu()
    {
        mainStartMenu.SetActive(true);
        colorSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        colorMenu1.SetActive(false);
        colorMenu2.SetActive(false);
        needCoins = false;
        highScoreText.text = "High Score: " + PlayerData.GetHighScore() + " meters";
    }

    public void BackToMainMenu()
    {
        SaveSystem.SaveGame();
        OpenMainMenu();
        needCoins = false;
    }

    public void OpenColorMenu1()
    {
        colorMenu1.SetActive(true);
        colorMenu2.SetActive(false);
        primaryColorButton.GetComponent<Image>().color = Color.white;
        secondaryColorButton.GetComponent<Image>().color = fadedGray;
    }

    public void OpenColorMenu2()
    {
        colorMenu1.SetActive(false);
        colorMenu2.SetActive(true);
        primaryColorButton.GetComponent<Image>().color = fadedGray;
        secondaryColorButton.GetComponent<Image>().color = Color.white;
    }

    public void OpenSettingsMenu()
    {
        mainStartMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
