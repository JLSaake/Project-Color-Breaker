using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{

    // Rewrite

    // Start
        // Match selected color for color 1 & 2 from player data
        // Update "selected" text

    // Have array with all color "buttons"
    // Update color function (takes color# and which color button)
        // gives where in array to get color from
        // get color from button image in array
        // set color in playerData to set color

    // TODO: use colorblind friendly colors


    public Color[] ColorSelection; // Array of color buttons
    public GameObject[] Buttons1;
    public GameObject[] Buttons2;
    int Color1Index = 0;
    int Color2Index = 1;


    void Start()
    {
        SetButtonColors();
    }

    void SetButtonColors()
    {
        if (ColorSelection.Length < Buttons1.Length || ColorSelection.Length < Buttons2.Length)
        {
            Debug.LogError("Insufficent Colors Specified in Color Selection Menu");
        }
        for(int i = 0; i < Buttons1.Length; ++i)
        {
            Buttons1[i].GetComponent<Image>().color = ColorSelection[i];
        }
        for(int j = 0; j < Buttons2.Length; ++j)
        {
            Buttons2[j].GetComponent<Image>().color = ColorSelection[j];
        }
    }


    public void SetColor1(int ButtonIndex)
    {
        if (ButtonIndex != Color2Index)
        {
            Color1Index = ButtonIndex;
            PlayerData.SetColor1(ColorSelection[ButtonIndex]);
        } else
        {
            // Log some sort of display warning here
        }
        Debug.Log(PlayerData.GetColor1());
    }

    public void SetColor2(int ButtonIndex)
    {
        if (ButtonIndex != Color1Index)
        {
            Color2Index = ButtonIndex;
            PlayerData.SetColor2(ColorSelection[ButtonIndex]);
        } else
        {
            // Log some sort of display warning here
        }
        Debug.Log(PlayerData.GetColor2());
    }


    /*



    #region Color Keys

    Color colorWhite = Color.white;
    Color colorBlack = Color.black;
    Color colorRed = Color.red;
    Color colorBlue = Color.blue;
    Color colorGreen = Color.green;

    #endregion

    public GameObject colorPrimaryButton0;
    public GameObject colorPrimaryButton1;
    public GameObject colorPrimaryButton2;
    public GameObject colorPrimaryButton3;
    public GameObject colorPrimaryButton4;
    public GameObject colorPrimaryButton5;
    public GameObject colorPrimaryButton6;
    public GameObject colorPrimaryButton7;
    public GameObject colorPrimaryButton8;
    public GameObject colorPrimaryButton9;
    public GameObject colorPrimaryButton10;
    public GameObject colorPrimaryButton11;

    private Text primaryButton0Text;

    Color colorPrimary = new Color();


    // Start is called before the first frame update
    void Start()
    {
        primaryButton0Text = colorPrimaryButton0.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColorPrimary(string color)
    {
        switch (color)
        {
            case "White":
                colorPrimary = colorWhite;
                primaryButton0Text.text = "Selected";
                break;
            case "Black":
                colorPrimary = colorBlack;
                break;
            case "Red":
                colorPrimary = colorRed;
                break;
            case "Blue":
                colorPrimary = colorBlue;
                break;
            case "Green":
                colorPrimary = colorGreen;
                break;
        }
        Debug.Log(colorPrimary);
    }


    */
}
