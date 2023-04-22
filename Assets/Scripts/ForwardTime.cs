using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Time travel events. Disable/Enable environents.
    /// </summary>
public class ForwardTime : MonoBehaviour
{
    public Transform player; // Get the transform of the player for position management during time travel effect
    public PlayerMovement _playerMovementScript; // Get the player's disable and enable move function
    public GameObject timeEffect_Forward; // Get the time forward effect prefab
    public GameObject timeEffect_Rewind; // Get the time rewind effect prefab
    public Transform forwardEffectSpawner; // Get the position of the forward time spawner object
    public Transform rewindEffectSpawner; // Get the position of the forward time spawner object
    public float effectTime = 2.0f; // Set the duration of the time jump effect
    public GameObject futureCity; // Get the future city background objects
    public GameObject pastForest; // Get the past forest background objects
    public SpriteRenderer playerColor; // Get the player sprite renderer in order to change the colour to the correct shades
    public Transform timeBigHand; // Reference the time control big hand
    public Transform timeSmallHand; // Referece the time control small hand
    public float handRotateSpeed; // Set the rotate speed of the time hands

    private bool inFuture = false; // Set the environment to the past
    private bool timeJumping = false; // The game will not be time jumping on startup
    private AudioSource timeJumpAudio;
    private Color futureColor = new Color(0.19f, 0.35f, 1f, 1f); // Store the color of the player for when in the future 
    
    void Start()
    {
        timeJumpAudio = GetComponent<AudioSource>(); // Init the audio source for the time jump sound FX
    }

    void Update()
    {
        // Check if time jumping
        if (timeJumping)
        {
            handRotateSpeed += 10f; // Speed up the clock hand rotate speed every frame
            // Check if the player is in the future or the past
            if (inFuture)
            {
                // Rotate hands clockwise
                timeBigHand.Rotate(0f, 0f, -handRotateSpeed * Time.deltaTime);
                timeSmallHand.Rotate(0f, 0f, (-handRotateSpeed * 0.6f) * Time.deltaTime);
            }
            else
            {
                // Rotate hands anticlockwise
                timeBigHand.Rotate(0f, 0f, handRotateSpeed * Time.deltaTime);
                timeSmallHand.Rotate(0f, 0f, (handRotateSpeed * 0.6f) * Time.deltaTime);
            }
        }
        else
        {
            handRotateSpeed = 200f; // Reset the clock hand rotate speed at the end of the time jump
            // Reset cloch hand rotations back to original position
            Quaternion defaultRotation = Quaternion.Euler(0f, 0f, 0f);
            timeBigHand.rotation = Quaternion.Slerp(timeBigHand.rotation, defaultRotation, Time.deltaTime);
            timeSmallHand.rotation = Quaternion.Slerp(timeSmallHand.rotation, defaultRotation, Time.deltaTime);
        }
    }

    public bool GetFuture() // returns true if in the future and false if not (Invoked by the PlayerMovement script for footstep audio FX)
    {
        return inFuture;
    }

    // Invoked from the Button component on the Clock_Face object
    public void TimeActivate() // Begin the time jump and set the correct parameters to the function call based on if in future or in past
    {
        if (!inFuture && !timeJumping) StartCoroutine(TimeJump(timeEffect_Forward, forwardEffectSpawner, !inFuture, futureColor));
        else if (inFuture && !timeJumping) StartCoroutine(TimeJump(timeEffect_Rewind, rewindEffectSpawner, !inFuture, Color.white));
    }

    IEnumerator TimeJump(GameObject timeEffect, Transform spawner, bool isInFuture, Color chosenColor)
    {
        Instantiate(timeEffect, spawner); // Create the time jump FX from the given spawner object
        timeJumpAudio.Play(); // Play the time jump sound FX
        inFuture = isInFuture; // Set whether the player is now in the future or the past
        timeJumping = true; // Set that the player is now currently time jumping and therefore cannot call this function again while true
        _playerMovementScript.SetMove(false, timeJumping); // Disable the player movement input

        yield return new WaitForSeconds(effectTime); // Delay completion of the next set of lines for X seconds

        pastForest.SetActive(!pastForest.activeSelf); // Switch the past forest environment to its other current active state
        futureCity.SetActive(!futureCity.activeSelf); // Switch the future city environment to its other current active state
        playerColor.color = chosenColor; // Change color of the player, depedning on if in future or in past
        timeJumping = false; // The player has completed the jump and is no longer time jumping
        _playerMovementScript.SetMove(true, timeJumping); // Enable the player movement input
    }
}