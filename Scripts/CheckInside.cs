using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInside : MonoBehaviour
{
    [SerializeField] CheckHumans humanCheck;
    [SerializeField] AudioClip insideClip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            humanCheck.inside = true;
            humanCheck.SetBox();
            humanCheck.transform.GetComponent<BoxCollider>().enabled = true;
            other.GetComponent<PlayerController>().StartAudio(insideClip);
            GameObject.FindObjectOfType<FriendlyAI>().goPatient = true;
            humanCheck.SetText(true);
            Destroy(this.gameObject);
        }
        
    }
}
