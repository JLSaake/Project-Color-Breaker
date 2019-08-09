using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{

    public static Color currTransColor; // Controlled in GameManager, current color of player
    private Color prevTransColor; // Previous transparent color, used for checking
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
        // Check to see if player has changed color
        if (prevTransColor != currTransColor)
        {
            ColorCheck();
            prevTransColor = currTransColor;
        }
    }

    public static void UpdateTransparentColor(Color color)
    {
        currTransColor = color;
    }

    public void SetColor(Color color, float alphaValue)
    {
        objColor = color;
        objTransColor = color;
        objTransColor.a = alphaValue;
    }

    void ColorCheck()
    {
        if(objColor == currTransColor)
        {
            // TODO: add transparency
            this.gameObject.layer = LayerMask.NameToLayer("BlockerTransparent");
            matBlock.SetColor("_BaseColor", objTransColor);
            rend.SetPropertyBlock(matBlock);
        } else
        {
            // TODO: reset to non-transparent color
            this.gameObject.layer = LayerMask.NameToLayer("BlockerActive");
            matBlock.SetColor("_BaseColor", objColor);
            rend.SetPropertyBlock(matBlock);
        }
    }
}
