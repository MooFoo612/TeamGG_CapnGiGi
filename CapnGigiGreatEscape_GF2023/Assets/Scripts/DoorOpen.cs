using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public float distance = 10f;
    private bool isOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, door.transform.position) < distance && !isOpen)
        {
            door.transform.Translate(Vector3.up * 10);
            isOpen = true;
        }
    }
}
