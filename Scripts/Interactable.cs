using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Outline outline;

    [TextArea]
    public string stats;

    public bool isInteracting = false;
    void Awake()
    {   
        outline = GetComponent<Outline>();
    }

    public void SetOutline(bool val) =>
           outline.enabled = val;
}
