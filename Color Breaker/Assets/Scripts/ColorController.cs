using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{

    #region Color Keys

    Color colorWhite = Color.white;
    Color colorBlack = Color.black;
    Color colorRed = Color.red;
    Color colorBlue = Color.blue;
    Color colorGreen = Color.green;

    #endregion

    Color color0 = new Color();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor0(string color)
    {
        switch (color)
        {
            case "White":
                color0 = colorWhite;
                break;
            case "Black":
                color0 = colorBlack;
                break;
            case "Red":
                color0 = colorRed;
                break;
            case "Blue":
                color0 = colorBlue;
                break;
            case "Green":
                color0 = colorGreen;
                break;
        }
        Debug.Log(color0);
    }
}
