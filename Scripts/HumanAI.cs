using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAI : MonoBehaviour
{
    private Animator anim;
    NavMeshAgent ai;
    public Waypoints points;
    Outline outline;

    public Health currentHealth;

    public bool getHit;
    //public bool isSitting = false;
    private bool hasPoint = false;
    public bool triageSetted = false;
    [TextArea]
    public string stats;

    public PlayerCanvasController.TriageVal triage;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        outline = GetComponent<Outline>();
       
    }

    private void Start()
    {
        /*if (isSitting)
            ai.enabled = false;*/
        if (getHit && currentHealth != Health.Death)
            anim.enabled = false;
    }
    public void Hit(float hitVal)
    {
        getHit = true;
        anim.enabled = false;
    }

    public void SetOutline(bool val)
    {
        if(outline)
            outline.enabled = val;
    }

    IEnumerator StandUp(int waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        //stand up animation and run to exit
    }

    public void Kill() => Debug.Log("Dead");
}
