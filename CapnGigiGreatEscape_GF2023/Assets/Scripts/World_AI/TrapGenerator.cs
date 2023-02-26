using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WorldObject_Classes;

public class TrapGenerator : MonoBehaviour
{
    // Grab Enemy Prefabs
    private List<GameObject> trapList = new List<GameObject>();
    private List<Vector3> trapSpawnPositions = new List<Vector3>();

    // Grab Spawn Positions 
    private UnityEngine.Object[] initArrayOfEnemySpawnMarkers;
    private Transform parentObj;
    private GameObject trapObj;
    private Transform trapToSpawn;

    private ProceduralAI ai;

    // Start is called before the first frame update
    void Awake()
    {
        // Fetch list of Platform prefabs (Runs in Start to ensure list has been generated by other script on Awake)
        trapList = ProceduralAI.trapPrefabs;
        try
        {
            trapSpawnPositions = GenerateTrapSpawnMarkerPositions();

        }
        catch (NullReferenceException nre)
        {
            Debug.Log(nre.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Spawner
    public void SpawnTraps()
    {
        parentObj = gameObject.transform;

        foreach (Transform childObj in parentObj)
        {
            if (childObj.CompareTag("TrapSpawn"))
            {
                trapSpawnPositions.Add(childObj.position);
            }
        }
        //Get the transform to refrence the Spawn Positions
        //Transform spawnedEnemy = SpawnEnemies(enemySpawnPositions);
        foreach (Vector3 trapSpawnPosition in trapSpawnPositions)
        {
            SpawnTraps(trapSpawnPosition);
        }
    }

    // SpawnEnemies(AtPosition)
    public Transform SpawnTraps(Vector3 trapSpawnPosition)
    {
        // Get random Enemy from List
        trapToSpawn = RandomTrapGenerator(0, trapList.Count);
        //Vector3 spawnPoint = RandomSpawnGenerator(0, enemySpawnPositions.Count);
        Transform spawnTrap = Instantiate(trapToSpawn, trapSpawnPosition, Quaternion.identity);
        ProceduralAI.trapSpawned += 1;
        // Return the transform for sister method
        return spawnTrap;
    }
    #endregion

    // REGGIE
    private Transform RandomTrapGenerator(int floor, int ceiling)
    {
        // Variable to hold the index of random list element
        int randomTrap = 0;

        // Get random index for list
        randomTrap = UnityEngine.Random.Range(0, trapList.Count);

        // Call GameObject from list and get its transform
        trapObj = trapList[randomTrap];
        trapToSpawn = trapObj.transform;

        // Return the randomly-chosen enemy to spawn
        return trapToSpawn;
    }

    private Vector3 RandomSpawnGenerator(int floor, int ceiling)
    {
        // Variable to hold the index of random list element
        int randomSpawnPoint = 0;

        // Get random index for list
        randomSpawnPoint = UnityEngine.Random.Range(0, trapSpawnPositions.Count);

        // Call Vector from list and get its transform
        Vector3 spawnPoint = trapSpawnPositions[randomSpawnPoint];
        //enemyToSpawn = enemyObj.transform;

        // Return the randomly-chosen enemy to spawn
        return spawnPoint;
    }

    public List<Vector3> GenerateTrapSpawnMarkerPositions()
    {
        if (trapSpawnPositions.Count > 0)
        {
            trapSpawnPositions.Clear();

            try
            {
                parentObj = gameObject.transform;

                foreach (Transform childObj in parentObj)
                {
                    if (childObj.CompareTag("EnemySpawn"))
                    {
                        trapSpawnPositions.Add(childObj.position);
                    }
                }
            }
            catch (NullReferenceException nre)
            {
                Debug.Log("Null Reference Exception! : " + nre);
            }

            return new List<Vector3>(trapSpawnPositions);
        }
        else
        {
            try
            {
                parentObj = gameObject.transform;

                foreach (Transform childObj in parentObj)
                {
                    if (childObj.CompareTag("EnemySpawn"))
                    {
                        trapSpawnPositions.Add(childObj.position);
                    }
                }
            }
            catch (NullReferenceException nre)
            {
                Debug.Log("Null Reference Exception! : " + nre);
            }

            // Return new Enemy List
            return new List<Vector3>(trapSpawnPositions);
        }
    }

    private void OnDisable()
    {
        // Code here
    }



}
