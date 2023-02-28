using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : ListFactory
{
    //public static EnemyFactory Instance { get; private set; }

    //private ListFactory lf;
    //private List<GameObject> enemyList;
    private Vector3 spawnLocation;

    private void Awake()
    {
        // Where to 
        spawnLocation = gameObject.transform.position;
        //lf = gameObject.AddComponent<ListFactory>();
        //enemyList = enemyPrefabs;
    }
    private void Start()
    {
        GenerateRandomEnemy(enemyPrefabs, spawnLocation);
    }
    private void GenerateRandomEnemy(List<GameObject> enemyPrefabs, Vector3 spawnLocation)
    {
        int randomEnemy = UnityEngine.Random.Range(0, enemyPrefabs.Count - 1);
        Instantiate(enemyPrefabs[randomEnemy], spawnLocation, Quaternion.identity);
    }

}
