using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum Team
{
    Red,
    Blue,
    Yellow
}

public class EnemyBehavior : MonoBehaviour
{
    protected NavMeshAgent navAgent = new NavMeshAgent();
    protected int currentPos = 0;
    protected int lastPos = 0;
    protected float waitCounter = 0;
    protected List<Vector3> targetPositions;

    [SerializeField] protected float distanceTreshold = 1.0f;
    [SerializeField] protected float waitTimeBetweenPos = 0.5f;

    [Header("Team")]
    [SerializeField] public Material material;
    [SerializeField] public Team team;
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        Move();
    }

    protected void Init()
    {
        targetPositions = PositionsInNavMesh.instance.waypoints;
        lastPos = Random.Range(0, targetPositions.Count);

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(targetPositions[lastPos]);

        transform.Find("Head").GetComponent<Renderer>().material = material;
    }

    protected void Move()
    {
        if (Vector3.Distance(transform.position, targetPositions[lastPos]) < distanceTreshold)
        {
            navAgent.isStopped = true;
            waitCounter += Time.deltaTime;

            //currentPos = (currentPos + 1)%(targetPositions.Count);

            //if (waitCounter >= waitTimeBetweenPos)
            //{
                //currentPos++;

                //if (currentPos > targetPositions.Count - 1)
                //{
                //    currentPos = 0;
                //}

                navAgent.isStopped = false;
                MoveToNextTarget();

                //float delay = 3.0f;
                //Invoke("MoveToNextTarget", delay);

                //waitCounter = 0;
            //}
        }
    }

    protected void MoveToNextTarget()
    {
        targetPositions = PositionsInNavMesh.instance.waypoints;
        int i = Random.Range(0, targetPositions.Count);
        while (lastPos == i)
        {
            i = Random.Range(0, targetPositions.Count);
        }
        
        navAgent.SetDestination(targetPositions[i]);
        lastPos = i;
    }
}
