using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
        Collider2D Col;
        private Animator animatorB;

        public Vector2 knockback = new Vector2(0, 0);
        public int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
                Col = GetComponent<Collider2D>();
                animatorB = gameObject.GetComponent<Animator>();
                animatorB.SetTrigger("Ignite");
                StartCoroutine("Detonator");
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

        private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision gameObject

        Damageable damageable = collision.GetComponent<Damageable>();
        // Check if can be hit 
        if(damageable != null){
            animatorB.SetTrigger("Boom");
            // Reverse the knockback vector direction depending on localScale 
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            // Testing: if hit successfully debug the hit 
            if(gotHit){
                Debug.Log(collision.name + " hit for " + damage);
                // Destroy the projectile
                Destroy(gameObject, 0.5f);
            }
        }
    }

        private IEnumerator Detonator() {
            yield return new WaitForSeconds(1.5f);
            // Col.Radius = 0.5;


            animatorB.SetTrigger("Boom");

            Destroy(gameObject, 0.5f);
        }
}
