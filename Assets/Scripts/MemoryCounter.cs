using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script derived from SC_FPSCounter.cs
public class MemoryCounter : MonoBehaviour
{

    public float updateInterval = 0.1f; //How often should the number update

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float usedMemoryMB;

    GUIStyle textStyle = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        timeleft = updateInterval;

        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float usedMemory = System.GC.GetTotalMemory(false); //true creates lag spike
            usedMemoryMB = usedMemory / (1024f * 1024f);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        //Display the fps and round to 2 decimals
        GUI.Label(new Rect(5, 30, 100, 25), usedMemoryMB.ToString("F2") + "MB", textStyle);
    }
}