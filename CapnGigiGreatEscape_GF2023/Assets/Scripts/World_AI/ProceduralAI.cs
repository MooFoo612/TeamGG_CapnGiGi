using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProceduralAI : MonoBehaviour
{
    #region Script Access

    // Ground Scripts
    GroundGenerator groundGenerator;

    // Platform Scripts
    PlatformGenerator platformGenerator;

    // Debugger
    Debugging db;

    #endregion


    #region GameObject Declarations

    // Player 
    private GameObject player;
    private float totalDistance;
    private bool reversedWorld;

    #endregion


    #region List & Array Declarations

    // List for Ground Section prefabs
    public static List<GameObject> groundPrefabs = new List<GameObject>();

    // List for Platform Section prefabs
    public static List<GameObject> platformPrefabs = new List<GameObject>();

    // List for Enemy prefabs
    public static List<GameObject> enemyPrefabs = new List<GameObject>();
    
    // List for Enemy Spawn Markers
    public static List<GameObject> enemySpawnMarkers = new List<GameObject>();

    // List for Collectable prefabs
    public static List<GameObject> collectablePrefabs = new List<GameObject>();

    // List for Enemy Spawn Markers
    public static List<GameObject> collectableSpawnMarkers = new List<GameObject>();

    // List for Trap prefabs
    public static List<GameObject> trapPrefabs = new List<GameObject>();

    // List for Enemy Spawn Markers
    public static List<GameObject> trapSpawnMarkers = new List<GameObject>();

    // List for World Generation
    public static List<GameObject> worldList = new List<GameObject>();

    // Create Ground Object array for Ground List dependency 
    private UnityEngine.Object[] initArrayOfGroundPrefabs;

    // Create Platform Object array for Platform List dependency
    private UnityEngine.Object[] initArrayOfPlatformPrefabs;

    // Create Enemy Object Array for Enemy List dependency
    private UnityEngine.Object[] initArrayOfEnemyPrefabs;

    // Create Enemy Object Array for Enemy List dependency
    private UnityEngine.Object[] initArrayOfEnemySpawnMarkers;

    // Create Collectable Object Array for Collectable list dependency
    private UnityEngine.Object[] initArrayOfCollectablePrefabs;

    // Create Enemy Object Array for Enemy List dependency
    private UnityEngine.Object[] initArrayOfCollectableSpawnMarkers;

    // Create Collectable Object Array for Collectable list dependency
    private UnityEngine.Object[] initArrayOfTrapPrefabs;

    // Create Enemy Object Array for Enemy List dependency
    private UnityEngine.Object[] initArrayOfTrapSpawnMarkers;

    // Create Object array to store generated World objects
    public UnityEngine.Object[] worldArray;

    #endregion

    #region Counters

    // Counts for Ground Sections Instantiated/Destroyed
    public static int platformChunkActivated = 0;
    public static int platformChunkDeactivated = 0;
    public static int groundChunkActivated = 0;
    public static int groundChunkDeactivated = 0;
    public static int chunksDeactivated = 0;

    // Counts for Enemies Instantiated/Destroyed/Killed
    public static int enemySpawned = 0;
    public static int enemyDestroyed = 0;
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

    #region Flow Control
    // Most of the High-Priority Setup occurs in Awake()
    private void Awake()
    {
        // Fetch Player object
        player = GameObject.Find("CapnGigi");

        // Load all GroundSection prefabs into list, display list in console
        groundPrefabs = GenerateGroundList();

        // Load all PlatformSection prefabs into list, display list in console
        platformPrefabs = GeneratePlatformList();

        // Load all Enemy prefabs into the list, display list in console
        enemyPrefabs = GenerateEnemyList();

        // Load all Collectable prefabs into the list, display list in console
        collectablePrefabs = GenerateCollectableList();
       
        // Load all Collectable prefabs into the list, display list in console
        trapPrefabs = GenerateTrapList();
       
        // Create World array
        worldArray = new UnityEngine.Object[5];

    }
    
    // Anything that doesn't need to be loaded *immediately* on application start
    private void Start()
    {
        // Using Singleton Design Pattern << Generate random selection of :
        // Platforms
        // Ground Sections
        // Enemies
        // Collectables
        // Generate World objects to populate 3/5 initial worldArray;
        // Copy 2 Game objects from Left Hand Side of player to Right Hand Side to save on resources

        // ------------------------------
        // For any tidy-up after Awake()
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    #endregion

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

    // Array for holding generated live Game World | Places player in middle Array block 
    public UnityEngine.Object[] GenerateGameWorld()
    {
        UnityEngine.Object[] liveGameWorld = new UnityEngine.Object[5];
        return liveGameWorld;
    }
    // Array for custom-built initial game-world | Places player in left-most Array block
    public UnityEngine.Object[] GenerateInitialGameWorld()
    {
        UnityEngine.Object[] initGameWorld = new UnityEngine.Object[5];
        return initGameWorld;
    }

    // List for holding Ground Section Prefabs
    public List<GameObject> GenerateGroundList()
    {
        // Create Object Array from Resources folder
        initArrayOfGroundPrefabs = Resources.LoadAll("GroundChunks", typeof(GameObject));

        //Optional Array Debug:
        //DebugGeneratedList(initArrayOfGroundObjects);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfGroundPrefabs)
        {
            groundPrefabs.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(groundPrefabs);

        // Return new Ground List
        return new List<GameObject>(groundPrefabs);
    }

    // List for holding Platform Section Prefabs
    public List<GameObject> GeneratePlatformList()
    {
        // Create Object Array from Resources folder
        initArrayOfPlatformPrefabs = Resources.LoadAll("PlatformChunks", typeof(GameObject));
        // Optional Array Debug:
        //DebugGeneratedObjectArray(initArrayOfPlatformPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfPlatformPrefabs)
        {
            platformPrefabs.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(platformPrefabs);

        // Return new Platform List
        return new List<GameObject>(platformPrefabs);
    
    }

    // List for holding Enemy Prefabs
    public List<GameObject> GenerateEnemyList()
    {
        // Create Object Array from Resources folder
        initArrayOfEnemyPrefabs = Resources.LoadAll("Enemies", typeof(GameObject));

        // Optional Debug:
        //DebugGeneratedObjectArray(initArrayOfEnemyPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfEnemyPrefabs)
        {
            enemyPrefabs.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(enemyPrefabs);

        // Return new Enemy List
        return new List<GameObject>(enemyPrefabs);
    }

    public List<GameObject> GenerateEnemySpawnMarkerList()
    {
        // Create Object Array from Resources folder
        initArrayOfEnemySpawnMarkers = GameObject.FindGameObjectsWithTag("EnemySpawn");

        // Optional Debug:
        //DebugGeneratedObjectArray(initArrayOfEnemyPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfEnemySpawnMarkers)
        {
            enemySpawnMarkers.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(enemyPrefabs);

        // Return new Enemy List
        return new List<GameObject>(enemySpawnMarkers);
    }

    public List<GameObject> GenerateCollectableList()
    {
        // Create Object Array from Resources folder
        initArrayOfCollectablePrefabs = Resources.LoadAll("Collectables", typeof(GameObject));

        // Optional Debug:
        //DebugGeneratedObjectArray(initArrayOfCollectablePrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfCollectablePrefabs)
        {
            collectablePrefabs.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(collectablePrefabs);

        // Return new Enemy List
        return new List<GameObject>(collectablePrefabs);
    }

    public List<GameObject> GenerateCollectableSpawnMarkerList()
    {
        // Create Object Array from Resources folder
        initArrayOfCollectableSpawnMarkers = GameObject.FindGameObjectsWithTag("CollectableSpawn");

        // Optional Debug:
        //DebugGeneratedObjectArray(initArrayOfCollectableSpawnMarkers);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfCollectableSpawnMarkers)
        {
            collectableSpawnMarkers.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(collectableSpawnMarkers);

        // Return new Enemy List
        return new List<GameObject>(collectableSpawnMarkers);
    }


    public List<GameObject> GenerateTrapList()
    {
        // Create Object Array from Resources folder
        initArrayOfTrapPrefabs = Resources.LoadAll("Traps", typeof(GameObject));

        // Optional Debug:
        //DebugGeneratedObjectArray(initArrayOfTrapPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfTrapPrefabs)
        {
            trapPrefabs.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(trapPrefabs);

        // Return new Enemy List
        return new List<GameObject>(trapPrefabs);
    }
    #endregion

    public List<GameObject> GenerateTrapSpawnMarkerList()
    {
        // Create Object Array from Resources folder
        initArrayOfTrapSpawnMarkers = GameObject.FindGameObjectsWithTag("TrapSpawn");

        // Optional Debug:
        //DebugGeneratedObjectArray(initArrayOfTrapPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfTrapSpawnMarkers)
        {
            trapSpawnMarkers.Add(value);
        }
        // Optional List Debug:
        //DebugGeneratedList(trapPrefabs);

        // Return new Enemy List
        return new List<GameObject>(trapSpawnMarkers);
    }

    private float TotalDistanceCalculation(Vector3 distanceMarker, Vector3 playerPosition)
    {
        if (!reversedWorld)
        {
            totalDistance += Vector3.Distance(playerPosition, distanceMarker);
            Debug.Log("Distance: " + totalDistance);
            return totalDistance;
        }
        else
        {
            totalDistance += Vector3.Distance(distanceMarker, playerPosition);
            return totalDistance;
        }
    }

    #region Debug Functions

    // For Debugging any generated Object Array | Length + Names
    private void DebugGeneratedObjectArray(UnityEngine.Object[] prefabArray)
    {
        // Display the Length of the Array
        Debug.Log("Array Size: " + prefabArray.Length);

        // Print the name of each object in the Array
        foreach (GameObject value in prefabArray)
        {
            Debug.Log("Prefab in Array: " + value.name);
        }
    }
    // For Debugging any generated List | Length + Names
    private void DebugGeneratedList(List<GameObject> prefabList)
    {
        // Display the Length of the List
        Debug.Log("List Size: " + prefabList.Count );

        // Print the name of each object in the List
        foreach (GameObject value in prefabList)
        {
            Debug.Log("Prefab in List: " + value.name);
        }
    }

    #endregion
}




