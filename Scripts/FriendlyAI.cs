using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    NavMeshAgent ai;

    Animator anim;

    public Transform player;
    public Transform patient;
    Vector3 lastPos;


    bool getHit = false;
    bool isSitting = false;
    bool hasPoint = true;

    public bool goPatient = false;
    private bool goLastPos = false;
    void Awake()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
    }

    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindObjectOfType<PlayerController>().AiPos();
        if (player)
            ai.destination = player.position;
    }
    void Start()
    {
        if(player)
                ai.destination = player.position;
    }

    void FixedUpdate() //Her oyun guncellemesinde kontrol edilir
    {
    
        if (!getHit && hasPoint && player) //Patlamadan etkilenmediyse calisir
        {
            if (isSitting) //Triyaj bolgesine giris yapildiginda calisir
            {
                anim.SetBool("Crouching", true);
                return;
            }

            if ((transform.position - ai.destination).magnitude < 0.1f) //Kullanci hareket etmiyorsa calisir
            {
                anim.SetBool("Walking", false);
                if (goPatient) //Yaraliya vardiysa calisir
                {
                    isSitting = true;
                }
                ai.isStopped = true;
            }
            else
            {
                ai.isStopped = false;
                anim.SetBool("Walking", true);
             
            }

            if (goPatient) //Varis noktasi olarak yaraliyi secer
            {
                ai.SetDestination(patient.position);
            }
            else if(goLastPos)
            {
                ai.SetDestination(lastPos);
            }
            else if(ai.isStopped && (transform.position - player.position).magnitude > 0.15f)
            {
                ai.SetDestination(player.position);
            }
        }
    }
    public void Hit(float hitVal)
    {
        getHit = true;
    }

    public void SetMove()
    {
        isSitting = false;

        anim.SetBool("Crouching", false);
    }

    public void SetMove(Vector3 pos)
    {
        lastPos = pos;
        goLastPos = true;
    }
}
