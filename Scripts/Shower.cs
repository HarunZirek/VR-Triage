using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour
{
    [SerializeField] ParticleSystem showerEffect;
    [SerializeField] AudioSource showerSound;

    private void OnTriggerEnter(Collider other)
    {
        showerEffect.Play();
        showerSound.PlayOneShot(showerSound.clip);
    }

    private void OnTriggerExit(Collider other)
    {
        showerEffect.Stop();
    }
}
