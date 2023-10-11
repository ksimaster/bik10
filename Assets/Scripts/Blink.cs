using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blink : MonoBehaviour
{

    public bool animationEnabled = true;
    public float blinkDuration = 3f;
    public float textDuration = 3f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;

    private float elapsedTime;
    private float elapsedTextTime;
    private Image fadeImage;
    private bool reverse = false;

    Color transparentBlack = new Color(0f, 0f, 0f, 0f);
    Color solidBlack = new Color(0f, 0f, 0f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = gameObject.GetComponent<Image>();
        fadeImage.color = transparentBlack;
    }
    

    // Update is called once per frame
    void Update()
    {

        if (animationEnabled) {
            elapsedTime += Time.deltaTime;

            float actualAlpha = elapsedTime / blinkDuration;
            fadeImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(minAlpha, maxAlpha, actualAlpha));

            if (elapsedTime > blinkDuration) {

                if (elapsedTextTime < textDuration)
                {
                    elapsedTextTime += Time.deltaTime;
                }
                else if (reverse ==  false)
                {
                    float temp = maxAlpha;
                    maxAlpha = minAlpha;
                    minAlpha = temp;
                    elapsedTime = 0f;
                    elapsedTextTime = 0f;
                    reverse= true;
                } else
                {
                    animationEnabled = false;
                }
            } 
        }
        
        
        
    }
}
