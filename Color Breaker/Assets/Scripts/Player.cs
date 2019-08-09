using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    private MaterialPropertyBlock matBlock;
    private Renderer rend;
    public float speed = 1; // Speed of the player
    // TODO: create function to update speed of player based on difficulty and position in world (GameManager?)


    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        // Move player forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);    
    }

    // Changes the player color
    public void UpdateColor(Color color)
    {
        matBlock.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(matBlock);
    }
}
