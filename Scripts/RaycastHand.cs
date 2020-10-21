using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;
public class RaycastHand : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Input_Sources handType;
    public SteamVR_Input_Sources handType2;
    PlayerCanvasController pCanvas; //sil playerControllera ekle
    HumanAI human;
    ButtonSelectObj btnSelect;
    Vector3 target;
    Interactable interactable;
    bool clicked = false;
    public GameObject teleportPlayer;
    public float rayLength;

    [SerializeField] LayerMask mask;

    public static bool inCutscene = false;

    private void OnLevelWasLoaded(int level)
    {
        teleportPlayer = GameObject.FindObjectOfType<Valve.VR.InteractionSystem.Teleport>().gameObject;
    }
    void Start()
    {
        pCanvas = GameObject.FindObjectOfType<PlayerCanvasController>();
        teleportPlayer = GameObject.FindObjectOfType<Valve.VR.InteractionSystem.Teleport>().gameObject;

    }
    private void OnDrawGizmos()
    {
        if (target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
           // Gizmos.DrawLine(transform.position, Vector3.forward * rayLength);
        }
    }
    public void Raycast()
    {

        if (grabAction.GetLastStateDown(handType2) && !inCutscene)
        {
            pCanvas.InteractHuman(false);
            pCanvas.InteractObj(false);
            if(interactable)
                interactable.isInteracting = false;
            if (teleportPlayer)
                teleportPlayer.SetActive(true);
            clicked = false;
        }

        RaycastHit raycastHit;
        GameObject gameObject = null;


        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, rayLength, mask))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            target = forward;

            gameObject = raycastHit.collider.gameObject;

            ButtonSelectObj selectedObj = gameObject.transform.GetComponent<ButtonSelectObj>();

            if (selectedObj)
            {
                btnSelect = selectedObj;

                if (grabAction.GetLastStateDown(handType))
                {
                    btnSelect.SetTrigeVal();
                }
            }
            else
            {
                if (btnSelect)
                {
                    btnSelect = null;
                }
            }

            HumanAI currentHuman = gameObject.transform.root.GetComponent<HumanAI>();
            if (currentHuman)
            {
                if (human && currentHuman != human)
                {
                    human.SetOutline(false);
                }

                if (!clicked)
                {

                    human = currentHuman;
                    human.SetOutline(true);
                    pCanvas.SetInteract(true, "VİTALLER");

                    if (grabAction.GetLastStateDown(handType))
                    {
                        pCanvas.SetHuman(human);
                        pCanvas.InteractHuman();
                        teleportPlayer.SetActive(false);
                        clicked = true;
                        //canvas
                    }
                }
            }
            else
            {
                Interactable currentInteractable = raycastHit.transform.root.gameObject.GetComponent<Interactable>();

                if (currentInteractable)
                {
                    if (currentInteractable.gameObject.name == "Door")
                    {
                        interactable = currentInteractable;
                        pCanvas.SetInteract(true, "İÇERİ GİR");
                        interactable.SetOutline(true);
                        if (grabAction.GetLastStateDown(handType))
                        {
                            pCanvas.SetInteract(false);
                            interactable.SetOutline(false);
                            pCanvas.ChangeScene(3);
                            //canvas

                        }
                    }
                    else if (currentInteractable.gameObject.name == "StartButton")
                    {
                        interactable = currentInteractable;
                        interactable.SetOutline(true);
                        if (grabAction.GetLastStateDown(handType))
                        {
                            currentInteractable.GetComponent<StartButton>().StartGame();
                        }
                    }
                    else if (currentInteractable.gameObject.name == "ExitButton")
                    {
                        interactable = currentInteractable;
                        interactable.SetOutline(true);
                        if (grabAction.GetLastStateDown(handType))
                        {
                            currentInteractable.GetComponent<ExitButton>().ExitGame();
                        }
                    }
                    else
                    {
                        interactable = currentInteractable;
                        pCanvas.SetInteract(true, "İNCELE");
                        interactable.SetOutline(true);

                        if (grabAction.GetLastStateDown(handType))
                        {
                            teleportPlayer.SetActive(false);
                            interactable.isInteracting = true;
                            pCanvas.SetInteractObj(interactable);
                            pCanvas.InteractObj();
                            teleportPlayer.SetActive(false);
                            //canvas
                        }
                    }
                }
                else
                {
                    ButtonClickCode clickedBtn = gameObject.transform.GetComponent<ButtonClickCode>();

                    if (clickedBtn)
                    {
                        if (grabAction.GetLastStateDown(handType))
                        {
                            clickedBtn.ButtonClick();
                        }
                    }
                }
            }
        }
        else
        {
            if (human)
            {
                human.SetOutline(false);
                pCanvas.SetInteract(false);
                human = null;
            }
            if (interactable)
            {
                interactable.SetOutline(false);
                pCanvas.SetInteract(false);
                interactable = null;
            }
        }
    }

}
