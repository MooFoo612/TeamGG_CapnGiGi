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
    Transform groundStart;
    Transform platformStart;
    Transform firstMarker;
    GameObject gigi;
    // Generators -------------------------------------
    [Header("Scripts")]
    GroundWarehouse gw;
    PlatformWarehouse pg;
    BackgroundWarehouse bg;
    // Distance management ----------------------------------  
    [Header("Distance")]
    float totalDistance;
    GameObject distanceMarkerObj;
    Vector3 distanceMarker;
    Vector3 playerPosition;
    // Constants -----------------------------------------------
    const float DISTANCE_TO_SPAWN_SECTION = 40f;
    const float DISTANCE_TO_SPAWN_BG = 60f;
    const float DISTANCE_TO_REVERSE = 200f;
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
        gigi = GameObject.Find("CapnGigi");

        // Distance Marker
        distanceMarkerObj = GameObject.Find("DistanceMarker");
        distanceMarker = distanceMarkerObj.transform.position;

        // Ensure world isn't reversed
        reversedWorld = false;

        // Access Ground Generator Script
        gw = GameObject.FindObjectOfType(typeof(GroundWarehouse)) as GroundWarehouse;

        // Access Platform Generator Script
        pg = GameObject.FindObjectOfType(typeof(PlatformWarehouse)) as PlatformWarehouse;

        // Access Background Generator Script
        bg = GameObject.FindObjectOfType(typeof(BackgroundWarehouse)) as BackgroundWarehouse;

        StartCoroutine(GenerateWorld());
    }
    private void FixedUpdate()
    {
        ReverseCheck(playerPosition, distanceMarker);
    }

    #region World Spawner

    private IEnumerator GenerateWorld()
    {
        while (reversedWorld)
        {
            // Set groundEndRight to last spawned section for check
            groundEnd_Left = gw.groundEnd_Left;

            // Get player position
            playerPosition = gigi.transform.position;

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
            groundEnd_Right = gw.groundEnd_Right;

            // Get player position
            playerPosition = gigi.transform.position;

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
        bg.GenerateBackground();
    }
    public void SpawnBackground_Left()
    {
        bg.GenerateBackground();
    }
    #endregion

    #region Ground Spawner
    public void SpawnGroundChunk_Right()
    {
        gw.SpawnGroundChunk_Right();
    }
    public void SpawnGroundChunk_Left()
    {
        gw.SpawnGroundChunk_Left();
    }
    #endregion

    #region Platform Spawner
    public void SpawnPlatformChunk_Right()
    {
        pg.SpawnPlatformChunk_Right();
    }
    public void SpawnPlatformChunk_Left()
    {
        pg.SpawnPlatformChunk_Left();
    }
    #endregion

    #region Reversal
    public void ReverseCheck(Vector3 playerPosition, Vector3 distanceMarker)
    {
        float currentDistance = Vector3.Distance(playerPosition, distanceMarker);

        if (currentDistance > DISTANCE_TO_REVERSE)
        {
            Destroy(distanceMarkerObj);
            Instantiate(distanceMarkerObj, playerPosition, Quaternion.identity);
            distanceMarker = distanceMarkerObj.transform.position;
            totalDistance += currentDistance;
            currentDistance = 0;
            reversedWorld = false;
            return;
        }
        else
        {
            totalDistance += currentDistance;
            currentDistance = 0;
            return;
        }
    }

    #endregion

}
