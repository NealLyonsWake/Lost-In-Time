using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Follow the player transform on the x-axis (the y-axis will remain the same for this game)
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    // Declare target that the camera should follow
    [SerializeField] private Transform target;

    // Declare the time it takes for the camera to reach the target
    public float smoothTime;

    // Declare for reference later
    private Vector3 velocity = Vector3.zero;


    void FixedUpdate()
    {
        // Create the target position variable
        Vector3 targetPosistion = new Vector3(target.position.x, transform.position.y, transform.position.z);
        
        // Smooth movement to target of player's x position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosistion, ref velocity, smoothTime);
    }
}

 