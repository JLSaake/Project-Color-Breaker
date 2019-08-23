using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject mainStartMenu;
    public GameObject colorSelectionMenu;
    public GameObject colorMenu1;
    public GameObject colorMenu2;


    private const string GAMESCENE = "GameScene"; // Scene where gameplay takes place

    // Start is called before the first frame update
    void Start()
    {
        OpenMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void OpenMainMenu()
    {
        mainStartMenu.SetActive(true);
        colorSelectionMenu.SetActive(false);
        colorMenu1.SetActive(false);
        colorMenu2.SetActive(false);
    }

    public void OpenColorMenu1()
    {
        colorMenu1.SetActive(true);
        colorMenu2.SetActive(false);
    }

    public void OpenColorMenu2()
    {
        colorMenu1.SetActive(false);
        colorMenu2.SetActive(true);
    }
}
