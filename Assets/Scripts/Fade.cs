using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///
/// </summary>
public class Fade : MonoBehaviour
{
    public SpriteRenderer titleImage; // reference to the title image
    public float fadeTime = 1.5f; // time it takes to fade in/out
    public float displayTime = 3.0f; // time to display the title before fading out

    public PlayerMovement _playerMoveScript;
    
    private Color startColor = Color.clear; // initial color of the title image
    private Color endColor = Color.white; // color to fade to

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        titleImage.color = startColor;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            titleImage.color = Color.Lerp(startColor, endColor, t / fadeTime);
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            titleImage.color = Color.Lerp(endColor, startColor, t / fadeTime);
            yield return null;
        }

        titleImage.color = startColor;
        _playerMoveScript.SetGetUp();
        Destroy(gameObject);
    }
}


