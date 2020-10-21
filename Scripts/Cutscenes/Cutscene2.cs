using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2 : MonoBehaviour
{
    PlayerController pController;
    private void Awake()
    {
        pController = GameObject.FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        pController.SetCutscene(false, 0.1f, false);
    }
}
