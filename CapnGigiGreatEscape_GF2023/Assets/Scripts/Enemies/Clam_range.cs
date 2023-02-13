using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam_range : MonoBehaviour
{
    #region Variables
    public GameObject target;
    public GameObject enemy;
    public GameObject spawn;
    public  GameObject projectile;
    private Animator animatorEN;
    private SpriteRenderer enemySR;
    bool targetDetected = false;
    #endregion
    #region initalisation
    // Start is called before the first frame update
    void Start()
    {
        animatorEN = enemy.GetComponent<Animator>();
        enemySR = enemy.GetComponent<SpriteRenderer>();
    }
    #endregion

    #region shoot
    void FixedUpdate(){
        //If statement ensures player is in range and peals spawn one at a time
        if (targetDetected && GameObject.Find("Pearl(Clone)")== null){
            //animates shooting
            animatorEN.SetTrigger("Shoot");
            // Spawns pearlsls
            Vector2 velocity= new Vector2(-5,0);

            GameObject spawnedProjectile = Instantiate(projectile,
                                        spawn.transform.position,
                                        Quaternion.identity);

         Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();

            rb.position = spawn.transform.position;
            rb.velocity = velocity;
         }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(target.name)){
            //Detects player
        targetDetected = true;
    }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(target.name)){
            //Detects player left
        targetDetected = false;
    }
    }
    #endregion
}
