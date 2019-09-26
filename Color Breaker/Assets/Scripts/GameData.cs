using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public float[] Color1;
    public float[] Color2;
    public int Coins;
    public int HighScore;


    public GameData()
    {
        Color1 = new float[3];
        Color1[0] = PlayerData.GetColor1().r;
        Color1[1] = PlayerData.GetColor1().g;
        Color1[2] = PlayerData.GetColor1().b;

        Color2 = new float[3];
        Color2[0] = PlayerData.GetColor2().r;
        Color2[1] = PlayerData.GetColor2().g;
        Color2[2] = PlayerData.GetColor2().b;

        Coins = PlayerData.GetCoins();

        HighScore = PlayerData.GetHighScore();

    }

    public void UpdatePlayerData()
    {
        PlayerData.SetColor1(new Color(Color1[0], Color1[1], Color1[2]));
        PlayerData.SetColor2(new Color(Color2[0], Color2[1], Color2[2]));
        PlayerData.SetCoins(Coins);
        PlayerData.SetHighScore(HighScore);
    }




}
