using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ListFactory : MonoBehaviour
{
    Debugging db;

    #region List Declarations

    // Ground Chunk Prefabs
    public static List<GameObject> groundPrefabs;

    // Platform Chunk Prefabs
    public static List<GameObject> platformPrefabs;

    // Enemy Prefabs
    public static List<GameObject> enemyPrefabs;

    // Collectable Prefabs
    public static List<GameObject> collectablePrefabs;

    // Trap Prefabs
    public static List<GameObject> trapPrefabs;

    #endregion

    #region Counters

    // Counts for Ground Sections Instantiated/Destroyed
    public static int platformChunkActivated = 0;
    public static int platformChunkDestroyed = 0;
    public static int groundChunkActivated = 0;
    public static int groundChunkDeactivated = 0;
    public static int chunksDeactivated = 0;

    public static int enemyKilled = 0;

    // Counts for Traps Instantiated/Destroyed
    public static int trapSpawned = 0;
    public static int trapDestroyed = 0;

    // Counts for Collectables Instantiated/Destroyed
    public static int collectableSpawned = 0;
    public static int collectableDestroyed = 0;
    public static int totalRunValue = 0;
    public static int actualRunValue = 0;

    #endregion

    // Most of the High-Priority Setup occurs in Awake()
    private void Awake()
    {
        // Load all GroundSection prefabs into list, display list in console
        groundPrefabs = GenerateGroundChunkList();

        // Load all PlatformSection prefabs into list, display list in console
        platformPrefabs = GeneratePlatformList();

        // Load all Enemy prefabs into the list, display list in console
        trapPrefabs = GenerateEnemyList();

        // Load all Collectable prefabs into the list, display list in console
        collectablePrefabs = GenerateCollectableList();

        // Load all Collectable prefabs into the list, display list in console
        trapPrefabs = GenerateTrapList();

    }

    #region Functions to retrieve co-ordinates
    private Transform GetNextEnemySpawnLocation()
    {
        return transform;
    }
    private Transform GetNextPlatformSpawnLocation()
    {
        return transform;
    }
    private Transform GetNextGroundSpawnLocation()
    {
        return transform;
    }
    #endregion

    #region List & Array Generation

    // GroundChunks
    public List<GameObject> GenerateGroundChunkList()
    {
        groundPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("GroundChunks"));
        return new List<GameObject>(groundPrefabs);
    }

    // PlatformChunks
    public List<GameObject> GeneratePlatformList()
    {
        // Return new Platform List
        platformPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("PlatformChunks"));
        return new List<GameObject>(platformPrefabs);
    }

    // Enemies
    public List<GameObject> GenerateEnemyList()
    {
        // Return new Enemy List
        enemyPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Enemies"));
        return new List<GameObject>(enemyPrefabs);
    }

    // Collectables
    public List<GameObject> GenerateCollectableList()
    {
        // Return new Collectable List
        collectablePrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Collectables"));
        return new List<GameObject>(collectablePrefabs);
    }

    // Traps
    public List<GameObject> GenerateTrapList()
    {
        // Return new Enemy List
        trapPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Traps"));
        return new List<GameObject>(trapPrefabs);
    }

    #endregion
}



