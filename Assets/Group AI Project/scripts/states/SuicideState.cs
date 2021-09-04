using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideState : State
{
    public SuicideState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        
    }
    public override void Act()
    {
        
    }
    public override void OnStateEnter()
    {
        Debug.Log("I give up");
    }
}
