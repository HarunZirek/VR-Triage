using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHumans : MonoBehaviour
{
    public HumanAI[] humans;

    public bool inside = false;

    public AudioClip audioExit;
    bool allChecked = true;
    [SerializeField] BoxCollider collider;
    [SerializeField] BoxCollider blockBack;

    public UnityEngine.UI.Text patientControl;

    PlayerController pController;

    public GameObject stage2;

    TMPro.TMP_Text timeText;

    private bool timeIsUp = false;

    private bool isTicking = false;

    int min = 3, sec = 0;

    int controlledNum = 0;

    [SerializeField] Transform teleportPos;
    public GameObject teleportPlayer;
    public GameObject blockPlayer;

    public Transform beforeExpPos;

    private void OnLevelWasLoaded(int level)
    {
        timeText = GameObject.Find("TimeText").GetComponent<TMPro.TMP_Text>();
    }
    private void Awake()
    {
        pController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void SetText(bool setTick)
    {
        isTicking = setTick;
        if (setTick)
            StartCoroutine(DecTime());
        else
        {
            timeText.text = "";
        }
    }

    private void FixedUpdate()
    {
        if (inside)
        {
            if (timeIsUp)
            {
                PlayerCanvasController.triageNum = controlledNum;
                allChecked = true;
                inside = false;
                isTicking = false;
                SetText(false);
                GameObject.FindObjectOfType<PlayerCanvasController>().SetHumanMenu(false);
                GameObject.FindObjectOfType<FriendlyAI>().goPatient = false;
                GameObject.FindObjectOfType<FriendlyAI>().SetMove();
                StartCoroutine(WaitExit());
            }

            if (patientControl)
            {
                allChecked = true;

                controlledNum = 0;
                foreach (HumanAI human in humans)
                {
                    if (!human.triageSetted)
                    {
                        allChecked = false;
                    }
                    else
                        controlledNum++;
                }
                patientControl.text = controlledNum + " / 10 Hastaya triyaj yapıldı.";
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(inside)
        {
            if (allChecked)
            {
                RaycastHand.inCutscene = true;
                PlayerCanvasController.triageNum = controlledNum;
                inside = false;
                isTicking = false;
                SetText(false);
                StartCoroutine(WaitExit());
                GameObject.FindObjectOfType<FriendlyAI>().goPatient = false;
                GameObject.FindObjectOfType<FriendlyAI>().SetMove();
            }
            else
            {
                //check all patients    
            }
        }
    }

    public void SetBox()
    {
        blockBack.enabled = true;
    }
    IEnumerator DecTime()
    {
        timeText.text = "Kalan sure " + min + ":" + sec;
        if (!isTicking)
        {
            yield break;
        }
        if (sec == 0)
        {
            if (min > 0)
            {
                sec = 59;
                min--;
            }
            else
            {
                timeIsUp = true;
                yield break;
            }
        }
        else 
        {
            sec--;
        }
        yield return new WaitForSeconds(1);
        if(isTicking)
            StartCoroutine(DecTime());
        yield break;
    }

    IEnumerator WaitExit()
    {
        Destroy(patientControl);
        pController.SetCutscene(true);
        teleportPlayer.SetActive(false);
        pController.StartAudio(audioExit);
        pController.transform.root.position = teleportPos.position;
        collider.enabled = false;
        Destroy(this.gameObject, audioExit.length+1f);
        stage2.SetActive(true);
        FriendlyAI friendlyObj = GameObject.FindObjectOfType<FriendlyAI>();
        friendlyObj.SetMove(beforeExpPos.position);
        yield return new WaitForSeconds(audioExit.length);
        RaycastHand.inCutscene = false;
        blockPlayer.SetActive(true);
        teleportPlayer.SetActive(true);
        pController.SetCutscene(false);

    }
}
