using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DayNightController : MonoBehaviour{
    [SerializeField] private float currentVal = 0;
    
    public Gradient fogColour;

    private float defaultYRot;
    private float defaultZRot;
    
    // Start is called before the first frame update
    void Start(){
        defaultYRot = transform.eulerAngles.y;
        defaultZRot = transform.eulerAngles.z;
        print(defaultYRot);
    }

    // Update is called once per frame
    void Update(){
        currentVal = (currentVal + 0.01f * Time.deltaTime) % 1f;
        transform.eulerAngles = new Vector3(currentVal*360,defaultYRot, defaultZRot);
        RenderSettings.fogColor = fogColour.Evaluate(currentVal);
    }  
}
