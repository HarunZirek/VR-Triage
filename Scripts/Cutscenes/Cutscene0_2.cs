using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene0_2 : MonoBehaviour
{
    private PlayerController pController;

    [SerializeField] BoxCollider cutSceneCollider;
    [SerializeField] BoxCollider cutSceneCollider2;
    [SerializeField] AudioClip clip;
    [SerializeField] Interactable interactable;

    bool isWorking = false;

    private void FixedUpdate()
    {
        if(interactable.isInteracting && isWorking)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isWorking = true;
            pController = other.GetComponent<PlayerController>();
            pController.SetCutscene(true);
            pController.StartAudio(clip);
            cutSceneCollider2.enabled = true;
        }
    }

}
