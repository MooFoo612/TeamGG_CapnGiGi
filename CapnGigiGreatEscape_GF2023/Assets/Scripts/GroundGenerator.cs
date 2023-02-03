using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables
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
        // Fetch values for variables
        groundCollider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (groundCollider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
        screenLeft = Camera.main.transform.position.x * -2;
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
    }
}
