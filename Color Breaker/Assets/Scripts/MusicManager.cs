using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [Tooltip("Music to loop during gameplay")]
    public AudioClip loopingMusic;
    private AudioSource musicAudio;
    private bool musicOn;

    // Start is called before the first frame update
    void Start()
    {
        musicAudio = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefsController.GetMusic() == 1)
        {
            musicOn = true;
            PlayMusic();
        } else
        {
            musicOn = false;
        }
    }

    // Starts music clip
    private void PlayMusic()
    {
        musicAudio.loop = true;
        musicAudio.clip = loopingMusic;
        musicAudio.Play();
    }

    // Pauses music upon opening game pause menu
    public void PauseMusic()
    {
        musicAudio.Pause();
    }

    // Unpauses or stops music when returning to game from pause menu
    public void UnPauseMusic()
    {
        if (PlayerPrefsController.GetMusic() == 1)
        {
            if (!musicOn) // music turned on from pause menu
            {
                PlayMusic();
                musicOn = true;
            } else // music was playing before pause menu
            {
                musicAudio.UnPause();
            }
        } else // music turned off from pause menu
        {
            if (musicOn) // the music was playing before the pause menu
            {
                musicAudio.Stop();
                musicOn = false;
            }
        }
    }
}
