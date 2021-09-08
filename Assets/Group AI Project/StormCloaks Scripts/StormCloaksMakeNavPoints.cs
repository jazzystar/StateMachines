using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloaksMakeNavPoints : StormCloaksState {

    public StormCloaksMakeNavPoints(StormCloaksStateController stateController) : base(stateController) { }
    int maxPoints = 0;
    int numPointsMade = 0;
    public override void CheckTransitions()
    {
      
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new StormCloaksChaseState(stateController));
        }
        if (maxPoints < numPointsMade)
        {
            stateController.SetState(new StormCloaksPatrolState(stateController));
        }
    }
    public override void Act()
    {
        if (stateController.destination == null || stateController.ai.DestinationReached())
        {

            stateController.destination.position = stateController.GetRandomPoint();
            stateController.AddNavPoint(stateController.destination.position);
            stateController.ai.SetTarget(stateController.destination);
            numPointsMade++;
        }
    }
    public override void OnStateEnter()
    {
        maxPoints = Random.Range(1,5);
        stateController.ChangeColor(Color.magenta);
        stateController.ai.agent.speed = 2f;
    }
}
