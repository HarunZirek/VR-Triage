using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryHuman : MonoBehaviour
{
    Animator entryHumanAnim;
    [SerializeField]Transform target;

    bool startWalk = false;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private void Awake() =>
        entryHumanAnim = GetComponent<Animator>();
    private void FixedUpdate()
    {
        if (startWalk)
        {
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                startWalk = false;
                entryHumanAnim.SetBool("Walking", false);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
                _direction = (target.position - transform.position).normalized;

                //create the rotation we need to be in to look at the target
                _lookRotation = Quaternion.LookRotation(_direction);

                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 20);
            }
        }
    }

    public void StartTalking(bool startTalk)
    {
        if (startTalk)
        {
            entryHumanAnim.SetBool("Talking", true);
        }
        else 
        {
            entryHumanAnim.SetBool("Walking", true);
            startWalk = true;
        }
    }
}
