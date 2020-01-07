using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    #region Sound Effects and Music
    // Both sound and music set to int representing bool (1: on / 0: off)
    const string SOUND_EFFECTS_KEY = "sound effects";
    const string MUSIC_KEY = "music";

    // Sound Effects
    public static void SetSoundEffects(int toggle)
    {
        PlayerPrefs.SetInt(SOUND_EFFECTS_KEY, toggle);
    }

    public static int GetSoundEffects()
    {
        return PlayerPrefs.GetInt(SOUND_EFFECTS_KEY, 1); // should always be either 1 or 0
    }

    // Music
    public static void SetMusic(int toggle)
    {
        PlayerPrefs.SetInt(MUSIC_KEY, toggle);
    }

    public static int GetMusic()
    {
        return PlayerPrefs.GetInt(MUSIC_KEY, 1); // should always be either 1 or 0
    }

    #endregion

    #region Ads

    const string DISTANCE_SINCE_AD_KEY = "distance since ad";

    public static void SetDistanceSinceAd(int distance)
    {
        PlayerPrefs.SetInt(DISTANCE_SINCE_AD_KEY, distance);
    }

    public static int GetDistanceSinceAd()
    {
        return PlayerPrefs.GetInt(DISTANCE_SINCE_AD_KEY, 0); // if unknown, default to 0
    }

    #endregion
}
