using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    private MaterialPropertyBlock matBlock;
    private Renderer rend;
    public float speed = 1; // Speed of the player
    // TODO: create function to update speed of player based on difficulty and position in world (GameManager?)
    private bool isAlive = true;


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

        /*
        if (transform.position.z > 4050)
        {
            StartCoroutine("TempRestart");
        } 
        */
    }

    // Changes the player color
    public void UpdateColor(Color color)
    {
        matBlock.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(matBlock);
    }

    void OnCollisionEnter(Collision collision)
    {
        // TODO: set isAlive to false
        StartCoroutine("TempRestart");
    }

    public bool GetPlayerIsAlive()
    {
        return isAlive;
    }

    IEnumerator TempRestart()
    {


        float s = speed;
        transform.position = Vector3.zero;
        speed = 0;
        yield return new WaitForSeconds(5f);
        speed = s;
    }
}
