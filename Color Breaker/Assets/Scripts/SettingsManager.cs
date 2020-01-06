using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    //public GameObject soundEffectsButton;
    public Text soundEffectsText;
    //public GameObject musicButton;
    public Text musicText;
    //public GameObject creditsButton;
    public GameObject creditsDisplay;
    //public GameObject creditsBackButton; // return from credits display back to settings

    // Start is called before the first frame update
    void Start()
    {
        SetSoundEffectsText();
        SetMusicText();
    }


    public void ToggleSoundEffects()
    {
        int newSound = PlayerPrefsController.GetSoundEffects() == 1 ? 0 : 1;
        PlayerPrefsController.SetSoundEffects(newSound);
        SetSoundEffectsText();
    }

    private void SetSoundEffectsText()
    {
        int currSound = PlayerPrefsController.GetSoundEffects();
        if (currSound == 1)
        {
            soundEffectsText.text = "SOUND ON ";
        } else
        if (currSound == 0)
        {
            soundEffectsText.text = "SOUND OFF";
        }
    }

    public void ToggleMusic()
    {
        int newMusic = PlayerPrefsController.GetMusic() == 1 ? 0 : 1;
        PlayerPrefsController.SetMusic(newMusic);
        SetMusicText();
    }

    private void SetMusicText()
    {
        int currMusic = PlayerPrefsController.GetMusic();
        if (currMusic == 1)
        {
            musicText.text = "MUSIC ON ";
        } else
        if (currMusic == 0)
        {
            musicText.text = "MUSIC OFF";
        }
    }



}
