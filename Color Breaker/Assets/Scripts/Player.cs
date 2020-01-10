using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    /*
        Player gameobject that moves throughout the gameplay scene
    */

    #region Public variables

    [Tooltip("Speed of the player in the Z direction")]
    public float speed = 120; // Speed of the player
    [Tooltip("Sound to play when the player switches colors")]
    public AudioClip switchClip;
    [Tooltip("Sound to play when player strikes a blocker")]
    public AudioClip crashSound;

    #endregion

    #region Helper Variables and Components

    private bool isAlive = true; // Is the player currently alive
    private bool isStarted = false; // Has the player initiated gameplay
    private Renderer rend; // Material renderer
    private MaterialPropertyBlock matBlock; // Material for setting the player's color
    private ParticleSystem explosionParticles; // Particles when the player strikes a blocker
    private AudioSource switchAudio; // Audio source to play switch color audio

    #endregion




    // Find and initialize components
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
        explosionParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        switchAudio = gameObject.GetComponent<AudioSource>();
    }

    // Progress the player forward
    void FixedUpdate()
    {
        if (isStarted && isAlive) // Level has begun
        {
            // Move player forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime); 
        }
    }
    
    #region Gameplay Functions

    // Start the game
    public void PlayerStart()
    {
        isStarted = true;
    }

    // Changes the player color
    public void UpdateColor(Color color)
    {
        matBlock.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(matBlock);
        PlaySwitchSound();
    }

    // Plays sound when changing color
    private void PlaySwitchSound()
    {
        if (PlayerPrefsController.GetSoundEffects() == 1)
        {
            switchAudio.PlayOneShot(switchClip);
        }
    }

    // Collision with a blocker
    void OnCollisionEnter(Collision collision)
    {
        isAlive = false;
        Blocker blocker = collision.gameObject.GetComponent<Blocker>();
        blocker.Explode();
        Explode();
    }

    // Effects when the player strikes an obstacle
    private void Explode()
    {
        if (PlayerPrefsController.GetSoundEffects() == 1) 
        {
            switchAudio.PlayOneShot(crashSound);
        }
        ParticleColor();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        explosionParticles.Play();
    }

    // Set color for player explosion particles
    private void ParticleColor()
    {
        ParticleSystem.MainModule main = explosionParticles.main;
        main.startColor = matBlock.GetColor("_BaseColor");
    }

    #endregion

    #region Getters

    // Get if the player is alive
    public bool GetPlayerIsAlive()
    {
        return isAlive;
    }

    // Get if the game has started
    public bool GetPlayerStarted()
    {
        return isStarted;
    }

    #endregion
}
