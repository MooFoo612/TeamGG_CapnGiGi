using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthBarText;
    public Slider healthSlider;
    Damageable playerDamageable;

    private void Awake()
    {
        // Find the player and get the damagbeable script component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();

        // Testing
        if(playerDamageable == null)
        {
            Debug.Log("No PLayer found in the scene");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Update the health slider and text and set them with the default values
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        // Return the percentage to update the health bar
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        // Update the health slider and text 
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = newHealth + " / " + maxHealth;
    }

    IEnumerator HealthIncrease(int currentHealth, int newHealth)
    {
        for (int x = playerDamageable.Health; x <= newHealth; x++)
        {
            healthSlider.value = x;
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    IEnumerator HealthDecrease(int currentHealth, int newHealth)
    {
        for (int x = playerDamageable.Health; x >= newHealth; x--)
        {
            healthSlider.value = x;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
