using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    /*
        Public interface for scripts to access game data.
        Temporary storage for game data, variables set
        when game data is updated.
    */

    private static Color color1 = Color.blue; // Current player primary color, defaults if none found
    private static Color color2 = Color.red; // Current player secondary color, defaults if none found
    private static int coins = 0; // Current player coin total
    private static int highScore = 0; // Current high score
    public static int[] colorCosts; // Cost of each color item, first 2 should be 0 -> controlled in MainMenuManager
    

    #region Getters and Setters
    public static void SetColor1(Color color)
    {
        color1 = color;
    }

    public static void SetColor2(Color color)
    {
        color2 = color;
    }

    public static void SetCoins(int newCoins)
    {
        coins = newCoins;
    }

    public static void AddCoins(int add)
    {
        coins += add;
    }

    public static void SetHighScore(int newHighScore)
    {
        highScore = newHighScore;
    }

    public static Color GetColor1()
    {
        return color1;
    }

    public static Color GetColor2()
    {
        return color2;
    }

    public static int GetCoins()
    {
        return coins;
    }

    public static int GetHighScore()
    {
        return highScore;
    }

    public static int GetColorCost(int index)
    {
        return colorCosts[index];
    }

    public static void PurchaseColor(int index)
    {
        colorCosts[index] = 0;
    }


    public static void SetColorCosts(int[] costs)
    {
        colorCosts = new int[costs.Length];
        for (int i = 0; i < costs.Length; ++i)
        {
            colorCosts[i] = costs[i];
        }
    }
    #endregion

}
