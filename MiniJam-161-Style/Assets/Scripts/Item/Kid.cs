using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    public Vector2 InitialPosition;
    public GameObject fireworkerObj;
    public GameObject treehouse;
    public float speed = 2f;
    public float OffsetX = 1f; //offset for staging the kids around the firework
    public float OffsetY = 0f;

    private bool idle = true;
    bool watching = false;
    bool BeenInHouse = false;
    bool goingBack = false;

    private Fireworker fireworker;

    // Start is called before the first frame update
    void Start()
    {
        fireworker = fireworkerObj.GetComponent<Fireworker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireworker.fireworking)
        {
            idle = false;
        }

        if (!idle)
        {
            float step = speed * Time.deltaTime;
            //move
            //to fireworker
            if (fireworker.fireworking)
            {
                //could use some adjustment for each kid
                Vector2 target = new Vector2(fireworker.transform.position.x + OffsetX,
                fireworker.transform.position.y + OffsetY);
                transform.position =
                Vector2.MoveTowards(transform.position, target, step); // to firework
                if (Vector2.Distance(InitialPosition, target) < 0.5f) // more tolerance here
                {
                    // watch
                    watching = true;
                    //animation
                }
            }
            else
            {
                watching = false; // not watching firework
                if (!BeenInHouse)
                {
                    //go to tree house
                    
                    transform.position =
                    Vector2.MoveTowards(transform.position,
                    treehouse.transform.position, step);

                    if (Vector2.Distance(InitialPosition,
                        treehouse.transform.position) < 0.1f)
                    {
                        // get to house
                        BeenInHouse = true;
                        //animation
                    }
                }
                else
                {
                    //go back
                    if (Vector2.Distance(InitialPosition, transform.position) < 0.01f) //get back
                    {
                        idle = true;
                    }
                }
            }
        }
    }
}
