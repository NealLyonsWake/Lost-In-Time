using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Movement code of the player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public GameObject dialogueBox;
    public ForwardTime _forwardTimeScript;
    public AudioClip forestStep1;
    public AudioClip forestStep2;
    public AudioClip cityStep1;
    public AudioClip cityStep2;  
    public float speed;
       
    private float walkingAnimSpeed;
    [SerializeField] private Rigidbody2D rb;

    private float horiz;
    private Animator animator;
    private AudioSource playerAudio;
    private bool moveAllowed = false;
    private bool awake = false;
    private bool timeJumping = false;
    private bool isInFuture = false;
    private int numberCollected = 0;

    private void Start()
    {        
        animator = GetComponent<Animator>(); // Init the animator        
        playerAudio = GetComponent<AudioSource>(); // Init the audio source
    }

    public void SetGetUp()
    {
        animator.SetBool("isAwake", true); // Trigger the character animation to get up from the ground
    }

    public void SetMove(bool decision, bool timeJump) // Enables or disables the player movement control 
    {
        moveAllowed = decision;
        timeJumping = timeJump;
        horiz = 0f;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        walkingAnimSpeed = 0.0f;
        animator.SetFloat("Movement", walkingAnimSpeed);
        isInFuture = _forwardTimeScript.GetFuture();
    }

    public void SetNumberCollected() // Increment the number of items collected (Invoked by the child object)
    {
        numberCollected++;
    }

    public void FootStepAudio() // Manage sound FX for the player's movement footsteps
    {
        int r = Random.Range(0, 2);
        if (!isInFuture)
        {            
            if (r < 1) playerAudio.clip = forestStep1;
            else playerAudio.clip = forestStep2;
        }
        else
        {
            if (r < 1) playerAudio.clip = cityStep1;
            else playerAudio.clip = cityStep2;
        }
        playerAudio.Play();
    }

    void Update()
    {
        // Make the initial dialogue box appear with the intro speech
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle") && !awake && !timeJumping)
        {
            awake = true;
            dialogueBox.SetActive(true);
        }

        if (moveAllowed)
        {
            horiz = Input.GetAxis("Horizontal"); // Get player input

            // Handle walking animation when the player moves
            if (rb.velocity.x != 0.0f)
            {
                walkingAnimSpeed = rb.velocity.x;
                if (rb.velocity.x > 0.01f)
                {
                    // Flip player transform to face right
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (rb.velocity.x < -0.01f)
                {
                    // Flip player transform to face left
                    walkingAnimSpeed *= -1;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else walkingAnimSpeed = 0.0f;
            animator.SetFloat("Movement", walkingAnimSpeed);
        }
        // Once the player has collected 2 items
        if (numberCollected >= 2)
        {
            numberCollected = -1;
            dialogueBox.SetActive(true);
            SetMove(false, false);
        }
    }

    private void FixedUpdate()
    {
        // Move the player
        rb.velocity = new Vector2(horiz * speed, rb.velocity.y);

        // Move player up to avoid clashing with hills on forest re-entry
        if (timeJumping) rb.velocity = new Vector2(rb.velocity.x, 1);
    }
}