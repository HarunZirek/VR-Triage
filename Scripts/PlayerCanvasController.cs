using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasController : MonoBehaviour
{
    Animator canvasAnimator;

    HumanAI currentHuman;
    Interactable currentInteractable;

    public UnityEngine.UI.Image greenT, yellowT, redT, blackT;

    public UnityEngine.UI.Text interactText;

    public UnityEngine.UI.Text humanStats;

    public UnityEngine.UI.Text interactStats;

    public TMPro.TMP_Text triageNumText;

    TriageVal currentVal;

    public GameObject triageTextObj;

    private void Awake()
    {
        canvasAnimator = GetComponent<Animator>();
    }
    public void SetInteract(bool val)
    {
        canvasAnimator.SetBool("isInteracting", val);
    }

    public void SetInteract(bool val, string name)
    {
        interactText.text = name;
        canvasAnimator.SetBool("isInteracting", val);
    }


    public void SetHuman(HumanAI human)
    {
        currentHuman = human;
    }

    public void SetInteractObj(Interactable interactable)
    {
        currentInteractable = interactable;
    }

    public void InteractObj()
    {
        if (currentInteractable)
        {
            interactStats.text = currentInteractable.stats;
            SetObjMenu(true);
            SetInteract(false);
        }
    }
    public void InteractObj(bool val)
    {
        if (currentInteractable)
        {
            SetObjMenu(val);
            SetInteract(false);
        }
    }
    public void InteractHuman()
    {
        if (currentHuman)
        {
            humanStats.text = currentHuman.stats;
            ResetTriage();
            if (currentHuman.triageSetted)
                SetTriage((int)currentHuman.triage);
            SetHumanMenu(true);
            SetInteract(false);
        }
    }

    public void InteractHuman(bool val)
    {
        if (currentHuman)
        {
            SetHumanMenu(val);
            if (currentHuman.triageSetted)
            {
                currentHuman.triage = currentVal;
                currentHuman.triageSetted = true;
            }
            SetInteract(false);
        }
    }

    public void SetHumanMenu(bool val)
    {
        if (val)
            CursorController.SetCursorState(true, false);
        else
            CursorController.SetCursorState(false, true);

        canvasAnimator.SetBool("settingTriage", val);
    }

    public void SetObjMenu(bool val)
    {
        canvasAnimator.SetBool("settingObjStats", val);
    }

    void ResetTriage()
    {
        greenT.enabled = false;
        yellowT.enabled = false;
        redT.enabled = false;
        blackT.enabled = false;
    }
    public void SetTriage(int val)
    {
        if (val == (int)TriageVal.green)
        {
            greenT.transform.gameObject.SetActive(true);
            greenT.enabled = true;
            yellowT.enabled = false;
            redT.enabled = false;
            blackT.enabled = false;
            currentVal = TriageVal.green;
            currentHuman.triageSetted = true;

        }
        else if (val == (int)TriageVal.yellow)
        {
            greenT.enabled = false;
            yellowT.enabled = true;
            yellowT.transform.gameObject.SetActive(true);
            redT.enabled = false;
            blackT.enabled = false;
            currentVal = TriageVal.yellow;
            currentHuman.triageSetted = true;
        }
        else if (val == (int)TriageVal.red)
        {
            greenT.enabled = false;
            yellowT.enabled = false;
            redT.enabled = true;
            redT.transform.gameObject.SetActive(true);
            blackT.enabled = false;
            currentVal = TriageVal.red;
            currentHuman.triageSetted = true;
        }
        else
        {
            greenT.enabled = false;
            yellowT.enabled = false;
            redT.enabled = false;
            blackT.transform.gameObject.SetActive(true);
            blackT.enabled = true;
            currentVal = TriageVal.black;
            currentHuman.triageSetted = true;
        }
    }

    public void SetBlackScreen(float waitTime, bool setVal)
    {
        StartCoroutine(BlackScreen(waitTime, setVal));
    }

    public void ChangeScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    IEnumerator BlackScreen(float waitTime, bool setIn)
    {   
        if (setIn)
        {
            canvasAnimator.SetBool("blackScreenOut", false);
            canvasAnimator.SetBool("blackScreenIn", true);
            yield return new WaitForEndOfFrame();
            canvasAnimator.SetBool("blackScreenIn", false);
        }
        yield return new WaitForSeconds(waitTime);
        if (!setIn)
        {
            canvasAnimator.SetBool("blackScreenIn", false);
            canvasAnimator.SetBool("blackScreenOut", true);
            yield return new WaitForEndOfFrame();
            canvasAnimator.SetBool("blackScreenOut", false);

        }
    }
    public enum TriageVal
    {
        green,
        yellow,
        red,
        black
    }

    public void SetQuestion()
    {
        StartCoroutine(BlackScreen(1.5f, true));
        CursorController.SetCursorState(true, false);
        triageNumText.text = "Yapılan Triyaj Sayısı : " + triageNum;

        triageTextObj.SetActive(true);
        canvasAnimator.SetBool("LastQuestion", true);
    }

    public void CloseQuestion()
    {
        canvasAnimator.SetTrigger("CloseQuest");
        triageTextObj.SetActive(false);
    }

    public static int triageNum = 0;
}
