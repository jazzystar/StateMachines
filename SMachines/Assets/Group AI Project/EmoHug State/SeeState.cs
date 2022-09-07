using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeState : State {

    public SeeState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (!stateController.inSight("Player"))
        {
            stateController.SetState(new WanderState(stateController));
            Debug.Log("Exited See State");
        }
    }
    public override void Act()
    {
        if(stateController.enemyToChase != null)
        {
            stateController.destination = stateController.enemyToChase.transform;
            stateController.ai.SetTarget(stateController.destination);
           // Debug.Log("Acted See State");
        }
    }
    public override void OnStateEnter()
    {
        stateController.ChangeColor(Color.red);
        stateController.ai.agent.speed = .5f;
        //change the color so visual change of see
        //Debug.Log("Entered See State");
    }

    /*
    https://docs.unity3d.com/ScriptReference/Physics.SphereCast.html
    */
}
