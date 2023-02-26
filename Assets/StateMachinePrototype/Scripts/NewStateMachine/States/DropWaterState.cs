using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWaterState : BaseState
{
    public override void OnUpdate()
    {
        if (stateMachine.GoToDestinationUntilReach(stateMachine.waterPicker.transform.position))
        {
            if (!stateMachine.CheckForAnimEnd("GrabPlace", "Walking")) return;
            stateMachine.waterPicker.GetComponent<Interactable>().OnInteract();
            stateMachine.currentState = new ObserveState();
            OnStateEnd();
        }
    }
}
