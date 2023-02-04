using UnityEngine;
using Cinemachine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables

    // Player object
    PlayerController player;
    Vector2 velocity;

    // General
    public float groundHeight;
    
    // RHS & LHS of ground collider
    public float groundRight;
    public float groundLeft;

    //RHS & LHS of visible play area
    public float screenRight;
    public float screenLeft;

    // Colliders
    BoxCollider2D groundCollider;

    // Logical checks
    bool groundGenerated = false;

    #endregion

    void Awake()
    {
        // Fetch player attributes
        player = GameObject.Find("CapnGigi").GetComponent<PlayerController>();
        velocity = GameObject.Find("CapnGigi").GetComponent<Rigidbody2D>().velocity;
        groundCollider = GetComponent<BoxCollider2D>();
        CinemachineVirtualCamera playerCam = GetComponent<CinemachineVirtualCamera>();

        // Assign calculations to world variables
        groundHeight = transform.position.y + (groundCollider.size.y / 2);
        screenRight = playerCam.transform.position.x * 2;
        //screenLeft = playerCam.transform.position.x * -2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Get the current direction from the ground
        Vector2 currentPosition = transform.position;
        currentPosition.x -= player.rb.velocity.x * Time.fixedDeltaTime;


        // Fetch the position of the furthest point to the right of the ground section every frame
        groundRight = transform.position.x + (groundCollider.size.x / 2);
        
        // If a ground section has not been generated
        if (!groundGenerated)
        {
            // and the end of the screen has moved beyond the furthest point right of the ground level
            if (groundRight < screenRight) 
            {
                // Set bool to true to prevent extra sections being spawned
                groundGenerated = true;
                // Generate the next section of the ground level
                generateSection();
            }
        }
    }

    void generateSection()
    {
        GameObject obj = Instantiate(gameObject);
        BoxCollider2D objCollider = obj.GetComponent<BoxCollider2D>();
        Vector2 position;
        position.x = screenRight + 30;
        position.y = transform.position.y;
        obj.transform.position = position;
    }
}
