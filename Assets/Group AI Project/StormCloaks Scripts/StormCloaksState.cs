using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StormCloaksState {

    protected StormCloaksStateController stateController;
    //constructor
    public StormCloaksState(StormCloaksStateController stateController)
    {
        this.stateController = stateController;
    }
    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }



	
}
