using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------
    [SerializeField] private Transform groundStart;
    [SerializeField] private Transform platformStart;
    [SerializeField] private GameObject player;

    // Generators -------------------------------------
    private GroundFactory groundGenerator;
    private PlatformFactory platformGenerator;
    private EnemyFactory enemyGenerator;
    private CollectableFactory collectableGenerator;
    private TrapFactory trapGenerator;
    private bool reversedWorld;

    // Distance management ---------------
    private float totalDistance;
    private GameObject distanceMarkerObj;
    private Vector3 distanceMarker;

    // Constants -----------------------------------------------
    private const float DISTANCE_TO_SPAWN_SECTION = 40f;
    private const float DISTANCE_TO_DELETE_SECTION = 180f;

    // Marker Positions---------------
    private Vector3 worldEndRight;
    private Vector3 worldEndLeft;
    //--------------------------------
    private Vector3 groundEndRight;
    private Vector3 groundEndLeft;
    //--------------------------------
    private Vector3 platformEndRight;
    private Vector3 platformEndLeft;
    //--------------------------------

    #endregion

    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Distance Marker
        distanceMarkerObj = GameObject.Find("DistanceMarker");
        distanceMarker = distanceMarkerObj.transform.position;

        // Ensure world isn't reversed
        reversedWorld = false;

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundFactory)) as GroundFactory;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformFactory)) as PlatformFactory;

        // Acess Enemy Generator Script
        enemyGenerator = GameObject.FindObjectOfType(typeof(EnemyFactory)) as EnemyFactory;

        // Access Collectable Generator Script
        collectableGenerator = GameObject.FindObjectOfType(typeof(CollectableFactory)) as CollectableFactory;

        // Access Trap Generator Script
        trapGenerator = GameObject.FindObjectOfType(typeof(TrapFactory)) as TrapFactory;

        StartCoroutine(GenerateWorld_Right());

    }

    #region World Spawner
    private void SpawnGameWorld_Right()
    {
        SpawnGroundChunk_Right();
        SpawnPlatformChunk_Right();
    }

    #endregion

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
    private IEnumerator GenerateWorld_Right()
    {
        
        while (!reversedWorld)
        {
            // Limit to 1sec
            yield return new WaitForSeconds(1f);

            // Set groundEndRight to last spawned section for check
            groundEndRight = groundGenerator.groundEnd_Right;

            // If the player is close enough
            if (Vector3.Distance(player.transform.position, groundEndRight) < DISTANCE_TO_SPAWN_SECTION)
            {
                // Spawn another section
                SpawnGameWorld_Right();
            }

            if (reversedWorld == true)
            {
                break;
            }
        }
    }
}

