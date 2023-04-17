using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Move in time events. Disable/Enable environents. Move player object up away from platforms.
    /// </summary>
public class ForwardTime : MonoBehaviour
{
    public Transform player; // Get the player transform of the player for position management during time warp effect
    public PlayerMovement _playerMovementScript; // Get the player's disable and enable move function
    public GameObject timeEffect_Forward; // Get the time forward effect prefab
    public GameObject timeEffect_Rewind; // Get the time rewind effect prefab
    public Transform forwardEffectSpawner; // Get the position of the forward time spawner object
    public Transform rewindEffectSpawner; // Get the position of the forward time spawner object
    public float effectTime = 2.0f; // Set the duration of the time jump effect
    public GameObject futureCity; // Get the future city background objects
    public GameObject pastForest; // Get the past forest background objects
    public SpriteRenderer playerColor; // Get the player sprite renderer in order to change the colour to the correct shades
    
    private bool inFuture = false;
    private bool timeJumping = false;
    private AudioSource timeJumpAudio;
    private Color futureColor = new Color(0.19f, 0.35f, 1f, 1f);

    void Start()
    {
        timeJumpAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if forward in time or in the past
        if (Input.GetKeyDown("space") && !inFuture && !timeJumping) StartCoroutine(TimeJump(timeEffect_Forward, forwardEffectSpawner, !inFuture, futureColor));
        else if (Input.GetKeyDown("space") && inFuture && !timeJumping) StartCoroutine(TimeJump(timeEffect_Rewind, rewindEffectSpawner, !inFuture, Color.white));  
    }

    IEnumerator TimeJump(GameObject timeEffect, Transform spawner, bool isInFuture, Color chosenColor)
    {
        // Funtion parameters to think about:
        // timeEffects (Forward or Rewind)
        // spawners
        // inFuture (true or false)
        // color
        Instantiate(timeEffect, spawner); // To be set by parameter
        timeJumpAudio.Play();
        inFuture = isInFuture; // To be set by parameter
        timeJumping = true;
        _playerMovementScript.SetMove(false, timeJumping);

        yield return new WaitForSeconds(effectTime);

        pastForest.SetActive(!pastForest.activeSelf); 
        futureCity.SetActive(!futureCity.activeSelf);
        playerColor.color = chosenColor; // To be set by parameter
        timeJumping = false;
        _playerMovementScript.SetMove(true, timeJumping);
    }
}
