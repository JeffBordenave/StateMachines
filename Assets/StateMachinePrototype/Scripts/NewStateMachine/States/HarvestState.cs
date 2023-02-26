using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestState : BaseState
{
    public override void OnUpdate()
    {
        if (stateMachine.GoToDestinationUntilReach(stateMachine.currentFocusedPlot.transform.position))
        {
            if (!stateMachine.CheckForAnimEnd("Harvest", "Walking")) return;
            stateMachine.currentFocusedPlot.GetComponent<HarvestablePlot>().OnInteract();
            stateMachine.currentState = new DropHarvestState();
            OnStateEnd();
        }
    }
}
