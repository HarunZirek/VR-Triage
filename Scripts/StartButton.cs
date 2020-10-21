using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    PlayerController pController;

    private void Awake()
    {
        pController = GameObject.FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        GameObject.FindObjectOfType<RaycastHand>().rayLength = 200f;
        pController.transform.root.position = playerPos.position;
        pController.transform.root.rotation = playerPos.rotation;
    }
    public void StartGame()
    {
        GameObject.FindObjectOfType<RaycastHand>().rayLength = 1.5f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
