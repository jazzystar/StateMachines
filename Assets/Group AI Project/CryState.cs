using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryState : State
{


    public CryState(StateController stateController) : base(stateController) { }

    float cryTimeLimit;
    float cryTimer;
    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new ChaseState(stateController));
        }
        if (cryTimer > cryTimeLimit)
        {
            stateController.SetState(new MakeNavPoints(stateController));
        }

    }
    public override void Act()
    {
        cryTimer += Time.deltaTime;
        if (stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetWanderPoint();
            stateController.ai.SetTarget(stateController.destination);
        }
    }
    public override void OnStateEnter()
    {
        cryTimer = 0f;
        cryTimeLimit = 3f;
        Debug.Log("I am sad.");
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 0f;
        }
        stateController.ChangeColor(Color.grey);
    }
}