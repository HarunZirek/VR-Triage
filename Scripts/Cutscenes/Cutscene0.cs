using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene0 : MonoBehaviour
{
    public GameObject blackObj;
    public AudioClip startAudio;    
    PlayerController pController;
    public GameObject teleportPlayer;
    public Transform playerPos;

    public AudioSource outsideAudio;
    private void Start()
    {
        pController = GameObject.FindObjectOfType<PlayerController>();
        pController.transform.root.position = playerPos.position;
        pController.transform.root.rotation = playerPos.rotation;
        StartCoroutine(cutseneNum());
        RaycastHand.inCutscene = true;
    }

    IEnumerator cutseneNum()
    {
        teleportPlayer.SetActive(false);
        pController.StartAudio(startAudio);
        pController.SetCutscene(true);
        yield return new WaitForSeconds(startAudio.length+1f);
        blackObj.SetActive(false);
        pController.SetCutscene(false);
        teleportPlayer.SetActive(true);
        outsideAudio.Play();
        RaycastHand.inCutscene = false  ;
        Destroy(this.gameObject);
    }

}
