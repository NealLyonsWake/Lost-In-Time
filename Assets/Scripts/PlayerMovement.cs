using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Basic movement code of the player
    /// </summary>
public class PlayerMovement : MonoBehaviour
{
    // Declare the player move speed
    public float speed;

    // Delcare float for animation walking speed
    private float walkingAnimSpeed;

    // Declare the player rigid body 
    [SerializeField] private Rigidbody2D rb;
    
    // Declare the horizontal value
    private float horiz;

    // Declare the animator
    private Animator animator;

    private bool moveAllowed = false;
    private bool awake = false;
    private bool timeJumping = false;
    
    private void Start()
    {
        // Set the animator
        animator = GetComponent<Animator>();            
    }

    public void SetGetUp()
    {
        animator.SetBool("isAwake", true);
    }

    public void SetMove(bool decision, bool timeJump)
    {
        moveAllowed = decision;
        timeJumping = timeJump;
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle") && !awake && !timeJumping)
        {
            awake = true;
            SetMove(true, false);
            
        }
        

        if (moveAllowed) 
        { 
        // Get player input
        horiz = Input.GetAxis("Horizontal");

        // Handle walking animation on player move
        if (rb.velocity.x != 0.0f) { 
            walkingAnimSpeed = rb.velocity.x;
            if (rb.velocity.x > 0.01f)
            {
                // Flip to face right
                animator.SetFloat("Movement", walkingAnimSpeed);
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (rb.velocity.x < -0.01f)
            {
                // Flip to face left
                // Correct animation direction when facing left
                walkingAnimSpeed *= -1;
                animator.SetFloat("Movement", walkingAnimSpeed);
                transform.localScale = new Vector3(-1, 1, 1);
            }           
        }
        else walkingAnimSpeed = 0.0f;
        animator.SetFloat("Movement", walkingAnimSpeed);
        }

       
    }

    private void FixedUpdate()
    {
        // Move the player (includes delta time)
        rb.velocity = new Vector2(horiz * speed, rb.velocity.y);

        if (timeJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1);
        }
    }
}
