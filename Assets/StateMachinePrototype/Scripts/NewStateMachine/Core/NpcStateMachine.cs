using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine : MonoBehaviour
{
    public float walkSpeed = 2;
    public BaseState currentState;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public GameObject currentFocusedPlot;
    [HideInInspector] public GameObject seedTray;
    [HideInInspector] public GameObject waterPicker;
    [HideInInspector] public GameObject harvestDropThing;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        seedTray = PlantManager.instance.seedTrayPrefab;
        waterPicker = PlantManager.instance.waterPicker;
        harvestDropThing = PlantManager.instance.harvestDropObject;
        spawnPosition = transform.position;
        animator.SetFloat("Speed", walkSpeed);

        currentState = new ObserveState();
        currentState.OnStart(this); 
    }

    private void Update() 
    { 
        currentState.OnUpdate(); 
    }

    public void OnStateEnd()
    {
        //Implementing transitions
    }

    public bool GoToDestinationUntilReach(Vector3 destination, float distanceToValidate = 1)
    {
        if (Vector3.Distance(transform.position, destination) < distanceToValidate)
        {
            return true;
        }
        else
        {
            Vector3 goingVector = (new Vector3(destination.x, transform.position.y, destination.z) - transform.position).normalized;
            transform.Translate(goingVector * walkSpeed * Time.deltaTime, Space.World);
            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
        }

        return false;
    }

    public bool CheckForAnimEnd(string animStateName, string nextAnimToPlay)
    {
        animator.Play(animStateName);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animStateName))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                animator.Play(nextAnimToPlay);
                return true;
            }
        }
        else
        {
            return false;
        }

        return false;
    }
}
