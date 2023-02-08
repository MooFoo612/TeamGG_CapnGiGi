using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
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
            if(_health <= 0){
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

    // Also here the velocity should not be changed while this is true but needs to be respected by other physics components (like player controller)
    // Just getting and setting the paramether on the animator
    public bool LockVelocity{
        get{
            return anim.GetBool(AnimationStrings.lockVelocity);
        } set {
            anim.SetBool(AnimationStrings.lockVelocity, value);
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
    }

    public bool Hit(int damage, Vector2 knockback){
        // If player is alive and is not during the invincibilty timer  
        if(IsAlive && !isInvincible){
            // Take damage 
            Health -= damage;
            // Start the invincibility timer to not be able to be hitted again
            isInvincible = true;
            // Update animator parameter 
            anim.SetTrigger(AnimationStrings.hitTrigger);
            // Lock velocity to have a little freeze effect
            LockVelocity = true;
            // Notify other subscribed components that the damagable was hit to handle the knockback, checking fist if is not null
            damageableHit?.Invoke(damage, knockback);
            // Able to be hit 
            return true;
        }
        // Unable to be hit
        return false;
    }
}
