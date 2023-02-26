using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public NpcStateMachine stateMachine;

    virtual public void OnStart(NpcStateMachine fsm)
    {
        stateMachine = fsm;
    }

    abstract public void OnUpdate();

    virtual public void OnStateEnd()
    {
        stateMachine.currentState.OnStart(stateMachine);
    }
}
