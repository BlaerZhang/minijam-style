using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    [HideInInspector] public Vector2 InitialPosition;
    public GameObject fireworkerObj;
    public GameObject treehouseObj;
    public GameObject kidShootable;
    public GameObject kidCollider;
    public float speed = 2f;
    public float timeInTree = 2f;
    public float OffsetX = 1f; //offset for staging the kids around the firework
    public float OffsetY = 0f;

    private bool idle = true;
    bool watching = false;
    bool beenInHouse = false;
    bool inHouse = false;
    bool goingBack = false;

    private Fireworker fireworker;
    private Treehouse treehouse;

    // Start is called before the first frame update
    void Start()
    {
        fireworker = fireworkerObj.GetComponent<Fireworker>();
        treehouse = treehouseObj.GetComponent<Treehouse>();
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireworker.fireworking)
        {
            idle = false;
        }

        if (!idle && !inHouse)
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
                if (!beenInHouse)
                {
                    //go to tree house
                    
                    transform.position =
                    Vector2.MoveTowards(transform.position,
                    treehouse.transform.position, step);

                    if (Vector2.Distance(transform.position,
                        treehouse.transform.position) < 0.1f)
                    {
                        // Debug.Log("Got to tree");
                        //get to house
                        beenInHouse = true;
                        //animation
                        StartCoroutine(TreeHouseAnimation());
                    }
                }
                else
                {
                    //go back
                    transform.position =
                    Vector2.MoveTowards(transform.position,
                    InitialPosition, step);
                    if (Vector2.Distance(InitialPosition, transform.position) < 0.01f) //get back
                    {
                        //stay until next firework
                        beenInHouse = false;
                        idle = true;
                    }
                }
            }
        }
    }

    private IEnumerator TreeHouseAnimation() {
        // Debug.Log("Tree triggered");
        inHouse = true;
        kidShootable.SetActive(false);
        kidCollider.SetActive(false);
        treehouse.GetKidsIn();
        yield return new WaitForSeconds(timeInTree);
        // Debug.Log("Tree triggered again.");
        treehouse.GetKidsOut();
        inHouse = false;
        kidShootable.SetActive(true);
        kidCollider.SetActive(true);

    }
}
