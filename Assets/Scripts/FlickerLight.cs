using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour{

    [SerializeField] private AnimationCurve intensity;
    [SerializeField] private Light campfireLight;

    public float flickerStrength;
    public float flickerOffset;

    private float curVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curVal = (curVal + 0.1f * Time.deltaTime) % 1f;
        //perlinnoise returns value 0-1 TODO: add another layer of noise
        float val1 = Mathf.PerlinNoise(curVal*40, 1);
        float val2 =  Mathf.PerlinNoise(curVal * 160, 100) * 0.2f;
        campfireLight.intensity = val1 + val2;
    }
}
