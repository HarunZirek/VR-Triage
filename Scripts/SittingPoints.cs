using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingPoints : MonoBehaviour
{
    Transform[] poses;

    private bool[] available;

    public GameObject[] humans;

    public int freq;

    private void Awake()
    {
        poses = new Transform[GameObject.FindGameObjectsWithTag("SitPos").Length];

        for (int i = 0; i < poses.Length; i++)
        {
            poses[i] = GameObject.FindGameObjectsWithTag("SitPos")[i].transform;
        }

        available = new bool[poses.Length];

        for (int i = 0; i < available.Length; i++)
        {
            available[i] = true;
        }

        for (int i = 0; i < freq; i++)
        {
            int rand = Random.Range(0, poses.Length);
            
            if (available[rand] == true)
            {
                available[rand] = false;
                GameObject cloneHuman = Instantiate(humans[Random.Range(0, humans.Length)], poses[rand].position,
                    poses[rand].rotation, null);

                //cloneHuman.GetComponent<HumanAI>().isSitting = true;
            }
        }
    }
}
