using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    // Create a List of collider to store the player's one once detected
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;
    void Awake(){
        col = GetComponent<Collider2D>();

    }
    void OnTriggerEnter2D(Collider2D collision){
        // If a collider enter into the detection zone add it to the List (it can only be triggered by the player because I disabled the collision for all the layers except the player's one for the enemyHitbox layer)
        detectedColliders.Add(collision);
    }

    void OnTriggerExit2D(Collider2D collision){
        // If a collider exit from the detection zone delete it from the List (it can only be triggered by the player because I disabled the collision for all the layers except the player's one for the enemyHitbox layer)
        detectedColliders.Remove(collision);

    }
}
