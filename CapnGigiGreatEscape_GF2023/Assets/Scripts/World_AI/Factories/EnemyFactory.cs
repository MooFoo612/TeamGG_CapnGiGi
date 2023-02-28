using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : ListFactory
{
    private Vector3 spawnLocation;

    private Transform enemyParent;

    private void Awake()
    {
        enemyParent = GameObject.Find("Enemies_Active").transform;
        spawnLocation = gameObject.transform.position;
    }
    private void Start()
    {
        GenerateRandomEnemy(enemies, spawnLocation, enemyParent);
    }
    private void GenerateRandomEnemy(List<GameObject> enemyPrefabs, Vector3 spawnLocation, Transform enemyParent)
    {
        int randomEnemy = UnityEngine.Random.Range(0, enemyPrefabs.Count - 1);
        Instantiate(enemyPrefabs[randomEnemy], spawnLocation, Quaternion.identity, enemyParent);
    }

}
