using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSeedState:BaseState
{
    override public void OnUpdate()
    {
        if (stateMachine.GoToDestinationUntilReach(stateMachine.seedTray.transform.position))
        {
            if (stateMachine.CheckForAnimEnd("GrabPlace", "Walking"))
            {
                stateMachine.currentState = new PlantSeedState();
                OnStateEnd();
            }
        }
    }
}
