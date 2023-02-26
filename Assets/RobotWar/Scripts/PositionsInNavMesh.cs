using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionsInNavMesh : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();
    public static PositionsInNavMesh instance;

    private List<GameObject> gameObjects = new List<GameObject>();


    void Awake()
    {
        instance = this;

        gameObjects = GameObject.FindGameObjectsWithTag("Waypoint").ToList();

        for (int i = 0; i < gameObjects.Count; i++)
        {
            waypoints.Add(gameObjects[i].transform.position);
        }
    }
}