using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region BlockerMaterials & Colors
    // Materials that will be used for procedurally generating blockers
    
    [Header("Materials & Colors")]
    [Tooltip("Material preset used for blockers")]
    public Material material0;
    [Tooltip("Material preset used for blockers")]
    public Material material1;
    [Tooltip("Material preset used for blockers in harder game modes")]
    public Material material2;
    [Tooltip("Material preset used for blockers in harder game modes")]
    public Material material3;
    [Space(5)]
    [Tooltip("Colors for use on blockers and player - NOTE: must be more than 2 and will eventually be set by code elsewhere")]
    public Color[] colors; // Location for all colors in the game
    [Tooltip("Alpha value for blockers that are safe to pass through")]
    [Range(0,1)]
    public float transparency = 0.5f; // Transparency value for materials
    private Material[] materials; // Used for storing and transfering preset materials
    private int currIndex = 0; // Current color selected
    private Color[] colorsAlpha; // Array created from colors (above) that have transparency values (able to be passed through)

    #endregion

    #region Procedural Variables
     // For use in Procedural generation and determing chunk spawns

    [Header("Procedural Generation")]
    [Tooltip("'Z' position to start generating the first chunk")]
    public int zStartPos = 100;
    [Tooltip("Size of each chunk to generate")]
    public int chunkLength = 800;
    [Tooltip("Distance from the player to the end of the previously generated chunk to generate the next chunk")]
    public int playerZDistanceToGenerateChunk = 800;
    [Tooltip("Starting increment spacing for generating blockers")]
    public int startingStep = 60;
    [Tooltip("Decrement rate for increment spacing each chunk generation")]
    public int stepDecreaseRate = 5;
    [Tooltip("Minimum space between blockers")]
    public int stepMin = 30;
    [Tooltip("Starting percentage chance to spawn a blocker at each increment")]
    public float startFrequency = 0.5f;
    [Tooltip("Increment rate of frequency each chunk generation")]
    public float frequencyIncreaseRate = 0.05f;
    [Tooltip("Maximum percentage chance of blocker spawning at each increment")]
    public float frequencyMax = 0.8f;
    [Tooltip("Maximum number of consecutive blockers of the same color")]
    [Range(1,10)]
    public int maxConsecutiveColor = 4;
    private int currStartZ; // Current starting Z value for the next chunk
    private int currEndZ; // Current ending Z value for the next chunk
    private int currStep; // Current step value for the next chunk
    private float currFrequency; // Current frequency value for the next chunk

    #endregion

    #region Scoring
    // For calculating scoring and currency

    [Header("Scoring")]
    [Tooltip("How many raw 'Z' units equal one distance unit")]
    public int distanceDivider = 10; // Amount to divide raw Z value by for distance
    [Tooltip("How many distance units equal one currency unit")]
    public int distancePerCoin = 50; // Amount of distance needed to cover for a single coin
    private int distance = 0; // Meters traveled by the player during this level
    private int coins = 0; // In game currency earned during this level

    #endregion

    #region Private Managers and Objects
    // Other sub-managers and objects that are found by script, and their related variables

    private PauseMenuManager pm;
    private ProceduralGenerator pg; // Handles generation of blockers
    private Player player; // Player object
    private Camera mainCam; // Main camera that follows player
    private Vector3 camOffset; // Offset of the camera from the player

    #endregion

    #region Other Helper Variables
    // Helper variables used in this class only

    public AudioClip coinSound;
    public AudioSource coinSource;
    [Tooltip("Gameobject with audio source to play on awake")]
    public GameObject crashSounds;
    [Space(20)]
    [Tooltip("Time to elapse before prompting player to tap to begin playing")]
    public float tapPromptTime = 5.0f;
    public GameObject playPanel;
    private bool isPaused = false;
    private bool gameOverCompleted = false;
    private bool isHighScore = false;
    private bool hasStarted = false; // bool flag to reduce calls in update

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[2];
        colors[0] = PlayerData.GetColor1();
        colors[1] = PlayerData.GetColor2();

        _ColorCheck(); // Assert that there are enough colors in the game
        _FindObjects(); // Find player and managers

        _UpdateMaterialColors();// Handle color changing of materials
        pg.SetMaterials(materials); // Set materials in Procedural Generator
        _SetAlphaColors(); // Establish an array of transparent colors for changing materials
        materials[currIndex].SetColor("_BaseColor", colorsAlpha[currIndex]); // Set starting material to be transparent

        _SetStartingProceduralVariables(); // Alter procedural values to prepare for first procedural generation        
        pg.GenerateChunk(currStartZ, currEndZ, currStep, currFrequency, maxConsecutiveColor); // First procedural generation chunk

        SkyboxManager.UpdateSkybox();
        
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = pm.GetIsPaused();
        bool playerAlive = player.GetPlayerIsAlive();

        if (!hasStarted)
        {
            if(Time.timeSinceLevelLoad >= tapPromptTime)
            {
                pm.ToggleTapToStart(true);
            }
            
            hasStarted = player.GetPlayerStarted();
            if (hasStarted)
            {
                pm.ToggleTapToStart(false);
            }
        }


        // TODO: Move these to their own functions to clean up Update
        if (playerAlive && !isPaused) // Game is ongoing, player is moving
        {
            // Camera follow player
            mainCam.transform.position = player.transform.position - camOffset;

            // If the player is within range to generate a new chunk
            if (player.transform.position.z + playerZDistanceToGenerateChunk >= currEndZ)
            {
                CalculateChunk();
                pg.GenerateChunk(currStartZ, currEndZ, currStep, currFrequency, maxConsecutiveColor);
            }

            Blocker.UpdatePlayerPos(player.transform.position.z);
            UpdateDistance();
            CheckCoinSound();

        } else
        if (!playerAlive && !gameOverCompleted) // Player has died, round is over
        {
            // CrashSound();
            playPanel.SetActive(false);
            CalculateCoins();
            isHighScore = distance > PlayerData.GetHighScore() ? true : false; // checks if current run is new high score
            pm.EndGame(coins, isHighScore);
            SaveAtEndGame();
            gameOverCompleted = true;
        }
        
    
    }

    #region Start Functions

    // Security check to ensure that there are enough colors implemented into the game
    private void _ColorCheck()
    {
        if (colors.Length < 1) // TODO: change to 2 to prevent infinite gameplay
        {
            Debug.LogWarning("ColorWarning: Insufficient colors set on instantiation of GameManager. Switching to defaults.");
            colors = new Color[2];
            colors[0] = Color.blue;
            colors[1] = Color.red;
        }
    }

    // Finding player and managers in the scene
    private void _FindObjects()
    {
        // Find player and set starting color and camera offset
        player = GameObject.FindObjectOfType<Player>();
        player.UpdateColor(colors[currIndex]);

        // If ever add a second camera, this would need to be changed
        mainCam = Camera.main;
        camOffset = player.transform.position - mainCam.transform.position;

        // Find other managers
        pg = GameObject.FindObjectOfType<ProceduralGenerator>();
        pm = GameObject.FindObjectOfType<PauseMenuManager>();

        playPanel = GameObject.FindGameObjectWithTag("PlayPanel");
    }

    // Update material presets with chosen colors
    private void _UpdateMaterialColors()
    {
        materials = new Material[colors.Length];
        material0.SetColor("_BaseColor", colors[0]);
        materials[0] = material0;
        material1.SetColor("_BaseColor", colors[1]);
        materials[1] = material1;
        
        // Setup for harder game modes
        if (colors.Length >= 3)
        {
            material2.SetColor("_BaseColor", colors[2]);
            materials[2] = material2;
            if (colors.Length == 4)
            {
                material3.SetColor("_BaseColor", colors[3]);
                materials[3] = material3;
                if (colors.Length > 4)
                {
                    Debug.LogWarning("ColorOveruseWarning: GameManager is not set up to use more than 4 colors in a round.");
                }
            }
        }
    }

    // Setup colors with correct alpha values for altering preset materials later
    private void _SetAlphaColors()
    {
        colorsAlpha = new Color[colors.Length];
        for (int i = 0; i < colorsAlpha.Length; ++i)
        {
            colorsAlpha[i].r = colors[i].r;
            colorsAlpha[i].g = colors[i].g;
            colorsAlpha[i].b = colors[i].b;
            colorsAlpha[i].a = transparency;
        }
    }

    // Setup initial values for first procedural generation
    private void _SetStartingProceduralVariables()
    {
        currStartZ = zStartPos;
        currStep = startingStep;
        currEndZ = currStartZ + chunkLength;
        currFrequency = startFrequency;
    }
    #endregion

    #region Gameplay Functions

    // Switches the player's active color and updates necessary managers
    public void ToggleColor()
    {
        // Start the player moving on first screen tap
        if(!player.GetPlayerStarted())
        {
            player.PlayerStart();
        }
        materials[currIndex].SetColor("_BaseColor", colors[currIndex]);
        ++currIndex;
        if (currIndex >= colors.Length)
        {
            currIndex = 0;
        }
        materials[currIndex].SetColor("_BaseColor", colorsAlpha[currIndex]);
        player.UpdateColor(colors[currIndex]);
        Blocker.UpdateTransparentColor(colors[currIndex]);
    }

    // Determine next variables for procedural generation chunk
    private void CalculateChunk()
    {
        currStartZ = currEndZ;
        currStep -= stepDecreaseRate;
        if (currStep < stepMin)
        {
            currStep = stepMin;
        }
        currEndZ = currStartZ + chunkLength;
        currFrequency += frequencyIncreaseRate;
        if (currFrequency > frequencyMax)
        {
            currFrequency = frequencyMax;
        }

    }

    private void CrashSound()
    {
        crashSounds.transform.position = player.transform.position;
        crashSounds.SetActive(true);
    }

    #endregion

    #region Scoring Calculations

    // Runs calculations to transform player's Z position into desired measurement
    void UpdateDistance()
    {
        distance = (int) (Mathf.Round(player.transform.position.z) / distanceDivider);
        // Debug.Log(distance + " meters");
        pm.UpdateDistanceText(distance); // Want this here or in Update?
    }

    // Checks to see if the coin sound should be played
    void CheckCoinSound()
    {
        int prevCoins = coins;
        CalculateCoins();
        if (prevCoins < coins)
        {
            if (PlayerPrefsController.GetSoundEffects() == 1)
            {
                coinSource.gameObject.transform.position = player.gameObject.transform.position;
                coinSource.PlayOneShot(coinSound);
            }
        }
    }

    // Calculates currency for player based on deisred distance measurement (not raw player Z)
    void CalculateCoins()
    {
        coins = distance / distancePerCoin;
        // Debug.Log(coins + " coins");
    }

    #endregion

    // Updates player data at the end of the game when the player dies
    void SaveAtEndGame()
    {
        // Save coins and high score
        PlayerData.AddCoins(coins);
        if (distance > PlayerData.GetHighScore())
        {
            PlayerData.SetHighScore(distance);
        }
        SaveSystem.SaveGame();
    }
    
}
