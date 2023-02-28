using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : ListFactory
{
    //public static EnemyFactory Instance { get; private set; }

    //private ListFactory lf;
    //private List<GameObject> collectableList;
    private Vector3 spawnLocation;

    private void Awake()
    {
        spawnLocation = transform.position;
        //lf = gameObject.AddComponent<ListFactory>(); 
        //collectableList = lf.GenerateCollectableList();
    }
    private void Start()
    {
        GenerateRandomCollectable(collectablePrefabs, spawnLocation);
    }
    private void GenerateRandomCollectable(List<GameObject> collectablePrefabs, Vector3 spawnLocation)
    {
        int randomCollectable = UnityEngine.Random.Range(0, collectablePrefabs.Count - 1);
        Instantiate(collectablePrefabs[randomCollectable], spawnLocation, Quaternion.identity);
    }
}
