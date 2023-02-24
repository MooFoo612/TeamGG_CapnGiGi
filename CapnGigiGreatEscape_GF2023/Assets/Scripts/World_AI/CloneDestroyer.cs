using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDestroyer : MonoBehaviour
{
    //private float distanceFromPlayer = 0f;
    private GameObject player;
    private Vector3 playerPosition;

    void Awake()
    {
        player = GameObject.Find("CapnGigi");
        StartCoroutine(DestroyClone());
    }

    private IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(20f);
        //playerPosition = player.transform.position;
        //distanceFromPlayer = Vector3.Distance(gameObject.transform.position, playerPosition);
        //Debug.Log("Distance " + distanceFromPlayer);

        ProceduralAI.chunksDestroyed += 1;
        Debug.Log("Chunks Destroyed: " + ProceduralAI.chunksDestroyed);
        Destroy(gameObject);
          
    }
}
