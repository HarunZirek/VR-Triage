using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1 : MonoBehaviour
{
    [SerializeField] BoxCollider triggeredBox;
    public GameObject teleportPlayer;
    public AudioSource startAudio;
    public EntryHuman animatorHuman;
    private void OnTriggerEnter(Collider other)
    {
        RaycastHand.inCutscene = true;
        animatorHuman.StartTalking(true);
            teleportPlayer.SetActive(false);
            StartCoroutine(cutseneNum());
            triggeredBox.enabled = false;
    }

    IEnumerator cutseneNum()
    {
        startAudio.PlayOneShot(startAudio.clip);
        yield return new WaitForSeconds(startAudio.clip.length);
        animatorHuman.StartTalking(false);
        teleportPlayer.SetActive(true);
        RaycastHand.inCutscene = false;
        Destroy(this.gameObject);
    }
}
