using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour
{
    private static List<GameObject> collectableList = new List<GameObject>();
    private GameObject collectableObj;
    private Transform collectableToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch list of Platform prefabs (Runs in Start to ensure list has been generated by other script on Awake)
        collectableList = ProceduralAI.collectablePrefabs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Transform RandomCollectableSpawner(int floor, int ceiling)
    {
        // Variable to hold the index of random list element
        int randomCollectable = 0;

        // Get random index for list
        randomCollectable = UnityEngine.Random.Range(0, collectableList.Count);

        // Call GameObject from list and get its transform
        collectableObj = collectableList[randomCollectable];
        collectableToSpawn = collectableObj.transform;

        // Return the randomly-chosen Platform Chunk
        return collectableToSpawn;
    }
}
