using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball : MonoBehaviour, IItem, IItemAuto

{
    public GameObject Player;
    public GameObject Fireworker;
    public Vector2 destination;
    public float speed = 2f;
    public bool settled = false;
    public bool following = false;

    public bool interactable = true;
    private bool pickable = true;


    public float FollowOffsetX = -0.5f;
    public float FollowOffsetY = 1f;
    public float DestiRangeY = 900f;
    public float DestiRangeX = 1900f;

    void Start()
    {
        destination = GetRandomPosition(DestiRangeX, DestiRangeY);
        Player = FindObjectOfType<PlayerMovement>().gameObject;
        Fireworker = GameObject.FindGameObjectWithTag("FireworkerInteract");
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, destination) < 0.001f)
        {
            settled = true;
        }

        if (!settled && !following) //
        {
            float step = speed * Time.deltaTime;
            transform.position =
                Vector2.MoveTowards(transform.position, destination, step);
        }
        else if (following)
        {
            float step = speed * Time.deltaTime;
            Vector2 targetPosition =
                new Vector2(Player.transform.position.x + FollowOffsetX,
                Player.transform.position.y + FollowOffsetX);
            transform.position =
                Vector2.MoveTowards(transform.position, targetPosition, step);
        }
    }


    public void Interact() //triggered by player pressing F
    {
        if (following)
        {
            Debug.Log("Baseball Ended Following");
            following = false;
            pickable = false; // not able to pick up again
            settled = true; // not continuing to move to destination
        }
    }

    public void Interact(GameObject other) //triggered by player or fireworker
    {
        if (other == Player && pickable && !following) //player picking up
        {
            Debug.Log("Baseball Started Following");
            following = true;
        }
        if (other == Fireworker)
        {
            // Debug.Log("meet fireworker");
            Destroy(this.gameObject);
        }
    }


    /* get the random position */
    private Vector2 GetRandomPosition(float width, float height)
    {
        float x = Random.Range(20, width);
        float y = Random.Range(0, height);
        // Debug.Log("destination" + x + "," + y);
        Vector3 worldPosition = Camera.main.
            ScreenToWorldPoint(new Vector2(x, y));
        return worldPosition;
    }

    
}


