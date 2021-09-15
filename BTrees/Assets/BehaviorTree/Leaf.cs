using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick(); //delegate that takes a method that returns a status
    public Tick ProcessMethod;
    public Leaf() { }
    public Leaf(string n, Tick pm) //we pass a pointer to our method when we construct a leaf, then in process we run the method we passed in.
    {
        name = n;
        ProcessMethod = pm;

    }
    public override Status Process()
    {
        if(ProcessMethod != null)
            return ProcessMethod();
        return Status.FAILURE;
    }
}
