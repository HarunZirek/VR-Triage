using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectObj : MonoBehaviour
{
    PlayerCanvasController playerCanvas;
    [SerializeField] PlayerCanvasController.TriageVal triage;

    private void Start()
    {
        playerCanvas = GameObject.FindObjectOfType<PlayerCanvasController>();
    }

    public void SetTrigeVal()
    {
        playerCanvas.SetTriage((int)triage);
    }
}
