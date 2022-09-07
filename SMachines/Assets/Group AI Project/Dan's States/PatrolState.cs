using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {


    public PatrolState(StateController stateController) : base(stateController) { }
   
    public override void CheckTransitions()
    {
        if (stateController.inSight("Player"))
        {
            stateController.SetState(new SeeState(stateController));
        }
        
    }
    public override void Act()
    {
        if(stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetNextNavPoint();
            stateController.ai.SetTarget(stateController.destination);
        }
    }
    public override void OnStateEnter()
    {
        stateController.destination = stateController.GetNextNavPoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 1f;
        }
        stateController.ai.SetTarget(stateController.destination);
        stateController.ChangeColor(Color.blue);
    }

}
