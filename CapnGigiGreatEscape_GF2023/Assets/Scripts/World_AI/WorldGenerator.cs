using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
    #region Variables
    //[SerializeField] 
    private Transform groundChunk;
    //[SerializeField]
    //private Transform platformChunk;
    //[SerializeField]
    //private Transform worldStart;
    [SerializeField] private Transform groundStart;
    [SerializeField] private Transform platformStart;
    [SerializeField] private GameObject player;
    private GroundGenerator groundGenerator;
    private PlatformGenerator platformGenerator;
    private EnemyGenerator enemyGenerator;
    private bool reversedWorld;

    private const float DISTANCE_TO_SPAWN_SECTION_RIGHT = 20f;
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
        //groundEndRight = groundStart.Find("GroundEnd_Right").position;
        //platformEndRight = platformStart.Find("PlatformEnd_Right").position;
        // worldEndRight = (platformStart - groundStart) / 2; 

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundGenerator)) as GroundGenerator;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformGenerator)) as PlatformGenerator;

        // Acess Enemy Generator Script
        enemyGenerator = GameObject.FindObjectOfType(typeof(EnemyGenerator)) as EnemyGenerator;

        StartCoroutine(ChunkSpawnTimer());

    }
    private void Start()
    {
    }
    private void Update()
    {
    }

    #region Ground Spawner
    public void SpawnGroundChunk_Right()
    {
        groundGenerator.SpawnGroundChunk_Right();
    }

    public void SpawnGroundChunk_Left()
    {
        groundGenerator.SpawnGroundChunk_Left();
    }

    #endregion

    #region Platform Spawner
    public void SpawnPlatformChunk_Right()
    {
        platformGenerator.SpawnPlatformChunk_Right();
    }

    public void SpawnPlatformChunk_Left()
    {
        platformGenerator.SpawnPlatformChunk_Left();
    }

    #endregion
    private IEnumerator ChunkSpawnTimer()
    {
        
        while (!reversedWorld)
        {
            yield return new WaitForSeconds(1f);

            groundEndRight = groundGenerator.groundEnd_Right;

            if (Vector3.Distance(player.transform.position, groundEndRight) < DISTANCE_TO_SPAWN_SECTION_RIGHT)
            {
                // Spawn another section
                SpawnGroundChunk_Right();
                SpawnPlatformChunk_Right();
                //Debug.Log("World Chunk generated");
            }
        }
 
    }
}


