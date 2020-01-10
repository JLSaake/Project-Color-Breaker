using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blocker : MonoBehaviour
{
    /*
        Class responsible for the game logic and color of each individual blocker.
    */

    #region Color Variables

    [Tooltip("Current color of the player object")]
    public static Color currTransColor; // Controlled in GameManager, the player's current color should be transparent on blockers
    [Tooltip("Previous player color, used to see if player has changed color")]
    public Color prevTransColor; // Previous transparent color, used to see if color needs to be checked
    [Tooltip("The color for this individual blocker duing gameplay")]
    public Color objColor; // Color for the object, set on initialization
    private Color objTransColor; // Transparent version of color for the object

    #endregion

    #region Helper Variables

    [Tooltip("Current z location of the player")]
    public static float playerZ = 0;
    private int zOffset = 20; // How far behind the player a blocker should be before being destroyed
    private ParticleSystem explosionParticles; // Particle system to play if the player collides with this blocker

    #endregion

    // Set explosion particle color to match blockers
    void Start()
    {
        explosionParticles = this.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.MainModule main = explosionParticles.main;
        main.startColor = objColor;
    }

    // Player color and location checking
    void Update()
    {
        // Check to see if player has changed color
        if (prevTransColor != currTransColor)
        {
            StartCoroutine("ColorCheck");
            prevTransColor = currTransColor;
        }
        // Checks to see if the player has cleared the blocker
        if (transform.position.z < playerZ - zOffset)
        {
            Destroy(this.gameObject);
        }
    }

    #region Color Logic

    // Initialize the color of this blocker object
    public void SetColor(Color color)
    {
        objColor = color;
        objColor.a = 1;
        ColorCheck();
    }

    // Updates to match the current player color
    public static void UpdateTransparentColor(Color color)
    {
        currTransColor = color;
        currTransColor.a = 1;
    }

    // Check to see if this blocker matches the current player color
    private IEnumerator ColorCheck()
    {
        if(objColor == currTransColor) // The player is currently the color of this blocker
        {
            this.gameObject.layer = LayerMask.NameToLayer("BlockerTransparent");
        } else // The player is currently not the color of this blocker
        {
            this.gameObject.layer = LayerMask.NameToLayer("BlockerActive");
        }
        yield return new WaitForSeconds(0.0f);
    }

    #endregion

    #region Location and Explosion

    // Updates the current position of the player for all blockers to see
    public static void UpdatePlayerPos(float newPlayerZ)
    {
        playerZ = newPlayerZ;
    }

    public void Explode()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        explosionParticles.Play();
    }

    #endregion
}
