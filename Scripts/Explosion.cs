using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;

public class Explosion : MonoBehaviour
{
    [SerializeField]ParticleSystem[] explosionPart;
    AudioSource expSound;
    public AudioSource fireSound;

    [Tooltip("Explosion Force (Use For Rigidbodies)")]
    public float explosionForce;

    [Tooltip("Death or serius injury")]
    public float highDangerZone;

    [Tooltip("Injury")]
    public float lowDangerZone;

    [Tooltip("Shockwave")]
    public float shockwave;

    [Space(4)]
    public float explosionDuration;

    public bool isWorking { get; private set; }

    public bool FirstExplosion;

    private void Awake()
    {
        isWorking = true;

        expSound = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        isWorking = false;
        foreach (var item in explosionPart)
        {
            item.gameObject.SetActive(true);
            item.Play();
        }
        fireSound.Play();
        expSound.PlayOneShot(expSound.clip);
        StartCoroutine(EndParticle());
        
        StartCoroutine(Wave(explosionForce, 0));
    }

    IEnumerator EndParticle()
    {
        yield return new WaitForSeconds(explosionDuration);
        /*foreach (var item in explosionPart)
        {
            var main = item.main;
            main.loop = false;
        }*/
    }

    //Window -> Package Manager -> Burst 
    //[BurstCompile(CompileSynchronously = true)]
    IEnumerator Wave(float force, int wave)
    {
        float radius;

        if (wave == 0)
        {
            radius = highDangerZone;
        }
        else if (wave == 1)
        {
            force = force / 2;
            radius = lowDangerZone;
        }

        else if (wave == 2)
        {
            force = force / 2;
            radius = shockwave;
        }

        else
            yield break;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.transform.root.gameObject.layer == 14)
                {
                    HumanAI human = rb.transform.root.GetComponent<HumanAI>();
                    if(human)
                    {
                        if (!human.getHit)
                        {
                            //use ecs system
                            //raycast wall control
                            human.Hit((transform.position - rb.transform.position).magnitude);
                            rb.AddExplosionForce(force, transform.position, radius, 3.0F);
                        }
                    }
                }
                else
                {
                    //hit environment use animations not rigidbodies !!!
                }
            }
        }
        yield return new WaitForFixedUpdate();
        wave++;
        StartCoroutine(Wave(explosionForce, wave));
    }
}


// Use it
public enum Health
{
    Death,
    Injury,
    ShockWave,
    Normal
}
