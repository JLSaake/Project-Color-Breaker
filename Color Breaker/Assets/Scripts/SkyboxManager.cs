using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    /*
        Manager for displaying random skybox
        at the start of a scene.
        Does not destroy on load.
    */

    [Tooltip("Current instance of the skybox manager")]
    public static SkyboxManager Instance;
    [Tooltip("Skyboxes that can be chosen from to display")]
    public Material[] skyboxes;

    // Ensure only one manager exists in the current scene
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Skybox");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    // Choose random skybox to display
    public static void UpdateSkybox()
    {
        if (Instance.skyboxes.Length != 0)
        {
            int r = Random.Range(0, Instance.skyboxes.Length);
            RenderSettings.skybox = Instance.skyboxes[r];
        }
    }

}
