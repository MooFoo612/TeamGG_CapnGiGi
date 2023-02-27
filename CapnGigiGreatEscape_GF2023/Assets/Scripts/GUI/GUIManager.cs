using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;

    private void Awake(){
        //  gameCanvas = FindObjectOfType<Canvas>();
        
    }

    private void OnEnable() {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable() {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived){
        // Create text at character hit using a camera function
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // Instantiate a copy of the damage text prefab
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        // Update the text with the current damage converting the int to a string
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored){
        // Create text at character hit using a camera function
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // Instantiate a copy of the damage text prefab
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        // Update the text with the current damage converting the int to a string
        tmpText.text = healthRestored.ToString();
    }
}
