using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Color[] colors; // Handled in the game manager
    private int colorCount;
    private int currIndex = 0;
    MaterialPropertyBlock matBlock;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            ++currIndex;
            if (currIndex >= colorCount)
            {
                currIndex = 0;
            }
            matBlock.SetColor("_BaseColor", colors[currIndex]);
            rend.SetPropertyBlock(matBlock);
        }
        
    }

    public void SetColors(Color[] inputColors)
    {
        colors = inputColors;
        colorCount = inputColors.Length;
        matBlock.SetColor("_BaseColor", colors[currIndex]);
        rend.SetPropertyBlock(matBlock);
    }
}
