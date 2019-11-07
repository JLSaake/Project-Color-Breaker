using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{

    public static SkyboxManager Instance;
    public Material[] skyboxes;

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


    public static void UpdateSkybox()
    {
        if (Instance.skyboxes.Length != 0)
        {
            int r = Random.Range(0, Instance.skyboxes.Length);
            RenderSettings.skybox = Instance.skyboxes[r];
        }
    }

}
