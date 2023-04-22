using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Face the player in the correct direction.
    /// Enables the dialogue trigger to collide with the player when they are near.
    /// </summary>
public class FacePlayer : MonoBehaviour
{
    public Transform target;
    public GameObject speechButton;
    public SpeechBubbleControl _speechBubbleControlScript;
  
    void Update()
    {
        if (target.position.x < transform.position.x) transform.localScale = new Vector3(-1f, 1f, 1f);
        else transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speechButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _speechBubbleControlScript.ResetButton();
        speechButton.SetActive(false);
    }
}