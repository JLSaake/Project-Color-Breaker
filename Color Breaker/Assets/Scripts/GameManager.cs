using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TEMP
    public Blocker blocker;

    public Player player;
    public Color[] colors; // Location for all colors in the game
    private int currIndex = 0; // Current color index

    public Camera mainCam;

    Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        _ColorCheck(); // Assert that there are ample colors in the game
        player = GameObject.FindObjectOfType<Player>();
        player.UpdateColor(colors[currIndex]);

//        player.SetColors(colors); // TODO: move exculusively out of Player
        mainCam = Camera.main;
        camOffset = player.transform.position - mainCam.transform.position;
        

        // TEMP - all hard coded for now
        blocker.SetColor(colors[1], 0.5f);
        Blocker.UpdateTransparentColor(colors[currIndex]);

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
            colors = new Color[2];
            colors[0] = Color.white;
            colors[1] = Color.black;
        }
    }
}
