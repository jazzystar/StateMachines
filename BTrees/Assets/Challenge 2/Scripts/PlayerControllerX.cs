using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    GameObject currentDog;
    public float inputTimer;
    public float inputTimeLimit = 1f;



void ChangeColor()
    {
        if (inputTimer > .4)
        {
            Renderer[] rends = currentDog.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rends)
            {
                Material[] mats = r.materials;
                foreach (Material m in mats)
                {
                    m.color = Color.magenta;

                }
            }
        }
 
        
    }

    private void Start()
    {
       
        inputTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        inputTimer += Time.deltaTime;
        if (inputTimer > inputTimeLimit)
        {

            // On spacebar press, send dog
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputTimer = 0;
                currentDog = Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            }
        }
        else if(currentDog !=null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ChangeColor();
                float speedUp = 0;
                speedUp += Time.deltaTime + 1;
                MoveForwardX mv = currentDog.GetComponent<MoveForwardX>();
                mv.speed += speedUp;
                
            }
            else
            {
                MoveForwardX mv = currentDog.GetComponent<MoveForwardX>();
                mv.speed = 30;
                //restore colors

            }
        }
    }
}
