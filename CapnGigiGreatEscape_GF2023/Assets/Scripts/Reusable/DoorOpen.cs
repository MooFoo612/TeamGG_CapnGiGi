using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float moveSpeed = 2f;
    private Vector3 originalPos;
    private Vector3 targetPos;
    private GameObject player;

    void Start()
    {
        originalPos = door.transform.position;
        targetPos = new Vector3(originalPos.x, originalPos.y + 10f, originalPos.z);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 10f)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        while (door.transform.position != targetPos)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
