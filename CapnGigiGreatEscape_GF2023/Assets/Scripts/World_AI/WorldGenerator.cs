using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;

public class WorldGenerator : ProceduralAI
{
    #region Variables
    [SerializeField] private Transform groundChunk;
    [SerializeField] private Transform platformChunk;
    [SerializeField] private Transform worldStart;
    [SerializeField] private Transform groundStart;
    [SerializeField] private Transform platformStart;
    [SerializeField] private GameObject player;
    private GroundGenerator groundGenerator;
    private PlatformGenerator platformGenerator;
    private EnemyGenerator enemyGenerator;
    private bool reversedWorld;

    private const float DISTANCE_TO_SPAWN_SECTION_RIGHT = 25f;
    private const float DISTANCE_TO_SPAWN_SECTION_LEFT = 25f;
    //private const float DISTANCE_TO_DESTROY_SECTION = 25f;

    // Marker Positions---------------
    private Vector3 worldEndRight;
    private Vector3 worldEndLeft;
    //--------------------------------
    private Vector3 groundEndRight;
    private Vector3 groundEndLeft;
    //--------------------------------
    private Vector3 platformEndRight;
    private Vector3 platformEndLeft;

    #endregion

    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Ensure world isn't reversed
        reversedWorld = false;

        // Find the child EndPosition object in the GameStart parent
        groundEndRight = groundStart.Find("GroundEnd_Right").position;
        platformEndRight = platformStart.Find("PlatformEnd_Right").position;
        // worldEndRight = (platformStart - groundStart) / 2; 

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundGenerator)) as GroundGenerator;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformGenerator)) as PlatformGenerator;

        // Acess Enemy Generator Script
        enemyGenerator = GameObject.FindObjectOfType(typeof(EnemyGenerator)) as EnemyGenerator;

    }
    private void Start()
    {

    }
    private void Update()
    {
        // If the world IS NOT reversed
        if (!reversedWorld)
        {
            // THIS IS WHERE STUFF IS BEING SPAWNED (TO THE RIGHT)

            // If the player is close enough to the next reference spawn point
            if (Vector3.Distance(player.transform.position, worldEndRight) < DISTANCE_TO_SPAWN_SECTION_RIGHT)
            {
                // Spawn another section
                SpawnGroundChunk_Right();
                SpawnPlatformChunk_Right();
                Debug.Log("World Chunk generated");
                StartCoroutine(AllPurposeTimer(1.5f));
            }
        }
        else // If the world IS reversed
        {
            // THIS IS WHERE STUFF IS BEING SPAWNED (TO THE LEFT)

            // If the player is close enough to the next spawn then: 
            if (Vector3.Distance(player.transform.position, worldEndLeft) < DISTANCE_TO_SPAWN_SECTION_LEFT)
            {
                // Spawn another section
                SpawnGroundChunk_Left();
                SpawnPlatformChunk_Left();
                Debug.Log("World Chunk generated");

            }
        }
    }
    #region Ground Spawner
    public void SpawnGroundChunk_Right()
    {
        groundGenerator.SpawnGroundChunk_Right();
    }

    /*
    public Transform SpawnGroundChunk_Right(Vector3 newSection)
    {
        Transform lastClockwiseSectionTransform = Instantiate(groundChunk, newSection, Quaternion.identity);
        //ProceduralAI.platformSpawned += 1;

        return groundGenerator.SpawnGroundChunk_Right(newSection);
    } 
    */

    public void SpawnGroundChunk_Left()
    {
        groundGenerator.SpawnGroundChunk_Left();
    }

    /*
    public Transform SpawnGroundChunk_Left(Vector3 newSection)
    {
        Transform lastClockwiseSectionTransform = Instantiate(groundChunk, newSection, Quaternion.identity);

        return groundGenerator.SpawnGroundChunk_Right(newSection);
    }
    */

    #endregion

    #region Platform Spawner
    public void SpawnPlatformChunk_Right()
    {
        platformGenerator.SpawnPlatformChunk_Right();
    }

    /*
    public Transform SpawnPlatformChunk_Right(Vector3 newSection)
    {
        Transform lastClockwiseSectionTransform = Instantiate(groundChunk, newSection, Quaternion.identity);
        //ProceduralAI.platformSpawned += 1;

        return platformGenerator.SpawnPlatformChunk_Right(newSection);

    }

    */

    public void SpawnPlatformChunk_Left()
    {
        platformGenerator.SpawnPlatformChunk_Left();
    }

    /*
    public Transform SpawnPlatformChunk_Left(Vector3 newSection)
    {
        Transform lastClockwiseSectionTransform = Instantiate(groundChunk, newSection, Quaternion.identity);

        return platformGenerator.SpawnPlatformChunk_Left(newSection);

    }

    */

    #endregion
    private IEnumerator AllPurposeTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}


