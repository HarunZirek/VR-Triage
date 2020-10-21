using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static void SetCursorState(bool isVisible, bool isLocked)
    {
        if (isLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        Cursor.visible = isVisible;
    }
}
