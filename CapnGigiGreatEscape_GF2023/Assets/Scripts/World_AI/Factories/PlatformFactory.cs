using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Factory))]
public class PlatformFactory : Factory
{
    #region Variables
    [SerializeField] private Transform platformStart;
    //[SerializeField] private Transform platformStartLeft;
    Transform lastPlatformEnd_Left;
    [SerializeField] private GameObject player;
    WorldGenerator worldGenerator;

    private Transform platformChunk;
    private Transform platformParent;

    // Positions for Spawning Chunks
    [HideInInspector] public Vector3 platformEnd_Right;
    [HideInInspector] public Vector3 platformEnd_Left;

    #endregion

    #region Startup
    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Nest in hierarchy
        platformParent = GameObject.Find("PlatformChunks_Active").transform;
        worldGenerator = gameObject.GetComponent<WorldGenerator>();

        // Find the child EndPosition object in the GameStart parent
        platformEnd_Right = platformStart.Find("PlatformEnd_Right").position;
        //platformEnd_Left = platformStart.Find("PlatformEnd_Left").position;
        //if(platformEnd_Left != null){Debug.Log("platformEnd_Left found");}



    }

    void Update(){
        if(worldGenerator.reversedWorld){
        platformEnd_Left = lastPlatformEnd_Left.Find("PlatformEnd_Left").position;
    }}
    #endregion


    #region Spawn Platforms to the Right
    public void SpawnPlatformChunk_Right()
    {
        platformChunk = RandomChunkerizer();

        //Get the transform to refrence the next End Position
        Transform lastPlatformEnd_Right = SpawnPlatformChunk_Right(platformChunk, platformEnd_Right, platformParent);

        platformEnd_Right = lastPlatformEnd_Right.Find("PlatformEnd_Right").position;

        //Debug.Log("Platform Spawned: " + platformChunkActivated);

    }
    public Transform SpawnPlatformChunk_Right(Transform platformChunk, Vector3 nextChunk, Transform platformParent)
    {
        // Spawn the Platform Chunk and log to AI count
        Transform nextPlatformChunk_Right = Instantiate(platformChunk, nextChunk, Quaternion.identity, platformParent);

        platformChunkActivated += 1;
        // Return the transform for sister method
        return nextPlatformChunk_Right;
    }
    #endregion

    #region Spawn Platforms to the Left
    public void SpawnPlatformChunk_Left()
    {
        platformChunk = RandomChunkerizer();

        //Get the transform to refrence the next End Position
        lastPlatformEnd_Left = SpawnPlatformChunk_Left(platformChunk, platformEnd_Left, platformParent);
        platformEnd_Left = GameObject.FindGameObjectWithTag("pltfEndLeft").GetComponent<Transform>().position;
        if (platformEnd_Left != null){Debug.Log("hey i got the ground end left" + platformEnd_Left);}

        //Debug.Log("Platform Spawned: " + platformChunkActivated);

    }
    public Transform SpawnPlatformChunk_Left(Transform platformChunk, Vector3 nextChunk, Transform platformParent)
    {
        // Spawn the Platform Chunk and log to AI count
        Transform nextPlatformChunk_Left = Instantiate(platformChunk, nextChunk, Quaternion.identity, platformParent);

        platformChunkActivated += 1;
        // Return the transform for sister method
        return nextPlatformChunk_Left;
    }
    /*
    public void SpawnPlatformChunk_Left()
    {
        //Get the transform to refrence the end of previous chunk
        Transform lastPlatformEnd_Left = SpawnPlatformChunk_Left(platformEnd_Left);
        platformEnd_Left = lastPlatformEnd_Left.Find("PlatformEnd_Left").position;
        Debug.Log("Platform Spawned: " + Factory.platformChunkActivated);

    }
    public Transform SpawnPlatformChunk_Left(Vector3 nextChunk)
    {
        // Get random Platform Chunk from List
        platformChunk = RandomChunkerizer();

        // Spawn the Platform chunk and log to AI count
        Transform nextPlatformChunk_Left = Instantiate(platformChunk, nextChunk, Quaternion.identity);
        Factory.platformChunkActivated += 1;

        // Return the transform for sister method
        return nextPlatformChunk_Left;
    }
    */
    #endregion

    #region Random "Chunkerizer"
    private Transform RandomChunkerizer()
    {
        //platformList = lf.GeneratePlatformList();
        int randomPick = UnityEngine.Random.Range(0, platformChunks.Count - 1);
        GameObject randomChunk = platformChunks[randomPick];
        Transform platformChunk = randomChunk.transform;
        return platformChunk;
    }
    #endregion
}

