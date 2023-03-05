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
    [Header("Reverse Status")] 
    public bool reversedWorld;
    //-------------------------------------------------
    [Header("Setup")]
    [SerializeField] Transform groundStart;
    [SerializeField] Transform platformStart;
    [SerializeField] Transform firstMarker;
    [SerializeField] GameObject player;
    // Generators -------------------------------------
    [Header("Scripts")]
    GroundWarehouse groundGenerator;
    PlatformWarehouse platformGenerator;
    BackgroundWarehouse backgroundGenerator;
    // Distance management ----------------------------------  
    [Header("Distance")]
    float totalDistance;
    Vector3 distanceMarker;
    Vector3 playerPosition;
    // Constants -----------------------------------------------
    const float DISTANCE_TO_SPAWN_SECTION = 40f;
    const float DISTANCE_TO_SPAWN_BG = 10f;
    const float DISTANCE_TO_REVERSE = 20f;
    // Marker Positions---------------   
    [Header("Markers")]
    Vector3 groundEnd_Right;
    Vector3 groundEnd_Left;
    //--------------------------------
    Vector3 platformEnd_Right;
    Vector3 platformEnd_Left;
    //--------------------------------
    Vector3 bgEnd_Right;
    Vector3 bgEnd_Left;
    //--------------------------------
    #endregion
    

    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Distance Marker (maybe move in the update)
        distanceMarker = GameObject.Find("DistanceMarker").transform.position;

        // Ensure world isn't reversed
        reversedWorld = false;

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundWarehouse)) as GroundWarehouse;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformWarehouse)) as PlatformWarehouse;

        // Access Background Generator Script
        backgroundGenerator = GameObject.FindObjectOfType(typeof(BackgroundWarehouse)) as BackgroundWarehouse;

        StartCoroutine(GenerateWorld());
    }
    #region World Spawner

    private IEnumerator GenerateWorld()
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

            if (reversedWorld)
            {
                break;
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

    #region Reversal
    public void ReverseCheck(Vector3 playerPosition, Vector3 distanceMarker)
    {
        float currentDistance = Vector3.Distance(playerPosition, distanceMarker);

        totalDistance += currentDistance;
        currentDistance = 0;

        if (Vector3.Distance(player.transform.position, distanceMarker) > DISTANCE_TO_REVERSE)
        {
            reversedWorld = false;
        }
        else
        {
            reversedWorld = true;
        }
    }

    #endregion

}
