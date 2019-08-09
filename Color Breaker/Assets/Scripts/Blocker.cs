using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blocker : MonoBehaviour
{

    private static Color currTransColor; // Controlled in GameManager, current color of player
    private Color prevTransColor; // Previous transparent color, used to see if color needs to be checked
    private Color objColor; // Color for the object, set on initialization
    private Color objTransColor; // Transparent version of color for the object


    // TODO: have blocker change material / track all materials in GameManager
    private MaterialPropertyBlock matBlock;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if player has changed color (will be called after setting color)
        if (prevTransColor != currTransColor)
        {
            ColorCheck();
            prevTransColor = currTransColor;
        }
    }

    // Updates to match the current player color
    public static void UpdateTransparentColor(Color color)
    {
        currTransColor = color;
    }

    // Initialize the color of this blocker object
    public void SetColor(Color color, float alphaValue)
    {
        objColor = color;
        objTransColor = color;
        objTransColor.a = alphaValue;
    }

    // Check to see if this blocker matches the current player color
    void ColorCheck()
    {
        if(objColor == currTransColor) // The player is currently the color of this blocker
        {
            this.gameObject.layer = LayerMask.NameToLayer("BlockerTransparent");
            matBlock.SetColor("_BaseColor", objTransColor);
            rend.SetPropertyBlock(matBlock);
        } else // The player is currently not the color of this blocker
        {
            this.gameObject.layer = LayerMask.NameToLayer("BlockerActive");
            matBlock.SetColor("_BaseColor", objColor);
            rend.SetPropertyBlock(matBlock);
        }
    }
}
