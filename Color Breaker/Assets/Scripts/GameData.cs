using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    /*
        Save data format for the game.
    */

    public float[] color1; // Player primary color
    public float[] color2; // Player secondary color
    public int coins; // Player total coins
    public int highScore; // Player current high score
    public int[] costOfColors; // Cost of each color item


    public GameData()
    {
        color1 = new float[3];
        color1[0] = PlayerData.GetColor1().r;
        color1[1] = PlayerData.GetColor1().g;
        color1[2] = PlayerData.GetColor1().b;

        color2 = new float[3];
        color2[0] = PlayerData.GetColor2().r;
        color2[1] = PlayerData.GetColor2().g;
        color2[2] = PlayerData.GetColor2().b;

        coins = PlayerData.GetCoins();

        highScore = PlayerData.GetHighScore();

        costOfColors = new int[12]; // num of colors -> need to make a const for this
        for(int i = 0; i < costOfColors.Length; ++i)
        {
            costOfColors[i] = PlayerData.GetColorCost(i);
        }

    }

    public void UpdatePlayerData()
    {
        PlayerData.SetColor1(new Color(color1[0], color1[1], color1[2]));
        PlayerData.SetColor2(new Color(color2[0], color2[1], color2[2]));
        PlayerData.SetCoins(coins);
        PlayerData.SetHighScore(highScore);
        PlayerData.SetColorCosts(costOfColors);
        
    }




}
