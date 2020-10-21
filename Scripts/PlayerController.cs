using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCanvasController pCanvas;
    RaycastHand rayHand;

    public GameObject blackScreen;

    bool isInteracting = false, inCutscene = false;

    HumanAI human = null;
    Interactable interactable = null;

    public ButtonClickCode[] buttonClickCodes;

    AudioSource pAudioSource;
    [SerializeField] Transform aiPos;
    public Transform AiPos()
    {
        return aiPos;
    }

    private void Awake()
    {
        pAudioSource = GetComponent<AudioSource>();
        pCanvas = GameObject.FindObjectOfType<PlayerCanvasController>();
        rayHand = GameObject.FindObjectOfType<RaycastHand>();

        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void Update()
    {
        rayHand.Raycast();
    }

    public void SetCutscene(bool cutScene)
    {
        inCutscene = cutScene;
        if (cutScene)
            rayHand.teleportPlayer.SetActive(false);
        else
            rayHand.teleportPlayer.SetActive(true);
    }
    public void SetCutscene(bool cutScene, float cutsceneTime, bool setVal)
    {
        inCutscene = cutScene;
        pCanvas.SetBlackScreen(cutsceneTime, setVal);
    }

    public void StartAudio(AudioClip audioClip)
    {
        pAudioSource.PlayOneShot(audioClip);
    }

    public void SetLastQuestion()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        RaycastHand.inCutscene = true;
        blackScreen.SetActive(true);    
        pCanvas.SetQuestion();
    }

    public void CloseQuestion()
    {
        RaycastHand.inCutscene = false;
        StartCoroutine(EndQuest());
    }

    IEnumerator EndQuest()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        yield return new WaitForSeconds(0.6f);
        pCanvas.CloseQuestion();
        blackScreen.SetActive(false);

        foreach (var item in buttonClickCodes)
        {
            item.gameObject.SetActive(true);
            item.ResetClick();
        }
    }
}
