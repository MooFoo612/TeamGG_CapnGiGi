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
    private Vector3 distanceMarker;
    private Vector3 firstDistanceMarkerPosition;
    

    // Constants -----------------------------------------------
    private const float DISTANCE_TO_SPAWN_SECTION_GROUND = 40f;
    private const float DISTANCE_TO_SPAWN_SECTION_BG = 10f;
    private const float DISTANCE_TO_DELETE_SECTION = 180f;

    private const float DISTANCE_TO_SPAWN_SECTION_GROUND_LEFT = 40f;
    private const float DISTANCE_TO_SPAWN_SECTION_BG_LEFT = 10f;

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

    // Platforms and Grounds chunks in game management ----------------------

    public bool _justReversed;
    public bool JustReversed 
    { 
        get
        {
            return _justReversed;
        } 
        private set 
        {
            _justReversed = value;
        }
    }
    #endregion

void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Distance Marker (maybe move in the update)
        distanceMarkerObj = GameObject.Find("DistanceMarker");
        distanceMarker = distanceMarkerObj.transform.position;
        firstDistanceMarkerPosition = distanceMarkerObj.transform.position;
        //Debug.Log("distance marker: " + distanceMarker);    

        // Ensure world isn't reversed
        reversedWorld = false;

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundFactory)) as GroundFactory;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformFactory)) as PlatformFactory;

        // Access Background Generator Script
        backgroundGenerator = GameObject.FindObjectOfType(typeof(BackgroundFactory)) as BackgroundFactory;

        StartCoroutine(GenerateWorld());

        

    }

    #region World Spawner
    private void SpawnGameWorld_Right()
    {
        SpawnGroundChunk_Right();
        SpawnPlatformChunk_Right();
<<<<<<< HEAD
        if(Vector3.Distance(player.transform.position, distanceMarker) > DISTANCE_TO_REVERSE){
            // Update the bool 
            
            JustReversed = true;
            
            distanceMarker = player.transform.position;
            
            
            
            
        }else{
            // Update the bool 
            
            Debug.Log("reverseWord value: " + reversedWorld);
            JustReversed = false;
        }
=======
>>>>>>> 1d764ea (Tidied up factory scripts)
    }

    private void SpawnGameWorld_Left()
    {
        SpawnGroundChunk_Left();
        SpawnPlatformChunk_Left();
        Debug.Log("changed marker position");
        distanceMarker = firstDistanceMarkerPosition;
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

    private void Update()       
    {
        Debug.Log("reverseWord value: " + reversedWorld); 
        
        if (distanceMarker != firstDistanceMarkerPosition)
        {
            reversedWorld = true;
            Debug.Log("Hello");
        } 
        else 
        {
            reversedWorld = false;
        }
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


    private IEnumerator GenerateWorld()
    {
        if (!reversedWorld)
        {
            // Limit to 1sec
            yield return new WaitForSeconds(0.1f);

            // Set groundEndRight to last spawned section for check
            groundEnd_Right = groundGenerator.groundEnd_Right;

            // Check distance to reverse world
            if (Vector3.Distance(player.transform.position, distanceMarker) > DISTANCE_TO_REVERSE)
            {
                // Update the bool            
                JustReversed = true;
                distanceMarker = player.transform.position;
            }
            else
            {
                // Update the bool            
                Debug.Log("reverseWord value: " + reversedWorld);
                JustReversed = false;
            }

            // Check distance to spawn ground
            if (Vector3.Distance(player.transform.position, groundEnd_Right) < DISTANCE_TO_SPAWN_SECTION_GROUND)
            {
                // Spawn another section
                SpawnGameWorld_Right();
            }

            // Check distance to spawn background
            if (Vector3.Distance(player.transform.position, bgEnd_Right) < DISTANCE_TO_SPAWN_SECTION_BG)
            {
                SpawnBackground_Right();
            }
        }
        else
        {
            Debug.Log("Hey i'm in the left coroutine");

            // Limit to 1sec
            yield return new WaitForSeconds(0.1f);

            // Set groundEndRight to last spawned section for check
            groundEnd_Left = groundGenerator.groundEnd_Left;

            // Check distance to reverse world
            if (Vector3.Distance(player.transform.position, distanceMarker) > DISTANCE_TO_REVERSE)
            {
                // Update the bool            
                JustReversed = true;
                distanceMarker = player.transform.position;
            }
            else
            {
                // Update the bool            
                Debug.Log("reverseWord value: " + reversedWorld);
                JustReversed = false;
            }

            // If the player is close enough
            if (Vector3.Distance(player.transform.position, groundEnd_Left) < DISTANCE_TO_SPAWN_SECTION_GROUND_LEFT)
            {
                
                // Spawn another section
                SpawnGameWorld_Left();
            }

            if (Vector3.Distance(player.transform.position, bgEnd_Left) < DISTANCE_TO_SPAWN_SECTION_BG_LEFT)
            {
                SpawnBackground_Left();
            }
        }
    }
}


