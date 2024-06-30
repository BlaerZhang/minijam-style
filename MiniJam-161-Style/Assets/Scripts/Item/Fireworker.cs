using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworker : MonoBehaviour
{

    public GameObject firework;
    public Animator animator;
    public float speed = 2f;
    public float fireworkLength = 5f;
    public bool isMoving = false; //tell the animator

    public bool fireworked = false;
    public bool fireworking = false;

    private Baseball nearbyItemAuto;
    private Baseball baseball;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("is_moving", isMoving);
    }

    // Update is called once per frame
    void Update()
    {
        // Find the baseball if the parent component is not already referenced
        if (baseball == null)
        {
            
            GameObject childObject = GameObject.FindWithTag("Baseball");
            if (childObject != null)
            {
                Transform parentTransform = childObject.transform.parent;
                if (parentTransform != null)
                {
                    baseball = parentTransform.GetComponent<Baseball>();
                    if (baseball != null)
                    {
                        Debug.Log("Baseball Found");
                        
                    }
                    else
                    {
                        Debug.LogError("Parent does not have a Baseball script");
                    }
                }
                else
                {
                    Debug.LogError("Baseball has no parent");
                }
            }
            else
            {
               // Debug.Log("Fireworker Found No Baseball Tag");
            }
        }



        if (baseball != null && (baseball.settled || baseball.following))
        {//then move towards it
            isMoving = true;
            animator.SetBool("is_moving", isMoving);
            float step = speed * Time.deltaTime;
            Vector2 direction = baseball.transform.position - transform.position;
            transform.position =
                Vector2.MoveTowards(transform.position, baseball.transform.position, step);
            if (direction.x >= 0)
            {
                animator.SetBool("going_right", true);
            }
            else
            {
                animator.SetBool("going_right", false);
            } 
            
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;
        Debug.Log("got in fireworker:" + other.ToString());
        //ShootFirework();

        Baseball itemAuto = other.GetComponent<Baseball>();
        if (itemAuto != null && itemAuto != nearbyItemAuto)
        {
            Debug.Log("Fireworker Trigger Success");
            nearbyItemAuto = itemAuto;

            //TODO: temp interact, to be optimized
            itemAuto.Interact(this.gameObject);
            StartCoroutine(ShootFirework());
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (!other.CompareTag("Interactable")) return;
    //    //Debug.Log("got in: " + other.name);

    //    IItemAuto itemAuto = other.GetComponent<IItemAuto>();
    //    if (itemAuto != null && itemAuto != nearbyItemAuto)
    //    {
    //        //Debug.Log("Trigger success");
    //        nearbyItemAuto = itemAuto;

    //        //TODO: temp interact, to be optimized
    //        nearbyItemAuto.Interact(this.gameObject);
    //        if (itemAuto.getComponent<Baseball>) {
    //          StartCoroutine(ShootFirework());
    //        }
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (!other.CompareTag("Interactable")) return;
    //    //Debug.Log("got out:" + other.name);

    //    IItemAuto itemAuto = other.GetComponent<IItemAuto>();
    //    if (itemAuto != null && itemAuto == nearbyItemAuto)
    //    {
    //        nearbyItemAuto = null;
    //    }
    //}






    private IEnumerator ShootFirework()
    {
        Debug.Log("Firework shot");
        isMoving = false;
        animator.SetBool("is_moving", isMoving);
        fireworked = true;
        fireworking = true;
        firework.SetActive(true);
        //wait
        yield return new WaitForSeconds(fireworkLength);
        //firework ends
        fireworking = false;
        firework.SetActive(false);
    }
}
