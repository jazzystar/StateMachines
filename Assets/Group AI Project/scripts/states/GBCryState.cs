using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBCryState : State
{


    public GBCryState(GameBabyStateController stateController) : base(stateController) { }

    float cryTimeLimit;
    float cryTimer;
    
    public override void CheckTransitions()
    {
        if (stateController.cryCounter > 2)
        {
            stateController.ai.isCrying = false;
            stateController.SetState(new ReflectOnLifeAtEdgeState(stateController));
        }
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.ai.isCrying = false;
            stateController.SetState(new GBChaseState(stateController));
        }
        if (cryTimer > cryTimeLimit)
        {
            stateController.ai.isCrying = false;
            stateController.SetState(new GBWanderState(stateController));
        }

        Debug.Log(stateController.cryCounter);


    }
    public override void Act()
    {
        cryTimer += Time.deltaTime;
        if (stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetWanderPoint();
            stateController.ai.SetTarget(stateController.destination);
        }

        if (cryTimer > cryTimeLimit)
        {
            stateController.ai.isCrying = false;
        }
    }
    public override void OnStateEnter()
    {
        cryTimer = 0f;
        cryTimeLimit = 3f;
        stateController.cryCounter++;
        Debug.Log("I am sad.");
       if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 0f;
            stateController.ChangeColor(Color.grey);
            stateController.ai.isCrying = true;
            Debug.Log("I should be crying");
        }
        
    }
}