using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public static int HUMAN_LAYER = 14;

    private Explosion[] explosions;

    private int setExplosionIndex;

    private void Awake()
    {
        explosions = new Explosion[GameObject.FindObjectsOfType<Explosion>().Length];

        explosions = GameObject.FindObjectsOfType<Explosion>();

        setExplosionIndex = Random.Range(0, explosions.Length);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if(explosions[setExplosionIndex].isWorking)
                explosions[setExplosionIndex].Explode();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
