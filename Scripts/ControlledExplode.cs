using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledExplode : MonoBehaviour
{
    public Explosion exp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            exp.Explode();
        }
    }
}
