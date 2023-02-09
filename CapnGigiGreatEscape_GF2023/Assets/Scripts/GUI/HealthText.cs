using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    // Pixel per second
    public Vector3 moveSpeed  = new Vector3(0, 75, 0);
    public float timeToFade = 1f;
    private float timeElapsed = 0f;
    TextMeshProUGUI textMeshPro;
    RectTransform textTransform;
    private Color startColor;

    private void Awake(){
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update(){
        // Move the damage text up 
        textTransform.position += moveSpeed * Time.deltaTime;
        // Update the timer 
        timeElapsed += Time.deltaTime;
        // If timer not finisehd yet 
        if(timeElapsed < timeToFade){
            //Each update, update the text color removing the alpha to make it fade and get transparent after the damage it's been showed 
            float fadeAlpha  = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        } else {
            // Remove the text 
            Destroy(gameObject);
        }
    }

}
