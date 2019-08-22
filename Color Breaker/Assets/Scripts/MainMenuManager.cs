using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private const string GAMESCENE = "GameScene"; // Scene where gameplay takes place

    // Start is called before the first frame update
    void Start()
    {
        
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
}
