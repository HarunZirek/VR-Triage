using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameScene : MonoBehaviour
{
    private void Start()
    {
        Invoke("ChangeScene", 1f);
    }

    private void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
