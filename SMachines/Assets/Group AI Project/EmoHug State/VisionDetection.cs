using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetection : MonoBehaviour
{
    public GameObject AI;
    public bool seesPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player"){
            seesPlayer = true;
             Debug.Log("colliding");
            AI.GetComponent<StateController>().ChangeColor(Color.red);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.name == "Player"){
            seesPlayer = false;
             Debug.Log("not colliding");
            AI.GetComponent<StateController>().ChangeColor(Color.yellow);
        }
    }
}
