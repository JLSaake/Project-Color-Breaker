using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public Color[] colors; // Location for all colors in the game

    // Start is called before the first frame update
    void Start()
    {
        _ColorCheck(); // Assert that there are ample colors in the game
        player = GameObject.FindObjectOfType<Player>();
        player.SetColors(colors);
    }

    // Update is called once per frame
    void Update()
    {
        
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
