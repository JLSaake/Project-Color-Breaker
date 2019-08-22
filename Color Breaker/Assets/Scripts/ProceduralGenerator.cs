using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{

    
    /*
    EDITOR FUNCTION KEYS

    [Range(min, max)]
    [Tooltip("tooltip text")]
    [Space(pixels)]
    [RequireComponent(typeof(type))]
    [Header("header text")]
    
    */

    public Blocker blocker; // Blocker prefab to generate for level
    public GameObject floor; // floor for game (set scale to chunk length)
    private Material[] materials; // Sent from GameManager, the materials to choose from for blockers
    private int consecutiveColor = 0;
    private int colorIndex = 0;


    // Generates next portion of the level
    public void GenerateChunk(int startZ, int endZ, int step, float frequency, int maxConsecutive)
    {
        int currZ = startZ;
        currZ += step;

        // Spawn new floor
        GameObject newFloor = Instantiate(floor);
        newFloor.transform.localScale = new Vector3(newFloor.transform.localScale.x, newFloor.transform.localScale.y, endZ - startZ);
        newFloor.transform.position = new Vector3(newFloor.transform.position.x, newFloor.transform.position.y, (endZ + startZ)/2);

        // Run through blocker spawning
        do
        {
            if (SpawnCheck(frequency)) // If a blocker is going to be spawned
            {
                SpawnBlocker(currZ, maxConsecutive);

            }
            currZ += step;
        }   while (currZ + step <= endZ);
    }

    // Checks to see if a blocker should be spawned, given frequency
    private bool SpawnCheck(float freq)
    {
        if (Random.Range(0.0f, 1.0f) <= freq)
        {
            return true;
        }
        return false;
    }

    // Spawn a blocker at the position
    private void SpawnBlocker(int currZ, int colorMax)
    {
        Blocker nb = Instantiate(blocker, new Vector3(0, 0, currZ), Quaternion.identity);
        // TODO: give material (unknown TODO)
        int m = Random.Range(0, materials.Length);
        if (m == colorIndex)
        {
            ++consecutiveColor;
            if (consecutiveColor > colorMax)
            {
                ++m;
                if (m >= materials.Length)
                {
                    m = 0;
                }
                consecutiveColor = 0;
            }
        }
        colorIndex = m;
        nb.gameObject.GetComponent<Renderer>().material = materials[m];
        nb.SetColor(materials[m].GetColor("_BaseColor"));
        
    }

    // Helper function for passing materials from GameManager
    public void SetMaterials(Material[] mats)
    {
        materials = mats;
    }
}
