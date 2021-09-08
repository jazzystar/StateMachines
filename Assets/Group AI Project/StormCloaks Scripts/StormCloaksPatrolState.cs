using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloaksPatrolState : StormCloaksState
{


    public StormCloaksPatrolState(StormCloaksStateController stateController) : base(stateController) { }
   
    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new StormCloaksChaseState(stateController));
        }
        
    }
    public override void Act()
    {
        if(stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetNextNavPoint();
            stateController.ai.SetTarget(stateController.destination,false,true);
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
