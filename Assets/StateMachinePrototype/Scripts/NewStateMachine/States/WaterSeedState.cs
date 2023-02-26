using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSeedState : BaseState
{
    public override void OnUpdate()
    {
        if (stateMachine.GoToDestinationUntilReach(stateMachine.currentFocusedPlot.transform.position))
        {
            if (!stateMachine.CheckForAnimEnd("Watering", "Walking")) return;
            stateMachine.currentFocusedPlot.GetComponent<HarvestablePlot>().OnWater();
            stateMachine.currentState = new DropWaterState();
            OnStateEnd();
        }
    }
}
