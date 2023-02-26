using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveState : BaseState
{
    override public void OnUpdate()
    {
        if (PlantManager.instance.CheckForHarvestablePlot())
        {
            stateMachine.currentFocusedPlot = PlantManager.instance.CheckForHarvestablePlot();
            stateMachine.currentState = new HarvestState();
            OnStateEnd();
        }
        else if (PlantManager.instance.CheckForSeedablePlot())
        {
            stateMachine.currentFocusedPlot = PlantManager.instance.CheckForSeedablePlot();
            stateMachine.currentState = new PickSeedState();
            stateMachine.animator.Play("Walking");
            OnStateEnd();
        }

        if (stateMachine.GoToDestinationUntilReach(stateMachine.spawnPosition))
        {
            stateMachine.animator.Play("Idle");
        }
    }
}
