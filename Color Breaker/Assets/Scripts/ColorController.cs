using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{

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
}
