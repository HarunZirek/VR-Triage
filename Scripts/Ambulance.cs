using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : MonoBehaviour
{
    public Light red, blue;

    public float freq;
    float currentFreq;
    void Update()
    {
        currentFreq += Time.deltaTime;

        if(currentFreq > freq)
        {
            currentFreq = 0f;
            if(red.enabled)
            {
                red.enabled = false;
                blue.enabled = true;
            }
            else
            {
                red.enabled = true;
                blue.enabled = false;
            }
        }
    }
}
