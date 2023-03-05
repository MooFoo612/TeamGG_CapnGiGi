using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoffHead : MonoBehaviour
{
    #region Variables
    public GameObject target;
    public GameObject enemy;
    public GameObject spawn;
    public  GameObject projectile;
    private Animator animatorEN;

    // private SpriteRenderer enemySR;
    public GameObject pivotPoint;
    public SoundEffect Shootaudio;
    
    public float angle;
    public float power = 5f;

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
                enemy.transform.localScale = new Vector2enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
            }
            // Update the value
            _facingDirection = value;
        }
    }
    */

    #endregion
    #region initalisation
    // Start is called before the first frame update
    void Start()
    {
        //shooterRb = enemy.GetComponent<Rigidbody2D>();
        animatorEN = enemy.GetComponent<Animator>();
        // enemySR = enemy.GetComponent<SpriteRenderer>();

    }
    #endregion

void Awake(){
        GameObject.Find("target");
}

    #region shoot
    private void FixedUpdate(){


        Vector2 targetPos = GameObject.FindWithTag("Player").transform.position - gameObject.transform.position;

        angle = Mathf.Atan2(targetPos.x, targetPos.y) * Mathf.Rad2Deg;
        
                Vector2 velocity = new Vector2(
        power * Mathf.Sin(angle * Mathf.Deg2Rad),
        power * Mathf.Cos(angle * Mathf.Deg2Rad)
        );

            pivotPoint.transform.rotation = Quaternion.Euler(0, 0, -angle -90);


        // if (angle <= -90)
        // {
            
        //     Debug.Log("This is where the cannon reverses");
        //     enemy.transform.localScale = new Vector2(-1, enemy.transform.localScale.y);
        //     // reverse position with transform
        //     }
        //     else{
        //                 enemy.transform.localScale = new Vector2(1, enemy.transform.localScale.y);
        //     }
      }
    
    private void Update(){


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(target.name)){
            //Detects player
            Debug.Log("Bang");
            StartCoroutine("ShotTimer");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(target.name) ){
            //Detects player left
        }
    }

    private IEnumerator ShotTimer() {
        yield return new WaitForSeconds(1.5f);


         Vector2 velocity = new Vector2(
        power * Mathf.Sin(angle * Mathf.Deg2Rad),
        power * Mathf.Cos(angle * Mathf.Deg2Rad)
        );   
            Shootaudio.PlaySoundEffect();
            // animatorEN.SetTrigger("Shoot");
            // Spawns projectiles


            // Vector2 velocity= new Vector2(-5,0);            
            GameObject spawnedProjectile = Instantiate(projectile,
                                        spawn.transform.position,
                                        Quaternion.identity);
    
            Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
            rb.position = spawn.transform.position;
            rb.velocity = new Vector2(
        power * Mathf.Sin(angle * Mathf.Deg2Rad),
        power * Mathf.Cos(angle * Mathf.Deg2Rad));                           
            

        }

    }
    #endregion