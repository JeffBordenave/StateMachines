using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GardenerState
{
    Void,
    Observe,
    PickSeed,
    PlantSeed,
    FetchWater,
    WaterSeed,
    DropWater,
    Harvest,
    DropHarvest
}

public class Gardener : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2;

    private delegate void StateDelegate();
    private StateDelegate Action;

    private Vector3 spawnPosition;
    private GameObject currentFocusedPlot;
    private GameObject seedTray;
    private GameObject waterPicker;
    private GameObject harvestDropThing;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetState(GardenerState.Observe);
        seedTray = PlantManager.instance.seedTrayPrefab;
        waterPicker = PlantManager.instance.waterPicker;
        harvestDropThing = PlantManager.instance.harvestDropObject;
        spawnPosition = transform.position;
        animator.SetFloat("Speed", walkSpeed);
    }

    private void Update()
    {
        Action();
    }

    #region set states
    private void SetState(GardenerState stateToSet)
    {
        walkSpeed += 0.2f;
        animator.SetFloat("Speed", walkSpeed);

        switch (stateToSet)
        {
            case GardenerState.Void:
                SetStateVoid();
                break;
            case GardenerState.Observe:
                SetStateObserve();
                break;
            case GardenerState.PickSeed:
                SetStatePickSeed();
                break;
            case GardenerState.PlantSeed:
                SetStatePlantSeed();
                break;
            case GardenerState.FetchWater:
                SetStateFetchWater();
                break;
            case GardenerState.WaterSeed:
                SetStateWaterSeed();
                break;
            case GardenerState.DropWater:
                SetStateDropWater();
                break;
            case GardenerState.Harvest:
                SetStateHarvest();
                break;
            case GardenerState.DropHarvest:
                SetStateDropHarvest();
                break;
            default:
                break;
        }
    }

    private void SetStateVoid()
    {
        Action = ActionVoid;
    }

    private void SetStateObserve()
    {
        Action = ActionObserve;
    }

    private void SetStatePickSeed()
    {
        Action = ActionPickSeed;
        animator.Play("Walking");
    }

    private void SetStatePlantSeed()
    {
        Action = ActionPlantSeed;
        animator.Play("Walking");
    }
    
    private void SetStateFetchWater()
    {
        Action = ActionFetchWater;
        animator.Play("Walking");
    }
    
    private void SetStateWaterSeed()
    {
        Action = ActionWaterSeed;
        animator.Play("Walking");
    }

    private void SetStateDropWater()
    {
        Action = ActionDropWater;
        animator.Play("Walking");
    }
    
    private void SetStateHarvest()
    {
        Action = ActionHarvest;
        animator.Play("Walking");
    }

    private void SetStateDropHarvest()
    {
        Action = ActionDropHarvest;
        animator.Play("Walking");
    }
    #endregion

    #region action states
    private void ActionVoid(){ }

    private void ActionObserve()
    {
        if (PlantManager.instance.CheckForHarvestablePlot())
        {
            currentFocusedPlot = PlantManager.instance.CheckForHarvestablePlot();
            SetStateHarvest();
        }
        else if (PlantManager.instance.CheckForSeedablePlot())
        {
            currentFocusedPlot = PlantManager.instance.CheckForSeedablePlot();
            SetStatePickSeed();
        }

        if (GoToDestinationUntilReach(spawnPosition))
        {
            animator.Play("Idle");
        }
    }

    private void ActionPickSeed()
    {
        if (GoToDestinationUntilReach(seedTray.transform.position))
        {
            if (CheckForAnimEnd("GrabPlace", "Walking")) 
                SetStatePlantSeed();
        }
    }

    private void ActionPlantSeed()
    {
        if (GoToDestinationUntilReach(currentFocusedPlot.transform.position,3f))
        {
            if (CheckForAnimEnd("Plant", "Walking"))
            {
                currentFocusedPlot.GetComponent<Interactable>().OnInteract();
                SetState(GardenerState.FetchWater);
                transform.position = currentFocusedPlot.transform.position;
            }
        }
    }

    private void ActionFetchWater()
    {
        if (GoToDestinationUntilReach(waterPicker.transform.position))
        {
            if (!CheckForAnimEnd("GrabPlace", "Walking")) return;
            waterPicker.GetComponent<Interactable>().OnInteract();
            SetState(GardenerState.WaterSeed);
        }
    }

    private void ActionWaterSeed()
    {
        if (GoToDestinationUntilReach(currentFocusedPlot.transform.position))
        {
            if (!CheckForAnimEnd("Watering", "Walking")) return;
            currentFocusedPlot.GetComponent<HarvestablePlot>().OnWater();
            SetState(GardenerState.DropWater);
        }
    }

    private void ActionDropWater()
    {
        if (GoToDestinationUntilReach(waterPicker.transform.position))
        {
            if (!CheckForAnimEnd("GrabPlace", "Walking")) return;
            waterPicker.GetComponent<Interactable>().OnInteract();
            SetState(GardenerState.Observe);
        }
    }

    private void ActionHarvest()
    {
        if (GoToDestinationUntilReach(currentFocusedPlot.transform.position))
        {
            if (!CheckForAnimEnd("Harvest", "Walking")) return;
            SetState(GardenerState.DropHarvest);
            currentFocusedPlot.GetComponent<HarvestablePlot>().OnInteract();
        }
    }
    
    private void ActionDropHarvest()
    {
        if (GoToDestinationUntilReach(harvestDropThing.transform.position))
        {
            if (!CheckForAnimEnd("GrabPlace", "Walking")) return;
            SetState(GardenerState.Observe);
        }
    }
    #endregion

    private bool GoToDestinationUntilReach(Vector3 destination, float distanceToValidate = 1)
    {
        if (Vector3.Distance(transform.position, destination) < distanceToValidate)
        {
            return true;
        }
        else
        {
            Vector3 goingVector = (new Vector3(destination.x,transform.position.y,destination.z) - transform.position).normalized;
            transform.Translate(goingVector * walkSpeed * Time.deltaTime, Space.World);
            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
        }

        return false;
    }
    private bool CheckForAnimEnd(string animStateName,string nextAnimToPlay)
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