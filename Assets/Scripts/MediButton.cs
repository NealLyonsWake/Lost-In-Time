using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Manage the medi vending machine button UI and usage.
    /// </summary>
public class MediButton : MonoBehaviour
{
    public Sprite defaultButton;
    public GameObject speechButton;
    public SpeechBubbleControl _speechBubbleControlScript;

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