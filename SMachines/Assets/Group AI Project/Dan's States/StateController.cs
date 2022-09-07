using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public GameObject navPointsParent;
    public State currentState;
    public GameObject[] navPoints;
    public GameObject enemyToChase;
    public int navPointNum;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 5;
    public GameObject wanderP;
    public GameObject newNavPoint;

    public GameObject enemySelf;

    public float distanceObstacle;
    public bool seen = false;

    public GameObject eyeCone;

    void Start()
    {
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        childrenRend = GetComponentsInChildren<Renderer>();
        //our second thought was instead of sphere raycast was to detect using an invisible cone on it's head
       // eyeCone.GetComponent(MeshRenderer).enabled = false;
        SetState(new PatrolState(this));
    }

    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();

    }
        //below is our sphere raycast
        /*we had a couple issues trying to get to the center of the ai 
          character controller and so we kind just got the position of the 
          ai itself and got the height from the capusle collider on the ai ctrler
          
          to see where we got the sphere raycast code see SeeState*/

       /* RaycastHit hit;

        Vector3 p1 = transform.position + enemySelf.transform.position;
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1,  enemySelf.GetComponent<CapsuleCollider>().height/2, transform.forward, out hit, 10))
        {
            distanceToObstacle = hit.distance;
            //Debug.Log(hit.distance);
            //distanceObstacle = distanceToObstacle;
            if(distanceToObstacle >= 2f){
                seen = true;
                distanceObstacle = distanceToObstacle;
            } 
            else{
                seen = false;
            }
            //Debug.Log("Sphere Casted");
        }*/
    
    public Transform GetNextNavPoint()
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
        return navPoints[navPointNum].transform;
    }
    public Transform GetWanderPoint()
    {
        //This could be done more efficeintly without introducing a empty game object
        Vector3 wanderPoint = new Vector3(Random.Range(-2f, 2f), 1.5f, Random.Range(-2f, 2f));
        GameObject go = Instantiate(wanderP, wanderPoint, Quaternion.identity);
        return go.transform;
    }
    public Vector3 GetRandomPoint()
    {
        Vector3 ran = new Vector3(Random.Range(-detectionRange, detectionRange), 1.5f, Random.Range(-detectionRange, detectionRange));
        return ran;
    }

    public void AddNavPoint(Vector3 pos)
    {
        GameObject go = Instantiate(newNavPoint, pos, Quaternion.identity);
        go.transform.SetParent(navPointsParent.transform);
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        

    }

    public void ChangeColor(Color color)
    {
        foreach(Renderer r in childrenRend)
        {
            foreach(Material m in r.materials)
            {
                m.color = color;
            }
        }
    }
    public bool CheckIfInRange(string tag)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < detectionRange)
                {
                    enemyToChase = g;
                    return true;
                }
            }
        }
        return false;
    }

    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

     public bool inSight(string tag)
        {
            return eyeCone.GetComponent<VisionDetection>().seesPlayer;
            /*basically chase state except uses the info gotten from
              our sphere raycast
              
              it tecnically works but sometimes it gets stuck or the detection goes but then goes away very quickly
              prob cause the raycast is in update...
              */
        //     enemies = GameObject.FindGameObjectsWithTag(tag);
        //     if (enemies != null && seen == true){
        //          foreach (GameObject g in enemies)
        //         {
        //             if(distanceObstacle < detectionRange){
        //                 return true;
        //             }
        //         }
        //      }
        // return false;
        }
}
