using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WorldObject_Classes;

public class TrapGenerator : MonoBehaviour
{
    // Grab Enemy Prefabs
    private static List<GameObject> trapList = new List<GameObject>();
    private static List<GameObject> trapSpawnMarkers = new List<GameObject>();

    // Grab Spawn Positions 
    private UnityEngine.Object[] initArrayOfTrapSpawnMarkers;
    private static List<Vector3> trapSpawnPositions = new List<Vector3>();
    private GameObject trapObj;
    private Transform trapToSpawn;
    private ProceduralAI ai;

    // Start is called before the first frame update
    void Awake()
    {
        // Fetch list of Platform prefabs (Runs in Start to ensure list has been generated by other script on Awake)
        trapList = ProceduralAI.enemyPrefabs;
        trapSpawnMarkers = GenerateTrapSpawnMarkerList();

    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Spawner
    public void SpawnTraps()
    {
        foreach (Transform transform in gameObject.transform)
        {
            if (transform.CompareTag("TrapSpawn"))
            {
                trapSpawnPositions.Add(transform.position);
                break;
            }
        }
        //Get the transform to refrence the Spawn Positions
        Transform spawnedTrap = SpawnTraps(trapSpawnPositions);
    }

    // SpawnEnemies(AtPosition)
    public Transform SpawnTraps(List<Vector3> enemySpawnPositions)
    {

        foreach (Vector3 trapSpawnPosition in enemySpawnPositions)
        {
            // Get random Enemy from List
            trapToSpawn = RandomTrapGenerator(0, trapList.Count);
            Transform spawnTrap = Instantiate(trapToSpawn, trapSpawnPosition, Quaternion.identity);
            ProceduralAI.enemySpawned += 1;
        }
        // Return the transform for sister method
        return trapToSpawn;
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

    public List<GameObject> GenerateTrapSpawnMarkerList()
    {
        try
        {
            trapSpawnMarkers = ai.GenerateTrapSpawnMarkerList();
        }
        catch (NullReferenceException nre)
        {
            Debug.Log("Null Reference Exception! : " + nre);
        }
        finally
        {
            //enemySpawnMarkers = trapSpawnMarkers.Clear();
        }

        // Return new Enemy List
        return new List<GameObject>(trapSpawnMarkers);
    }

    private void OnDisable()
    {
        // Code here
    }



}
