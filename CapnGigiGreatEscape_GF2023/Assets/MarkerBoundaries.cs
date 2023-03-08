using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBoundaries : WorldGenerator
{
    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameObject.name == "LeftMarker")
        {
            reversedWorld = false;
        }
        else if (collision.tag == "Player" && gameObject.name == "RightMarker")
        {
            reversedWorld = true;
        }
    }
}
