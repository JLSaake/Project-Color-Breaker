using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [Tooltip("Speed of the player in the Z direction")]
    public float speed = 100; // Speed of the player
    private bool isAlive = true; // Is the player currently alive
    private bool isStarted = false; // Has the player initiated gameplay
    private Renderer rend;
    private MaterialPropertyBlock matBlock;



    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStarted) // Level has begun
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
    }

    // Collision with a blocker
    void OnCollisionEnter(Collision collision)
    {
        isAlive = false;
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
