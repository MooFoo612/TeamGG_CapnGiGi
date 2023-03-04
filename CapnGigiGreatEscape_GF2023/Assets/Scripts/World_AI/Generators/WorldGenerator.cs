using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class WorldGenerator : Factory
{
    #region Variables
    // ------------------------------------------------
    [SerializeField] private Transform groundStart;
    [SerializeField] private Transform platformStart;
    [SerializeField] private GameObject player;
    // Generators -------------------------------------
    private GroundFactory groundGenerator;
    private PlatformFactory platformGenerator;
    private BackgroundFactory backgroundGenerator;
    public bool reversedWorld;
    // Distance management ---------------
    private float totalDistance;
    private GameObject distanceMarkerObj;
    private Vector3 playerPosition;
    // Constants -----------------------------------------------
    private const float DISTANCE_TO_SPAWN_SECTION = 40f;
    private const float DISTANCE_TO_SPAWN_BG = 10f;
    private const float DISTANCE_TO_REVERSE = 50f;

    // Normal variables
    bool toReverse;
    // Marker Positions---------------   
    private Vector3 groundEnd_Right;
    private Vector3 groundEnd_Left;
    //--------------------------------
    private Vector3 platformEnd_Right;
    private Vector3 platformEnd_Left;
    //--------------------------------
    private Vector3 bgEnd_Right;
    private Vector3 bgEnd_Left;
    //--------------------------------
    #endregion
    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");
        // Distance Marker (maybe move in the update)
        distanceMarkerObj = GameObject.Find("DistanceMarker");

        // Ensure world isn't reversed
        reversedWorld = false;

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundFactory)) as GroundFactory;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformFactory)) as PlatformFactory;

        // Access Background Generator Script
        backgroundGenerator = GameObject.FindObjectOfType(typeof(BackgroundFactory)) as BackgroundFactory;

        StartCoroutine(GenerateWorld_Right());
    }
    #region World Spawner

    private IEnumerator GenerateWorld_Right()
    {
        while (reversedWorld)
        {
            // Set groundEndRight to last spawned section for check
            groundEnd_Left = groundGenerator.groundEnd_Left;

            // Get player position
            playerPosition = player.transform.position;

            // Check distance to spawn ground
            if (Vector3.Distance(playerPosition, groundEnd_Left) < DISTANCE_TO_SPAWN_SECTION)
            {
                // Spawn another section
                SpawnGameWorld_Left();
            }
            // Check distance to spawn background
            if (Vector3.Distance(playerPosition, bgEnd_Left) < DISTANCE_TO_SPAWN_BG)
            {
                SpawnBackground_Left();
            }

            if (!reversedWorld)
            {
                break;
            }
            // Limit to 1sec
            yield return new WaitForSeconds(0.1f);
        }

        while (!reversedWorld)
        {
            // Set groundEndRight to last spawned section for check
            groundEnd_Right = groundGenerator.groundEnd_Right;

            // Get player position
            playerPosition = player.transform.position;

            // Check distance to spawn ground
            if (Vector3.Distance(playerPosition, groundEnd_Right) < DISTANCE_TO_SPAWN_SECTION)
            {
                // Spawn another section
                SpawnGameWorld_Right();
            }
            // Check distance to spawn background
            if (Vector3.Distance(playerPosition, bgEnd_Right) < DISTANCE_TO_SPAWN_BG)
            {
                SpawnBackground_Right();
            }

            // Limit to 1sec
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator GenerateWorld_Left()
    {
        // Limit to 1sec
        yield return new WaitForSeconds(0.1f);
        // Set groundEndRight to last spawned section for check
        groundEnd_Right = groundGenerator.groundEnd_Left;

        // Check distance to spawn ground
        if (Vector3.Distance(playerPosition, groundEnd_Left) < DISTANCE_TO_SPAWN_SECTION)
        {
            // Spawn another section
            SpawnGameWorld_Left();
        }
        // Check distance to spawn background
        if (Vector3.Distance(playerPosition, bgEnd_Left) < DISTANCE_TO_SPAWN_BG)
        {
            SpawnBackground_Left();
        }

        // Limit to 1sec
        yield return new WaitForSeconds(0.1f);
    }
    private void SpawnGameWorld_Right()
    {
        SpawnGroundChunk_Right();
        SpawnPlatformChunk_Right();
    }
    private void SpawnGameWorld_Left()
    {
        SpawnGroundChunk_Left();
        SpawnPlatformChunk_Left();
    }
    #endregion
    #region Background Spawner
    public void SpawnBackground_Right()
    {
        backgroundGenerator.GenerateBackground();
    }
    public void SpawnBackground_Left()
    {
        backgroundGenerator.GenerateBackground();
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
    
}
