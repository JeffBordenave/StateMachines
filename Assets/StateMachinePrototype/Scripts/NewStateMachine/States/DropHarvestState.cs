using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHarvestState : BaseState
{
    public override void OnUpdate()
    {
        if (stateMachine.GoToDestinationUntilReach(stateMachine.harvestDropThing.transform.position))
        {
            if (!stateMachine.CheckForAnimEnd("GrabPlace", "Walking")) return;
            stateMachine.currentState = new ObserveState();
            OnStateEnd();
        }
    }
}
