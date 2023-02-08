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

    #endregion

    #region Marker Object Declarations

    // Section Spawn will take the players currentSpeed+jumpAbilities into account to increase distance between sections
    [SerializeField] Transform sectionSpawn;

    // EnemySpawnLocation will take the position of enemy spawnPoints
    [SerializeField] Transform enemySpawnLocation;

    // Platform section will take x distance from previous platform section + y distance from currently instantiated ground
    [SerializeField] Transform platformSpawnLocation;

    // Ground section will take lastEndPosition location
    [SerializeField] Transform groundSpawnLocation;

    #endregion

    #region GameObject Declarations

    // Player 
    private GameObject player;

    #endregion

    #region List Declarations

    // List for Ground Section prefabs
    public static List<GameObject> groundPrefabs = new List<GameObject>();

    // List for Platform Section prefabs
    public static List<GameObject> platformPrefabs = new List<GameObject>();

    // List for Enemy prefabs
    public static List<GameObject> enemyPrefabs = new List<GameObject>();

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
    // Start is called before the first frame update
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

    }
    
    // Called after Awake()
    private void Start()
    {
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

    #region List Generation

    // List for holding Ground Section Prefabs
    public List<GameObject> GenerateGroundList()
    {
        // Create Object Array from Resources folder
        Object[] subListOfGroundObjects = Resources.LoadAll("GroundSections", typeof(GameObject));

        //Optional Debug:
        DebugGeneratedList(subListOfGroundObjects);

        // Return new Ground List
        return new List<GameObject>(groundPrefabs);
    }

    // List for holding Platform Section Prefabs
    public List<GameObject> GeneratePlatformList()
    {
        // Create Object Array from Resources folder
        Object[] subListOfPlatformPrefabs = Resources.LoadAll("PlatformSections", typeof(GameObject));

        // Optional Debug:
        DebugGeneratedList(subListOfPlatformPrefabs);

        // Return new Platform List
        return new List<GameObject>(platformPrefabs);
    
    }

    // List for holding Enemy Prefabs
    public List<GameObject> GenerateEnemyList()
    {
        // Create Object Array from Resources folder
        Object[] subListOfEnemyPrefabs = Resources.LoadAll("Enemies", typeof(GameObject));

        // Optional Debug:
        DebugGeneratedList(subListOfEnemyPrefabs);

        // Return new Enemy List
        return new List<GameObject>(enemyPrefabs);
    }
    #endregion

    #region Debug Functions

    // For Debugging any generated List | Length + Names
    private void DebugGeneratedList(Object[] prefabList)
    {
        // Display the Length of the List
        Debug.Log(prefabList.Length);

        // Print the name of each object in the List
        foreach (GameObject value in prefabList)
        {
            Debug.Log(value.name);
        }
    }
    #endregion
}


