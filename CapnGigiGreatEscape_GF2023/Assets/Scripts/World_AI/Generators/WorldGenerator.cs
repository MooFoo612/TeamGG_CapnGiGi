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
    

    // Constants -----------------------------------------------
    private const float DISTANCE_TO_SPAWN_SECTION_GROUND = 40f;
    private const float DISTANCE_TO_SPAWN_SECTION_BG = 10f;
    private const float DISTANCE_TO_DELETE_SECTION = 180f;

    private const float DISTANCE_TO_SPAWN_SECTION_GROUND_LEFT = -40f;
    private const float DISTANCE_TO_SPAWN_SECTION_BG_LEFT = -10f;
    private const float DISTANCE_TO_DELETE_SECTION_LEFT = -180f;

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
    //public string platforms = "PlatformChunk"; 
    //// Create a list to fill with the current platform chunks in the game
    //private List<GameObject> activePlatformChunks = new List<GameObject>();
    //public string grounds = "GroundChunk"; 
    //// Create a list to fill with the current ground chunks in the game
    //private List<GameObject> activeGroundChunks = new List<GameObject>();
    private float timer = 0f; 
    public bool _justReversed;
    public bool JustReversed { 
        get{
            return _justReversed;
            // Return the value inside the isMoving variable just created
        } private set {
            // Set isMoving to the value is gonna be passed into the set
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
        //Debug.Log("distance marker: " + distanceMarker);    

        // Ensure world isn't reversed
        reversedWorld = false;

        // Access Ground Generator Script
        groundGenerator = GameObject.FindObjectOfType(typeof(GroundFactory)) as GroundFactory;

        // Access Platform Generator Script
        platformGenerator = GameObject.FindObjectOfType(typeof(PlatformFactory)) as PlatformFactory;

        // Access Background Generator Script
        backgroundGenerator = GameObject.FindObjectOfType(typeof(BackgroundFactory)) as BackgroundFactory;

        StartCoroutine(GenerateWorld_Right());
        StartCoroutine(GenerateWorld_Left());

    }

    #region World Spawner
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

    private void Update()
    {

        
        /*
        // Find all game objects with the platform tag and add them to the list
        GameObject[] activePlatforms = GameObject.FindGameObjectsWithTag(platforms);
        foreach (GameObject obj in activePlatforms){
            activePlatformChunks.Add(obj);
        }
        // Find all game objects with the ground tag and add them to the list
        GameObject[] activeGrounds = GameObject.FindGameObjectsWithTag(grounds);
        foreach (GameObject obj in activeGrounds){
            activeGroundChunks.Add(obj);
            // Delete duplicates 
            //if(activePlatformChunks.Count !=  activePlatformChunks.Distinct.Count){
            //    Destroy(obj);
            //}
        }
        

        // Remove any platforms that have been destroyed or removed from the scene from the list 
        for (int i = activePlatformChunks.Count - 1; i >= 0; i--){
            if (activePlatformChunks[i] == null){
                Debug.Log("I'm removing something");
                activePlatformChunks.RemoveAt(i);
            }
        }
        // Remove any grounds that have been destroyed or removed from the scene from the list 
        for (int i = activeGroundChunks.Count - 1; i >= 0; i--){
            if (activeGroundChunks[i] == null){
                Debug.Log("I'm removing something");
                activeGroundChunks.RemoveAt(i);
            }
        }

        Debug.Log("Active platforms array elements: " + activePlatformChunks.Count);
        Debug.Log("Active GROUNDS array elements: " + activeGroundChunks.Count);
        */
        // Check if player reached the distance to reverse the game and increase the difficulty 
        if(Vector3.Distance(player.transform.position, distanceMarker) > DISTANCE_TO_REVERSE){

            // Update the bool
            reversedWorld = true;
            Debug.Log("reverseWord value: " + reversedWorld);
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                
                JustReversed =true;
                // Reset the timer
                timer = 0f;
            }else{
                JustReversed =false;
            }

            /*
            // Destroy all but the last platform added to the list
            for (int i = 0; i < activePlatformChunks.Count-1 ; i++){
                Destroy(activePlatformChunks[i]);
            }
            // Destroy all but the last ground added to the list
            for (int i = 0; i < activeGroundChunks.Count-1; i++){
                Destroy(activeGroundChunks[i]);
            }
            // NEXT STEP create the new marker and make it a loop or recognize the directions
            */
        }else{
            // Update the bool 
            reversedWorld = false;
            Debug.Log("reverseWord value: " + reversedWorld);
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
    private IEnumerator GenerateWorld_Left()
    {
        
        while (reversedWorld)
        {
            // Limit to 1sec
            yield return new WaitForSeconds(1f);

            // Set groundEndRight to last spawned section for check
            groundEnd_Left = groundGenerator.groundEnd_Left;


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

            if (reversedWorld == false)
            {
                break;
            }
        }
    }


}


