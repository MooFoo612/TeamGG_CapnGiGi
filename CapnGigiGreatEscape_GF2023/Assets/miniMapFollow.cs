using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapFollow : MonoBehaviour
{
    //public Transform player;

    //// Update is called once per frame
    //void LateUpdate()
    //{
    //    Vector3 newPosition = player.position;
    //    newPosition.y = transform.position.y;
    //    transform.position = newPosition;
    //}

    public GameObject player;
    public Vector3 offset = new Vector3(0, 2, -10);

    void Start()
    {
        player = GameObject.Find("CapnGigi");
    }

    void FixedUpdate()
    {
        if (player)
        {
            transform.position = new Vector3(
                player.transform.position.x + offset.x,
                player.transform.position.y + offset.y,
                player.transform.position.z + offset.z);
        }
    }
}
