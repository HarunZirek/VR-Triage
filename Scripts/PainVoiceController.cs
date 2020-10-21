using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainVoiceController : MonoBehaviour
{
    AudioSource audioSrc;

    public float freq;
    public float currentFreq;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        currentFreq += Time.deltaTime;
        
        if(currentFreq > freq)
        {
            audioSrc.PlayOneShot(audioSrc.clip);
            currentFreq = 0;
        }
    }
}
