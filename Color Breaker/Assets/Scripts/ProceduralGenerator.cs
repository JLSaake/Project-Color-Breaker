using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{

    [Tooltip("Blocker prefab for generating obstacles")]
    public Blocker blocker; // Blocker prefab to generate for level
    [Tooltip("Floor prefab for generating chunks")]
    public GameObject floor; // floor for game (set scale to chunk length)
    private Material[] materials; // Sent from GameManager, the materials to choose from for blockers
    private int consecutiveColor = 0; // Counter for cosecutive colors generated
    private int colorIndex = 0; // Index of most recently generated color

    #region private generation variables

    int p_startZ;
    int p_endZ;
    int p_step;
    float p_frequency;
    int p_maxConsecutive;

    #endregion



    // Sets generation variables and calls coroutine
    public void GenerateChunk(int startZ, int endZ, int step, float frequency, int maxConsecutive)
    {
        p_startZ = startZ;
        p_endZ = endZ;
        p_step = step;
        p_frequency = frequency;
        p_maxConsecutive = maxConsecutive;

        StartCoroutine("GenerateChunkCoroutine");
    }

    private IEnumerator GenerateChunkCoroutine()
    {
        int currZ = p_startZ;
        currZ += p_step;

        // Spawn new floor
        GameObject newFloor = Instantiate(floor);
        newFloor.transform.localScale = new Vector3(newFloor.transform.localScale.x, newFloor.transform.localScale.y, p_endZ - p_startZ);
        newFloor.transform.position = new Vector3(newFloor.transform.position.x, newFloor.transform.position.y, (p_endZ + p_startZ)/2);

        // Run through blocker spawning
        do
        {
            if (SpawnCheck(p_frequency)) // If a blocker is going to be spawned
            {
                SpawnBlocker(currZ, p_maxConsecutive);

            }
            currZ += p_step;
        }   while (currZ + p_step <= p_endZ);
        yield return new WaitForSeconds(0.0f);
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
        if (m == colorIndex) // The color is a consecutive color
        {
            ++consecutiveColor;
            if (consecutiveColor > colorMax) // The limit for consecutive color has been reached
            {
                ++m;
                if (m >= materials.Length) // Array wrap around
                {
                    m = 0;
                }
                consecutiveColor = 1; // New blocker is first instance of new consecutive color count
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
