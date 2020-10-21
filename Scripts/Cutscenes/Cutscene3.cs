using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3 : MonoBehaviour
{
    [SerializeField]Transform friendlyLastPos1;

    [SerializeField] Explosion explosion;

    PlayerController playerController;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject[] removedObj;

    public AudioSource crowdAudio;
    private void OnEnable()
    {
        for (int i = 0; i < removedObj.Length; i++)
        {
            removedObj[i].SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController = other.GetComponent<PlayerController>();
            StartCoroutine(WaitExplosion());
        }
    }

    IEnumerator WaitExplosion()
    {
        playerController.SetCutscene(true);
        FriendlyAI friendlyObj = GameObject.FindObjectOfType<FriendlyAI>();
        friendlyObj.SetMove(friendlyLastPos1.position);
        friendlyObj.transform.gameObject.layer = 14;
        yield return new WaitForSeconds(1.5f);
        friendlyObj.transform.GetComponent<Animator>().enabled = false;
        friendlyObj.enabled = false;
        crowdAudio.Stop();
        explosion.Explode();
        yield return new WaitForSeconds(8f);
        playerController.SetLastQuestion();
        Destroy(this.gameObject);
    }

}
