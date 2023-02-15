using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    #region Variables
    public GameObject target;
    public Rigidbody2D shooterRb;
   public GameObject enemy;
    public GameObject spawn;
    public  GameObject projectile;
    private Animator animatorEN;
    private SpriteRenderer enemySR;
    public float recoilInpulse = 7;
    bool targetDetected = false;
    #endregion
    #region initalisation
    // Start is called before the first frame update
    void Start()
    {
        shooterRb = enemy.GetComponent<Rigidbody2D>();
        animatorEN = enemy.GetComponent<Animator>();
        enemySR = enemy.GetComponent<SpriteRenderer>();
    }
    #endregion

 public float ShootTimer{
        get{
            return animatorEN.GetFloat(AnimationStrings.shootTimer);
        } private set {
            // The mathf.max is there to be sure that the value doesn't go under 0
            animatorEN.SetFloat(AnimationStrings.shootTimer, Mathf.Max(value, 0));
        }
    }

    #region shoot
    void FixedUpdate(){
        //If statement ensures player is in range and peals spawn one at a time
        if (targetDetected && GameObject.Find("Pearl(Clone)")== null || GameObject.Find("Cannonball(Clone)")== null){
            if(ShootTimer > 0){
            ShootTimer -= Time.fixedDeltaTime;
        }
        if(ShootTimer > 1f){
            //animates shooting
            animatorEN.SetTrigger("Shoot");
            // Spawns pearls
            Vector2 velocity= new Vector2(-5,0);
            GameObject spawnedProjectile = Instantiate(projectile,
                                        spawn.transform.position,
                                        Quaternion.identity);
             if (shooterRb){Debug.Log("Heeeyyyyy i'm detetcted");}
            shooterRb.velocity = new Vector2(shooterRb.velocity.x * - recoilInpulse, shooterRb.velocity.y);

            Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();

            rb.position = spawn.transform.position;
            rb.velocity = velocity;
        }
         


        
            



           
           
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
