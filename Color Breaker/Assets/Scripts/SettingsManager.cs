using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    /*
        Handles settings menu on Main Menu scene.
        Toggles sound and music text and icons.
    */

    [Header("Sound Effects")]
    [Tooltip("Text displaying sound effect status")]
    public Text soundEffectsText;
    [Tooltip("Image displaying sound effect status")]
    public Image soundEffectsImage;
    [Tooltip("Sprite to dispaly when sound effects are on")]
    public Sprite soundEffectsImageOn;
    [Tooltip("Sprite to display when sound effects are off")]
    public Sprite soundEffectsImageOff;

    [Header("Music")]
    [Tooltip("Text displaying music status")]
    public Text musicText;
    [Tooltip("Image displaying music status")]
    public Image musicImage;
    [Tooltip("Sprite to display when music is on")]
    public Sprite musicImageOn;
    [Tooltip("Sprite to display when music is off")]
    public Sprite musicImageOff;

    // Set sound effects and music objects to correct state
    void Start()
    {
        SetSoundEffectsText();
        SetMusicText();
    }

    #region Sound Effects

    // Swap sound effects current state
    public void ToggleSoundEffects()
    {
        int newSound = PlayerPrefsController.GetSoundEffects() == 1 ? 0 : 1;
        PlayerPrefsController.SetSoundEffects(newSound);
        SetSoundEffectsText();
    }

    // Set the text and icon for sound effects
    private void SetSoundEffectsText()
    {
        int currSound = PlayerPrefsController.GetSoundEffects();
        if (currSound == 1)
        {
            soundEffectsText.text = "SOUND ON ";
            soundEffectsImage.sprite = soundEffectsImageOn;
        } else
        if (currSound == 0)
        {
            soundEffectsText.text = "SOUND OFF";
            soundEffectsImage.sprite = soundEffectsImageOff;
        }
    }

    #endregion

    #region Music

    // Swap music current state
    public void ToggleMusic()
    {
        int newMusic = PlayerPrefsController.GetMusic() == 1 ? 0 : 1;
        PlayerPrefsController.SetMusic(newMusic);
        SetMusicText();
    }

    // Set the text and icon for music
    private void SetMusicText()
    {
        int currMusic = PlayerPrefsController.GetMusic();
        if (currMusic == 1)
        {
            musicText.text = "MUSIC ON ";
            musicImage.sprite = musicImageOn;
        } else
        if (currMusic == 0)
        {
            musicText.text = "MUSIC OFF";
            musicImage.sprite = musicImageOff;
        }
    }

    #endregion
}
