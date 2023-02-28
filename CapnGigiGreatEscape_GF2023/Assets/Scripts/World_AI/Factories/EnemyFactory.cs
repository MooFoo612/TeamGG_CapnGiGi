using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : ListFactory
{
    private ListFactory lf;
    private List<GameObject> enemyList;
    private Vector3 spawnLocation;

    private void Awake()
    {
        // Where to 
        spawnLocation = gameObject.transform.position;
        lf = gameObject.AddComponent<ListFactory>();
        enemyList = lf.GenerateEnemyList();
    }
    private void Start()
    {
        GenerateRandomEnemy(enemyList, spawnLocation);
    }
    private void GenerateRandomEnemy(List<GameObject> enemyList, Vector3 spawnLocation)
    {
        int randomEnemy = UnityEngine.Random.Range(0, enemyList.Count - 1);
        Instantiate(enemyList[randomEnemy], spawnLocation, Quaternion.identity);
    }

}
