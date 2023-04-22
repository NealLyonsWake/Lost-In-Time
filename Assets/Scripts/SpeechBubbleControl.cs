using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// UI script for mouse control for speech alert system for dialogue call.
    /// Mimics a standard UI button which calls the dialogue object.
    /// </summary>
public class SpeechBubbleControl : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public GameObject dialogue;
    public DialogueManager _dialogueManagerScript;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ResetButton()
    {
        sr.sprite = defaultSprite;
    }

    // The following functions mimic active button FX
    private void OnMouseOver()
    {
        sr.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        sr.sprite = defaultSprite;
    }

    private void OnMouseDown()
    {
        dialogue.SetActive(true);
        _dialogueManagerScript.SetDialogueLine(gameObject.name);
        gameObject.SetActive(false);
    }
}
