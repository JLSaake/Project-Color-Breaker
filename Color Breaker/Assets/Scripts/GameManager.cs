using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TEMP
    public Blocker blocker;

    private Player player; // Player object
    private Camera mainCam; // Main camera that follows player
    private Vector3 camOffset; // Offset of the camera from the player
    public Color[] colors; // Location for all colors in the game
    private int currIndex = 0; // Current color selected

    // Start is called before the first frame update
    void Start()
    {
        _ColorCheck(); // Assert that there are enough colors in the game
        player = GameObject.FindObjectOfType<Player>();
        player.UpdateColor(colors[currIndex]);

        mainCam = Camera.main;
        camOffset = player.transform.position - mainCam.transform.position;
        

        // TEMP - all hard coded for now, and for one variable
        blocker.SetColor(colors[1], 0.5f);
        Blocker.UpdateTransparentColor(colors[currIndex]);

        // TODO: add first blocker, alwyas controlled by GameManager, to be second color

    }

    // Update is called once per frame
    void Update()
    {
        // Camera follow player
        mainCam.transform.position = player.transform.position - camOffset;

        // Input to change color
        if (Input.GetKeyDown(KeyCode.K))
        {
            ++currIndex;
            if (currIndex >= colors.Length)
            {
                currIndex = 0;
            }
            player.UpdateColor(colors[currIndex]);
            Blocker.UpdateTransparentColor(colors[currIndex]);

        }
    }

    // Security check to ensure that there are enough colors implemented into the game
    private void _ColorCheck()
    {
        if (colors.Length < 1) // TODO: change to 2 to prevent infinite gameplay
        {
            Debug.LogWarning("ColorWarning: Insufficient colors set on instantiation of GameManager. Switching to defaults.");
            colors = new Color[2];
            colors[0] = Color.white;
            colors[1] = Color.black;
        }
    }
}
