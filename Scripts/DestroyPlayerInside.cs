using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerInside : MonoBehaviour
{
    public GameObject blockObj;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(blockObj);
            Destroy(this.gameObject);
        }
    }
}
