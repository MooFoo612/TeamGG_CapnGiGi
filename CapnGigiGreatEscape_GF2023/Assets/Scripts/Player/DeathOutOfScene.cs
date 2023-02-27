using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOutOfScene : MonoBehaviour
{
    public GameObject player;
    
    

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -6)
        {
            Destroy(player);

            // go to game menu/shop
        }
    }
}
