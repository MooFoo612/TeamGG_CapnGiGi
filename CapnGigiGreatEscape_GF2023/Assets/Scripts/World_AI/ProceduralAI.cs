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

    // Enemy Scripts
    Krabby krabby;

    // Debugger
    Debugging db;

    #endregion

    #region Marker Object Declarations

    // Section Spawn will take the players currentSpeed+jumpAbilities into account to increase distance between sections
    //[SerializeField] Transform sectionSpawn;

    // EnemySpawnLocation will take the position of enemy spawnPoints
    //[SerializeField] Transform enemySpawnLocation;

    // Platform section will take x distance from previous platform section + y distance from currently instantiated ground
    //[SerializeField] Transform platformSpawnLocation;

    // Ground section will take lastEndPosition location
    //[SerializeField] Transform groundSpawnLocation;

    #endregion

    #region GameObject Declarations

    // Player 
    private GameObject player;

    #endregion

    #region List & Array Declarations

    // List for Ground Section prefabs
    public static List<GameObject> groundPrefabs = new List<GameObject>();

    // List for Platform Section prefabs
    public static List<GameObject> platformPrefabs = new List<GameObject>();

    // List for Enemy prefabs
    public static List<GameObject> enemyPrefabs = new List<GameObject>();

    // List for World Generation
    public static List<GameObject> worldList = new List<GameObject>();

    // Create Ground Object array for Ground List dependency 
    private UnityEngine.Object[] initArrayOfGroundPrefabs;

    // Create Platform Object array for Platform List dependency
    private UnityEngine.Object[] initArrayOfPlatformPrefabs;

    // Create Enemy Object Array for Enemy List dependency
    private UnityEngine.Object[] initArrayOfEnemyPrefabs;

    // Create Object array to store generated World objects
    public UnityEngine.Object[] worldArray;

    #endregion

    #region Counters

    // Counts for Ground Sections Instantiated/Destroyed
    public static int groundSpawned = 0;
    public static int groundDestroyed = 0;

    //Counts for Platforms Instantiated/Destroyed
    public static int platformSpawned = 0;
    public static int platformDestroyed = 0;

    // Counts for Enemies Instantiated/Destroyed/Killed
    public static int enemySpawned = 0;
    public static int enemyDestroyed = 0;
    public static int enemyKilled = 0;

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

        // Create World array
        worldArray = new UnityEngine.Object[5];

    }
    
    // Anything that doesn't need to be loaded *immediately* on application start
    private void Start()
    {
        // Generate random selection of :
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
        // Feed information to Generator Scripts
    }

    // Fixed Update for stability
    private void FixedUpdate()
    {
        // Manage any Physics-based decision-making
    }

    // Cleanup
    private void OnApplicationQuit()
    {
        // Display any information gathered for testing purposes or do any cleanup here
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
        initArrayOfGroundPrefabs = Resources.LoadAll("GroundSections", typeof(GameObject));

        //Optional Array Debug:
        //DebugGeneratedList(subListOfGroundObjects);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfGroundPrefabs)
        {
            groundPrefabs.Add(value);
        }
        // Optional List Debug:
        DebugGeneratedList(groundPrefabs);

        // Return new Ground List
        return new List<GameObject>(groundPrefabs);
    }

    // List for holding Platform Section Prefabs
    public List<GameObject> GeneratePlatformList()
    {
        // Create Object Array from Resources folder
        initArrayOfPlatformPrefabs = Resources.LoadAll("PlatformSections", typeof(GameObject));
        // Optional Array Debug:
        //DebugGeneratedObjectArray(subListOfPlatformPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfPlatformPrefabs)
        {
            platformPrefabs.Add(value);
        }
        // Optional List Debug:
        DebugGeneratedList(platformPrefabs);

        // Return new Platform List
        return new List<GameObject>(platformPrefabs);
    
    }

    // List for holding Enemy Prefabs
    public List<GameObject> GenerateEnemyList()
    {
        // Create Object Array from Resources folder
        initArrayOfEnemyPrefabs = Resources.LoadAll("Enemies", typeof(GameObject));

        // Optional Debug:
        //DebugGeneratedObjectArray(subListOfEnemyPrefabs);

        // Fill list with Array objects
        foreach (GameObject value in initArrayOfEnemyPrefabs)
        {
            enemyPrefabs.Add(value);
        }
        // Optional List Debug:
        DebugGeneratedList(enemyPrefabs);

        // Return new Enemy List
        return new List<GameObject>(enemyPrefabs);
    }
    #endregion

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




