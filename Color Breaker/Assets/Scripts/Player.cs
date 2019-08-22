using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    private MaterialPropertyBlock matBlock;
    private Renderer rend;
    public float speed = 100; // Speed of the player
    private bool isAlive = true;
    private bool isStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted) // Level has begun
        {
            // Move player forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime); 
        }
    }

    // Changes the player color
    public void UpdateColor(Color color)
    {
        matBlock.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(matBlock);
    }

    void OnCollisionEnter(Collision collision)
    {
        isAlive = false;
    }

    public bool GetPlayerIsAlive()
    {
        return isAlive;
    }

    public bool GetPlayerStarted()
    {
        return isStarted;
    }

    public void PlayerStart()
    {
        isStarted = true;
    }
}
