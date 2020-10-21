using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickCode : MonoBehaviour
{
    public bool clicked
    {
        get;
        private set;
    } = false;

    [SerializeField] Color color;
    [SerializeField] Color oldColor;
    [SerializeField] UnityEngine.UI.Image btnImg;
    public void ButtonClick()
    {
        btnImg.color = color;
        clicked = true;
        ButtonClickCode[] buttonClickCodes = GameObject.FindObjectsOfType<ButtonClickCode>();

        for (int i = 0; i < buttonClickCodes.Length; i++)
        {
            if(!buttonClickCodes[i].clicked)
            {
                buttonClickCodes[i].gameObject.SetActive(false);
            }
        }

        StartCoroutine(ReturnMenu());
    }

    IEnumerator ReturnMenu()
    {
        yield return new WaitForSeconds(7f);
        GameObject.FindObjectOfType<PlayerController>().CloseQuestion();
    }

    public void ResetClick()
    {
        btnImg.color = oldColor;
        clicked = false;
    }
}
