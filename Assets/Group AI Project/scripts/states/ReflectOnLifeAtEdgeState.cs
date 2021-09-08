using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectOnLifeAtEdgeState : State
{
    public ReflectOnLifeAtEdgeState(GameBabyStateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        stateController.SetState(new ReflectOnLifeAtEdgeState(stateController));
    }
    public override void Act()
    {
        stateController.reflectOnLife.SetActive(true);
        Debug.Log("I am giving up");
        stateController.ReflectOnLifeAtEdgeSpace.position = new Vector3(28, -2, -3);
        stateController.destination = stateController.ReflectOnLifeAtEdgeSpace;
        stateController.ai.SetTarget(stateController.destination);
        stateController.ai.agent.speed = 3f;
    }
    public override void OnStateEnter()
    {
        Debug.Log("I give up");
    }
}
