using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehavior : MonoBehaviour
{
    BehaviorTree tree;
    NavMeshAgent agent;
    public GameObject car;
    public GameObject diamond;
    public GameObject door;
    public GameObject frontDoor;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;
    Node.Status treeStatus = Node.Status.RUNNING;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tree = new BehaviorTree();
        Sequence steal = new Sequence("Steal Something");
        Leaf goToBackDoor = new Leaf("Go To Back Door", GoToBackDoor);
        Leaf goToFrontDoor = new Leaf("Go To Front Door", GoToFrontDoor);

        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van", GoToCar);

        Selector openDoor = new Selector("Open Door");
        openDoor.AddChild(goToFrontDoor);
        openDoor.AddChild(goToBackDoor);

        steal.AddChild(openDoor);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);
        tree.PrintTree();


    }
    //if agent is moving, he's doing a behavior
    public Node.Status GoToDiamond()
    {
        return GoToLocation(diamond.transform.position);

    }
    public Node.Status GoToCar()
    {
        return GoToLocation(car.transform.position);
    }
    public Node.Status GoToFrontDoor()
    {
       return GoToDoor(frontDoor);
    }
    public Node.Status GoToBackDoor()
    {
        return GoToDoor(door);
    }

    public Node.Status GoToDoor(GameObject d)
    {
        Node.Status s = GoToLocation(d.transform.position);
        if (s == Node.Status.SUCCESS)
        {
            //made it to the door?  This triggers when you make it to the front door as soon as the next door is passed in, fix this
            if (!door.GetComponent<Lock>().isLocked)
            {
                door.SetActive(false);
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        }
        else return s;
    }


    Node.Status GoToLocation(Vector3 dest)
    {
        float distanceToTarget = Vector3.Distance(dest, transform.position);
        if (state == ActionState.IDLE) //runs once when your idle, then skips
        {
            agent.SetDestination(dest);
            state = ActionState.WORKING;

        }

        else if (Vector3.Distance(agent.pathEndPosition, dest) >= 2)
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarget < 2)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING; 
           
       
    }

    void Update()
    {
        if(treeStatus == Node.Status.RUNNING)
            treeStatus = tree.Process(); //we want to stop looping and get out of sequences when appropriate

    }
}
