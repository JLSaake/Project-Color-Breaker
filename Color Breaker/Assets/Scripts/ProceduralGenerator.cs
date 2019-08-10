using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{

    public Blocker blocker; // Blocker prefab to generate for level
    private Material[] materials; // Sent from GameManager, the materials to choose from for blockers


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generates next portion of the level
    // (endZ - startZ) % step == 0
    // frequency should be in range [0-1] (inclusive)
    public void GenerateChunk(int startZ, int endZ, int step, float frequency)
    {
        float currFrequency = frequency; // Option to make it more likely to spawn after failed spawns
        int currZ = startZ;

        do
        {
            if (SpawnCheck(currFrequency)) // If a blocker is going to be spawned
            {
                SpawnBlocker(currZ);

            }
            currZ += step;
        }   while (currZ <= endZ);
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
    private void SpawnBlocker(int currZ)
    {
        Blocker nb = Instantiate(blocker, new Vector3(0, 0, currZ), Quaternion.identity);
        // TODO: give material
        int m = Random.Range(0, materials.Length);
        nb.gameObject.GetComponent<Renderer>().material = materials[m];
        nb.SetColor(materials[m].GetColor("_BaseColor"));
        
    }


    public void SetMaterials(Material[] mats)
    {
        materials = mats;
    }
}
