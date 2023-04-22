using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Detects items and manages events on collision with them, such as sound FX and clean up.
    /// </summary>
public class ItemFinder : MonoBehaviour
{
    public PlayerMovement _playerMovementScript;
    private new AudioSource audio;
  
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Time_Component")
        {
            audio.Play();
            Destroy(collision.gameObject);
            _playerMovementScript.SetNumberCollected();
        }
    }
}