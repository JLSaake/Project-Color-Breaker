using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    /*
        Manager responsible for controling the gameplay music.
    */

    [Tooltip("Music to loop during gameplay")]
    public AudioClip loopingMusic;
    private AudioSource musicAudio; // Audio Source on the manager to play the looping music clip
    private bool musicOn; // Does the player want the music on

    // Find audio source and get player music preferences
    void Start()
    {
        musicAudio = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefsController.GetMusic() == 1)
        {
            musicOn = true;
        } else
        {
            musicOn = false;
        }
    }

    #region Play Music Logic

    // Starts music clip
    public void PlayMusic()
    {
        if (musicOn)
        {
            musicAudio.loop = true;
            musicAudio.clip = loopingMusic;
            musicAudio.Play();
        }
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
                musicOn = true;
                PlayMusic();
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

    #endregion
}
