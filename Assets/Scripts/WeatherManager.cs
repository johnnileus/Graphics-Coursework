using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour{

    [SerializeField] private Color rainFogCol;
    [SerializeField] private Color snowFogCol;

    [SerializeField] private Material skyboxDefault;
    [SerializeField] private Material skyboxOvercast;

    [SerializeField] private GameObject SunDirLight;

    [SerializeField] private GameObject SnowGO;
    [SerializeField] private GameObject RainGO;

    [SerializeField] private float fogDensity;

    private ParticleSystem SnowPS;
    private ParticleSystem RainPS;
    
    // Start is called before the first frame update
    void Start(){
        SnowPS = SnowGO.GetComponent<ParticleSystem>();
        RainPS = RainGO.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            print("normal + day/night");
            
            RenderSettings.skybox = skyboxDefault;
            RenderSettings.fogDensity = 0.0004f;
            SunDirLight.SetActive(true);
            
            //disable weather
            SnowGO.SetActive(false);
            RainGO.SetActive(false);
            
        } else if (Input.GetKeyDown(KeyCode.T)) {
            print("snowy");

            RenderSettings.skybox = skyboxOvercast;
            RenderSettings.fogColor = snowFogCol;
            RenderSettings.fogDensity = 0.005f;
            SunDirLight.SetActive(false);
            
            SnowGO.SetActive(true);
            RainGO.SetActive(false);
            

        } else if (Input.GetKeyDown(KeyCode.R)) {
            print("rainy");
            
            RenderSettings.skybox = skyboxOvercast;
            RenderSettings.fogColor = rainFogCol;
            RenderSettings.fogDensity = fogDensity;
            SunDirLight.SetActive(false);
            
            SnowGO.SetActive(false);
            RainGO.SetActive(true);
        }
    }
}
