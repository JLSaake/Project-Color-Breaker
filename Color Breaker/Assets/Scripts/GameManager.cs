using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TEMP
    public Blocker blocker;

    #region BlockerMaterials
    // Materials that will be used for procedurally generating blockers
    public Material material0;
    public Material material1;
    public Material material2;
    public Material material3;

    #endregion

    private ProceduralGenerator pg; // Handles generation of blockers
    private Player player; // Player object
    private Camera mainCam; // Main camera that follows player
    private Vector3 camOffset; // Offset of the camera from the player
    public Color[] colors; // Location for all colors in the game
    private int currIndex = 0; // Current color selected
    private Color[] colorsAlpha; // Array created from colors (above) that have transparency values (able to be passed through)
    public float transparency = 0.5f; // Transparency value for materials

    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        _ColorCheck(); // Assert that there are enough colors in the game
        player = GameObject.FindObjectOfType<Player>();
        player.UpdateColor(colors[currIndex]);

        mainCam = Camera.main;
        camOffset = player.transform.position - mainCam.transform.position;

        pg = GameObject.FindObjectOfType<ProceduralGenerator>();
        

        // Handle color changing of materials
        materials = new Material[colors.Length];
        material0.SetColor("_BaseColor", colors[0]);
        materials[0] = material0;
        material1.SetColor("_BaseColor", colors[1]);
        materials[1] = material1;
        if (colors.Length >= 3)
        {
            material2.SetColor("_BaseColor", colors[2]);
            materials[2] = material2;
            if (colors.Length == 4)
            {
                material3.SetColor("_BaseColor", colors[3]);
                materials[3] = material3;
                if (colors.Length > 4)
                {
                    Debug.LogWarning("ColorOveruseWarning: GameManager is not set up to use more than 4 colors in a round.");
                }
            }
        }

        pg.SetMaterials(materials);

        // Establish an array of transparent colors for changing materials
        colorsAlpha = new Color[colors.Length];
        for (int i = 0; i < colorsAlpha.Length; ++i)
        {
            colorsAlpha[i].r = colors[i].r;
            colorsAlpha[i].g = colors[i].g;
            colorsAlpha[i].b = colors[i].b;
            colorsAlpha[i].a = transparency;
        }
        materials[currIndex].SetColor("_BaseColor", colorsAlpha[currIndex]);


        // TEMP - all hard coded for now, and for one variable
        blocker.SetColor(colors[1]);
        Blocker.UpdateTransparentColor(colors[currIndex]);

        // TODO: add first blocker, alwyas controlled by GameManager, to be second color

        // TEMP - Testing ProceduralGenerator
        pg.GenerateChunk(80, 2000, 15, 0.45f);

    }

    // Update is called once per frame
    void Update()
    {
        // Camera follow player
        mainCam.transform.position = player.transform.position - camOffset;

        // Input to change color
        if (Input.GetKeyDown(KeyCode.Space))
        {
            materials[currIndex].SetColor("_BaseColor", colors[currIndex]);
            ++currIndex;
            if (currIndex >= colors.Length)
            {
                currIndex = 0;
            }
            materials[currIndex].SetColor("_BaseColor", colorsAlpha[currIndex]);
            player.UpdateColor(colors[currIndex]);
            Blocker.UpdateTransparentColor(colors[currIndex]);

        }
    }

    // Security check to ensure that there are enough colors implemented into the game
    private void _ColorCheck()
    {
        if (colors.Length < 1) // TODO: change to 2 to prevent infinite gameplay
        {
            Debug.LogWarning("ColorWarning: Insufficient colors set on instantiation of GameManager. Switching to defaults.");
            colors = new Color[2];
            colors[0] = Color.white;
            colors[1] = Color.black;
        }
    }
}
