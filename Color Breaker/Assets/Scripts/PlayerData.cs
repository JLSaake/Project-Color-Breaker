using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private static Color Color1 = Color.white;
    private static Color Color2 = Color.black;
    private static int Coins = 0;
    private static int HighScore = 0;

    // public static int[] ColorCosts;



    #region Getters and Setters
    public static void SetColor1(Color color)
    {
        Color1 = color;
    }

    public static void SetColor2(Color color)
    {
        Color2 = color;
    }

    public static void SetCoins(int newCoins)
    {
        Coins = newCoins;
    }

    public static void AddCoins(int add)
    {
        Coins += add;
    }

    public static void SetHighScore(int newHighScore)
    {
        HighScore = newHighScore;
    }

    public static Color GetColor1()
    {
        return Color1;
    }

    public static Color GetColor2()
    {
        return Color2;
    }

    public static int GetCoins()
    {
        return Coins;
    }

    public static int GetHighScore()
    {
        return HighScore;
    }

    #endregion

}
