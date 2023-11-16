using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour{
    
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject blur;
    [SerializeField] private GameObject plane;

    public float propellerSpinSpeed;
    public float blurSpinSpeed;

    [SerializeField] private GameObject[] controlPoints;
    private int numOfPoints;
    private float sectionSize;
    private float sectionProgress;
    private int currentSection;
    
    private float progress = 0;
    [SerializeField] private float animSpeed;
    
    // Start is called before the first frame update
    void Start() {
        numOfPoints = controlPoints.Length;
        sectionSize = 1 / numOfPoints;
    }

    // Update is called once per frame
    void Update() {

        currentSection = Mathf.FloorToInt(progress / (1-numOfPoints));
        sectionProgress = progress - currentSection * sectionSize;
        
        progress += animSpeed * Time.deltaTime;
        plane.transform.position = Vector3.Slerp(controlPoints[0].transform.position, controlPoints[1].transform.position, progress);
        
        
        
        //animate propeller
        propeller.transform.Rotate(new Vector3(propellerSpinSpeed * Time.deltaTime, 0, 0));
        blur.transform.Rotate(new Vector3(blurSpinSpeed * Time.deltaTime, 0, 0));
    }
}
