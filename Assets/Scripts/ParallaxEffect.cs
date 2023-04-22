using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Parallex effect for the environment background and sky.
/// Cite: Unity Parallax Tutorial - How to infinite scrolling background, Dani:
/// https://www.youtube.com/watch?v=zit45k6CUMk
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    // Deterine the length and start position of the environment sprites
    private float length;
    private float startPos;

    // Get the camera
    private Transform cam;

    // Variable on how much parallax to apply
    public float parallax;


    void Start()
    {
        // Init the cam
        cam = Camera.main.transform;

        // Set the start position of the environment sprite
        startPos = transform.position.x;

        // Set the length of the environment sprite using the sprite renderer component
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Make a temporary variable that measures the distance from the start point
        float dist = (cam.position.x * parallax);

        // Change the position reletive to cam position, start position and parallax level
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
