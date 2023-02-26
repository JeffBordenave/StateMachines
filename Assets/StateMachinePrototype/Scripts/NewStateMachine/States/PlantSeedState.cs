using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeedState : BaseState
{
    override public void OnUpdate()
    {
        if (stateMachine.GoToDestinationUntilReach(stateMachine.currentFocusedPlot.transform.position, 3f))
        {
            if (stateMachine.CheckForAnimEnd("Plant", "Walking"))
            {
                stateMachine.currentFocusedPlot.GetComponent<Interactable>().OnInteract();
                stateMachine.transform.position = stateMachine.currentFocusedPlot.transform.position;
                stateMachine.currentState = new FetchWaterState();
                OnStateEnd();
            }
        }
    }
}
