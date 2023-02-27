using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    #region Variables
    public GameObject target;
    public GameObject enemy;
    public GameObject spawn;
    public  GameObject projectile;
    private Animator animatorEN;
    private SpriteRenderer enemySR;

    public CoinAudio Shootaudio;
    

    
    /*
    public float recoilInpulse = 0.5f;
    public Rigidbody2D shooterRb;
    public enum FacingDirection {Right, Left}
    private FacingDirection _facingDirection;
    public FacingDirection Direction{
        get{
            // The get works with the same logic of the player 
            return _facingDirection;
        } set {
            // If the value does't correspond 
            if(_facingDirection != value){
                // Flip sprite direction using localScale 
                enemy.transform.localScale = new Vector2(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
            }
            // Update the value
            _facingDirection = value;
        }
    }
    */

    public float ShootTimer{
        get{
            return animatorEN.GetFloat(AnimationStrings.shootTimer);
        } private set {
            // The mathf.max is there to be sure that the value doesn't go under 0
            animatorEN.SetFloat(AnimationStrings.shootTimer, Mathf.Max(value, 0));
        }
    }
    
    #endregion
    #region initalisation
    // Start is called before the first frame update
    void Start()
    {
        //shooterRb = enemy.GetComponent<Rigidbody2D>();
        animatorEN = enemy.GetComponent<Animator>();
        enemySR = enemy.GetComponent<SpriteRenderer>();
    }
    #endregion

    

    #region shoot
    private void FixedUpdate(){
        
        /*
        if (shooterRb){
                Debug.Log("Heeeyyyyy i'm detetcted");
            }
        */

            //animates shooting


            /*
            if(Direction == FacingDirection.Right){
                shooterRb.velocity = new Vector2(-recoilInpulse, shooterRb.velocity.y);
            } else {
                shooterRb.velocity = new Vector2(recoilInpulse, shooterRb.velocity.y);
            }
            */
            //shooterRb.velocity = new Vector2(shooterRb.velocity.x * - recoilInpulse, shooterRb.velocity.y);

                

    }

    private void Update(){
        if(ShootTimer > 0){
            ShootTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(target.name)){
            //Detects player
            StartCoroutine("ShotTimer");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(target.name)){
            //Detects player left
        }
    }

    private IEnumerator ShotTimer() {
        yield return new WaitForSeconds(1.5f);
                
            Shootaudio.PlayCoinAudio();
            animatorEN.SetTrigger("Shoot");
            // Spawns pearls
            Vector2 velocity= new Vector2(-5,0);
            GameObject spawnedProjectile = Instantiate(projectile,
                                        spawn.transform.position,
                                        Quaternion.identity);

             Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
            rb.position = spawn.transform.position;
            rb.velocity = velocity;                            
            

        }

    }
    #endregion

