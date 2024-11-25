using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    TMP_Text _text;
    public float minAlpha = 0.3f; // Minimum alpha value
    public float maxAlpha = 1.0f; // Maximum alpha value
    public float blinkSpeed = 1.5f; // Speed of Blink
    
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        StartCoroutine(BlinkAlpha());
    }
    
    IEnumerator BlinkAlpha()
    {
        Color originalColor = _text.color; // Save the original color
        float alpha = maxAlpha;
        bool fadingOut = true;

        while (true)
        {
            if (fadingOut)
            {
                alpha -= Time.deltaTime * blinkSpeed;
                if (alpha <= minAlpha)
                {
                    alpha = minAlpha;
                    fadingOut = false; // Start fading in
                }
            }
            else
            {
                alpha += Time.deltaTime * blinkSpeed;
                if (alpha >= maxAlpha)
                {
                    alpha = maxAlpha;
                    fadingOut = true; // Start fading out
                }
            }

            _text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // Wait for the next frame
        }
    }
}
