﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blocker : MonoBehaviour
{

    public static Color currTransColor; // Controlled in GameManager, current color of player
    public static float playerZ = 0;
    private int zOffset = 20;
    public Color prevTransColor; // Previous transparent color, used to see if color needs to be checked
    public Color objColor; // Color for the object, set on initialization
    private Color objTransColor; // Transparent version of color for the object


    // Update is called once per frame
    void Update()
    {
        // Check to see if player has changed color
        if (prevTransColor != currTransColor)
        {
            ColorCheck();
            prevTransColor = currTransColor;
        }
        // Checks to see if the player has cleared the blocker
        if (transform.position.z < playerZ - zOffset)
        {
            Destroy(this.gameObject);
        }
    }

    // Updates to match the current player color
    public static void UpdateTransparentColor(Color color)
    {
        currTransColor = color;
        currTransColor.a = 1;
    }

    // Updates the current position of the player for all blockers to see
    public static void UpdatePlayerPos(float newPlayerZ)
    {
        playerZ = newPlayerZ;
    }

    // Initialize the color of this blocker object
    public void SetColor(Color color)
    {
        objColor = color;
        objColor.a = 1;
        ColorCheck();
    }

    // Check to see if this blocker matches the current player color
    private void ColorCheck()
    {
        if(objColor == currTransColor) // The player is currently the color of this blocker
        {
            this.gameObject.layer = LayerMask.NameToLayer("BlockerTransparent");
        } else // The player is currently not the color of this blocker
        {
            this.gameObject.layer = LayerMask.NameToLayer("BlockerActive");
        }
    }
}
