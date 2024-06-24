using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the movement
    public float dodgeSpeed = 15.0f; // Speed of the dodge
    public float dodgeDuration = 0.15f; // Duration of the dodge
    public float dodgeCooldown = 1.0f; // Cooldown time for the dodge

    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    private bool canDodge = true;
    private bool isDodging = false;
    private Vector3 dodgeDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // set animation
        animator.SetFloat(horizontal, moveHorizontal);
        animator.SetFloat(vertical, moveVertical);

        // Calculate movement vector
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        if (!isDodging)
        {
            // Move the player
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            // Check for dodge input
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDodge && movement.magnitude > 0)
            {
                dodgeDirection = movement.normalized;
                StartCoroutine(Dodge());
            }
        }
    }

    private System.Collections.IEnumerator Dodge()
    {
        canDodge = false;
        isDodging = true;

        float elapsedTime = 0f;

        while (elapsedTime < dodgeDuration)
        {
            transform.Translate(dodgeDirection * dodgeSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDodging = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }
}
