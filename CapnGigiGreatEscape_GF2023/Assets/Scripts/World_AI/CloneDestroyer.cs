using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDestroyer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyClone());
    }

    private IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(15f);
        ProceduralAI.chunksDestroyed += 1;
        Debug.Log("Chunks Destroyed: " + ProceduralAI.chunksDestroyed);
        Destroy(gameObject);
    }
}
