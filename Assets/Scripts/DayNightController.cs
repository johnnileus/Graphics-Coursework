using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class DayNightController : MonoBehaviour{
    [SerializeField] private float currentVal = 0.95f; //time of day (0-1)
    [SerializeField] private bool cycleTime = true;
    
    public Gradient fogColour; //fog colour over time
    public AnimationCurve dirLightStrength; //strength of sun over time

    private float defaultYRot;
    private float defaultZRot;

    private Light dirLight;
    private LensFlareComponentSRP sunFlare;
    
    // Start is called before the first frame update
    void Start(){
        defaultYRot = transform.eulerAngles.y;
        defaultZRot = transform.eulerAngles.z;
        dirLight = transform.GetComponent<Light>();
        sunFlare = transform.GetComponent<LensFlareComponentSRP>();
    }

    // Update is called once per frame
    void Update(){
        if (cycleTime) {
            currentVal = (currentVal + 0.01f * Time.deltaTime) % 1f;
        }
        transform.eulerAngles = new Vector3(currentVal*360,defaultYRot, defaultZRot);
        
        //changes values based on time of day
        //turn sun off
        dirLight.intensity = dirLightStrength.Evaluate(currentVal);
        //change fog colour
        RenderSettings.fogColor = fogColour.Evaluate(currentVal);
        //change skybox strength
        RenderSettings.ambientIntensity = dirLightStrength.Evaluate(currentVal);
        //change sunflare strength (issue of sunflare appearing off screen at night)
        sunFlare.intensity = dirLightStrength.Evaluate(currentVal);
    }  
}
