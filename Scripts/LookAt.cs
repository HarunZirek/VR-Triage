using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float waitTime;

    bool changePos1 = false;
    bool changePos2 = false;

    public Transform lookPos;
    public Transform newPos;

    Transform pCam;
    Transform pPos;
    private void FixedUpdate()
    {
        if (changePos1)
        {
            var step = 200 * Time.deltaTime;

            var rotation = Quaternion.LookRotation(lookPos.position - pPos.position);
            pPos.rotation = Quaternion.Slerp(pPos.rotation, rotation, Time.deltaTime * step);

            var rotationCam = Quaternion.LookRotation(lookPos.position - pCam.position);
            pPos.rotation = Quaternion.Slerp(pPos.rotation, rotationCam, Time.deltaTime * step * 2);
        }
        /*else if (changePos2)
        {
            var step = 200 * Time.deltaTime;

            var rotation = Quaternion.LookRotation(newPos.position - pPos.position);
            pPos.rotation = Quaternion.Slerp(pPos.rotation, rotation, Time.deltaTime * step);

            var rotationCam = Quaternion.LookRotation(newPos.position - pCam.position);
            pPos.rotation = Quaternion.Slerp(pPos.rotation, rotationCam, Time.deltaTime * step * 2);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            newPos.position = other.transform.position;
            newPos.rotation = other.transform.rotation;
            pPos = other.transform;
            pCam = other.GetComponentInChildren<Camera>().transform;
            StartCoroutine(WaitForLook(other.GetComponent<PlayerController>()));
        }
    }

    IEnumerator WaitForLook(PlayerController pController)
    {
        pController.SetCutscene(true);
        changePos1 = true;

        yield return new WaitForSeconds(waitTime/2);
        changePos1 = false;
        /*pController.SetCutscene(false);
        yield return new WaitForSeconds(waitTime* 5);
        pController.SetCutscene(true);
        changePos2 = true;
        yield return new WaitForSeconds(waitTime / 2);
        changePos2 = false;*/
        pController.SetCutscene(false);
        Destroy(this.gameObject);
    }
}
