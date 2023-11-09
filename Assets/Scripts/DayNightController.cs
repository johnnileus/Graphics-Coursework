using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DayNightController : MonoBehaviour{
    [SerializeField] private float currentVal = 0;
    
    public Gradient fogColour;
    
    // Start is called before the first frame update
    void Start(){
 
    }

    // Update is called once per frame
    void Update(){
        currentVal = (currentVal + 0.01f * Time.deltaTime) % 1f;
        transform.rotation = Quaternion.Euler(currentVal*360,60, 60);
        RenderSettings.fogColor = fogColour.Evaluate(currentVal);
    }  
}
