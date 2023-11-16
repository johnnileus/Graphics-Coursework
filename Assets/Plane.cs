using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour{
    
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject blur;

    public float propellerSpinSpeed;
    public float blurSpinSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        propeller.transform.Rotate(new Vector3(propellerSpinSpeed * Time.deltaTime, 0, 0));
        blur.transform.Rotate(new Vector3(blurSpinSpeed * Time.deltaTime, 0, 0));
    }
}
