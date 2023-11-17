using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour{
    
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject blur;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject originalTerrain;
    [SerializeField] private GameObject craterVFX;

    [SerializeField] private float propellerSpinSpeed;
    [SerializeField] private float blurSpinSpeed;
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float upAndDownSpeed;
    [SerializeField] private float upAndDownStrength;
    [SerializeField] private float fallingRotationSpeed;
    
    [SerializeField] private GameObject[] controlPoints;
    [SerializeField] private GameObject crashCP;
    
    private int numOfPoints;
    private float sectionSize;
    private float sectionProgress;
    private int currentSection;

    private bool crashed;
    
    private float progress = 0;
    [SerializeField] private float animSpeed;
    [SerializeField] private int transitionPoint;
    
    // Start is called before the first frame update
    void Start(){
        numOfPoints = controlPoints.Length;
        sectionSize = 1f / (numOfPoints - 1f);
        
    }

    // Update is called once per frame
    void Update(){
        if (progress < 1) {
            currentSection = Mathf.FloorToInt(progress / sectionSize);
            sectionProgress = (progress - (currentSection * sectionSize)) * (numOfPoints - 1);

            progress += animSpeed * Time.deltaTime;

            plane.transform.position = Vector3.Slerp(controlPoints[currentSection].transform.position,controlPoints[currentSection + 1].transform.position, sectionProgress);
            
            plane.transform.LookAt(controlPoints[currentSection+1].transform.position);
            
            var eulerAngles = plane.transform.eulerAngles;
            if (currentSection < transitionPoint) {
                float zRot = Mathf.Sin(Time.time * wobbleSpeed);
                float yOffset = MathF.Sin(Time.time * upAndDownSpeed);
                plane.transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, zRot*20);
                plane.transform.position += new Vector3(0, yOffset * upAndDownStrength, 0);
            }
            else {
                float zRot = (Time.time * fallingRotationSpeed) % 360;
                eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, zRot*40);
                plane.transform.eulerAngles = eulerAngles;
            }

            //animate propeller
            propeller.transform.Rotate(new Vector3(propellerSpinSpeed * Time.deltaTime, 0, 0));
            blur.transform.Rotate(new Vector3(blurSpinSpeed * Time.deltaTime, 0, 0));
        }
        else if (!crashed){
            plane.transform.position = crashCP.transform.position;
            plane.transform.rotation = crashCP.transform.rotation;
            originalTerrain.SetActive(false);

            crashed = true;
        }
    }
        
}
