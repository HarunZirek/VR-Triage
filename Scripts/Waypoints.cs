using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Vector3 getWaypoint()
    {
        return points[Random.Range(0,points.Length)].position;
    }

    public Transform[] points;
}
