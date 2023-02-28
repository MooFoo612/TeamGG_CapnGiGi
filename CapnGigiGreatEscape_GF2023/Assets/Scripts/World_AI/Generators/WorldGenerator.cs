using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class WorldGenerator : ListFactory
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
    private bool reversedWorld;

    // Distance management ---------------
    private float totalDistance;
    private GameObject distanceMarkerObj;
    private float distanceMarker;

    // Constants -----------------------------------------------
    private const float DISTANCE_TO_SPAWN_SECTION_GROUND = 40f;
    private const float DISTANCE_TO_SPAWN_SECTION_BG = 80f;
    private const float DISTANCE_TO_DELETE_SECTION = 180f;

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

        // Distance Marker
        distanceMarkerObj = GameObject.Find("DistanceMarker");
        distanceMarker = distanceMarkerObj.transform.position.x;

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
    private void SpawnGameWorld_Right()
    {
        SpawnGroundChunk_Right();
        SpawnPlatformChunk_Right();
    }

    #endregion

    #region Background Spawner

    public void SpawnBackground_Right()
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

    private void Update()
    {
       
    }

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
            groundEnd_Right = groundGenerator.groundEnd_Right;


            // If the player is close enough
            if (Vector3.Distance(player.transform.position, groundEnd_Right) < DISTANCE_TO_SPAWN_SECTION_GROUND)
            {
                // Spawn another section
                SpawnGameWorld_Right();
            }

            if (Vector3.Distance(player.transform.position, bgEnd_Right) < DISTANCE_TO_SPAWN_SECTION_BG)
            {
                SpawnBackground_Right();
            }

            if (reversedWorld == true)
            {
                break;
            }
        }
    }
}


