using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator anim;
    [SerializeField] private bool isInvincible = false;
    public float invincibilityTime = 0.25f;
    private float timeSinceHit = 0;
    private int _maxHealth;
    public int MaxHealth{
        get{
            return _maxHealth;
        } set {
            _maxHealth = value;
        }
    }
    [SerializeField] private int _health = 100;
    public int Health{
        get {
            return _health;
        } set {
            _health = value;
            // If health is less than 0 
            if(_health < 0){
                // Kill the character 
                IsAlive = false;
            }
            // the charact
        }
    }


    [SerializeField] private bool _isAlive = true;
    public bool IsAlive{
        get{
            return _isAlive;
        } set {
            _isAlive = value;
            anim.SetBool(AnimationStrings.isAlive, value);
        }
    }

    void Awake(){
        anim = GetComponent<Animator>();
    }
    private void Update(){
        // If is in invincibilty timer 
        if(isInvincible){
            // If timer is finished
            if(timeSinceHit > invincibilityTime){
                // Remove invincibility
                isInvincible = false;
                // Reset the timeSinceHit timer 
                timeSinceHit = 0;
            }
            // Update the timeSinceHit timer 
            timeSinceHit += Time.deltaTime;
        }
        // Testing shit 
        Hit(10);
    }

    public void Hit(int damage){
        // If player is alive and is not during the invincibilty timer  
        if(IsAlive && !isInvincible){
            // Take damage 
            Health -= damage;
            // Start the invincibility timer to not be able to be hitted again
            isInvincible = true;

        }
    }
}
