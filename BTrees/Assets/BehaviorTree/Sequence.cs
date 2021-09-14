using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
public Sequence(string n)
    {
        name = n;
    }
    public override Status Process()
    {
        //need to loop through all children, but we can't actually loop we set it running and see if its still running the next looping tick
        Status childStatus = children[currentChild].Process(); //picks up the first child starting at 0
        //Debug.Log("Processing " + children[currentChild].name);
        if (childStatus == Status.RUNNING) return Status.RUNNING;
        if (childStatus == Status.FAILURE) return childStatus; //if one children action has failed, this whole node fails and we just return without running anything else

        currentChild++;
        if(currentChild >= children.Count)
        {
            //we've made it to the end of our list
            currentChild = 0;
            return Status.SUCCESS;
        }
        return Status.RUNNING;
    }
}

