using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    #region Const Keys
    const string COLOR0_RED_KEY = "color0 red";
    const string COLOR0_GREEN_KEY = "color0 green";
    const string COLOR0_BLUE_KEY = "color0 blue";
    const string COLOR1_RED_KEY = "color1 red";
    const string COLOR1_GREEN_KEY = "color1 green";
    const string COLOR1_BLUE_KEY = "color1 blue";
    const string COLOR2_RED_KEY = "color2 red";
    const string COLOR2_GREEN_KEY = "color2 green";
    const string COLOR2_BLUE_KEY = "color2 blue";
    const string COLORFLOOR_RED_KEY = "colorfloor red";
    const string COLORFLOOR_GREEN_KEY = "colorfloor green";
    const string COLORFLOOR_BLUE_KEY = "colorfloor blue";
    const string TOP_DISTANCE_KEY = "top distance";
    const string CURRENT_COINS_KEY = "current coins";
    #endregion


    #region Color0

    // Sets all floats for color 0
    public static void SetColor0(Color color)
    {
        // TODO: Add checks to ensure <= 1?
        PlayerPrefs.SetFloat(COLOR0_RED_KEY, color.r);
        PlayerPrefs.SetFloat(COLOR0_GREEN_KEY, color.g);
        PlayerPrefs.SetFloat(COLOR0_BLUE_KEY, color.b);

    }

    public static float GetColor0Red()
    {
        return PlayerPrefs.GetFloat(COLOR0_RED_KEY);
    }

    public static float GetColor0Green()
    {
        return PlayerPrefs.GetFloat(COLOR0_GREEN_KEY);
    }

    public static float GetColor0Blue()
    {
        return PlayerPrefs.GetFloat(COLOR0_BLUE_KEY);
    }
    
    #endregion

    #region Color1

    // Sets all floats for color 1
    public static void SetColor1(Color color)
    {
        // TODO: Add checks to ensure <= 1?
        PlayerPrefs.SetFloat(COLOR1_RED_KEY, color.r);
        PlayerPrefs.SetFloat(COLOR1_GREEN_KEY, color.g);
        PlayerPrefs.SetFloat(COLOR1_BLUE_KEY, color.b);

    }

    public static float GetColor1Red()
    {
        return PlayerPrefs.GetFloat(COLOR1_RED_KEY);
    }

    public static float GetColor1Green()
    {
        return PlayerPrefs.GetFloat(COLOR1_GREEN_KEY);
    }

    public static float GetColor1Blue()
    {
        return PlayerPrefs.GetFloat(COLOR1_BLUE_KEY);
    }

    #endregion

    #region Color2

    // Sets all floats for color 2
    public static void SetColor2(Color color)
    {
        // TODO: Add checks to ensure <= 1?
        PlayerPrefs.SetFloat(COLOR2_RED_KEY, color.r);
        PlayerPrefs.SetFloat(COLOR2_GREEN_KEY, color.g);
        PlayerPrefs.SetFloat(COLOR2_BLUE_KEY, color.b);

    }

    public static float GetColor2Red()
    {
        return PlayerPrefs.GetFloat(COLOR2_RED_KEY);
    }

    public static float GetColor2Green()
    {
        return PlayerPrefs.GetFloat(COLOR2_GREEN_KEY);
    }

    public static float GetColor2Blue()
    {
        return PlayerPrefs.GetFloat(COLOR2_BLUE_KEY);
    }

    #endregion

    #region ColorFloor

    // Sets all floats for floor color
    public static void SetColorFloor(Color color)
    {
        // TODO: Add checks to ensure <= 1?
        PlayerPrefs.SetFloat(COLORFLOOR_RED_KEY, color.r);
        PlayerPrefs.SetFloat(COLORFLOOR_GREEN_KEY, color.g);
        PlayerPrefs.SetFloat(COLORFLOOR_BLUE_KEY, color.b);

    }

    public static float GetColorFloorRed()
    {
        return PlayerPrefs.GetFloat(COLORFLOOR_RED_KEY);
    }

    public static float GetColorFloorGreen()
    {
        return PlayerPrefs.GetFloat(COLORFLOOR_GREEN_KEY);
    }

    public static float GetColorFloorBlue()
    {
        return PlayerPrefs.GetFloat(COLORFLOOR_BLUE_KEY);
    }
    #endregion

    #region Scoring

    // Sets the top distance
    public static void SetTopDistance(int distance)
    {
        PlayerPrefs.SetInt(TOP_DISTANCE_KEY, distance);
    }

    // Get current top distance
    public static int GetTopDistance()
    {
        PlayerPrefs.GetInt(TOP_DISTANCE_KEY);
    }

    // Sets the current coins number of coins the player possesses
    public static void SetCurrentCoins(int coins)
    {
        PlayerPrefs.SetInt(CURRENT_COINS_KEY, coins);
    }

    // Gets the current number of coins the player possesses
    public static int GetCurrentCoins()
    {
        PlayerPrefs.GetInt(CURRENT_COINS_KEY);
    }

    #endregion
}
