using UnityEngine;
using System.Collections.Generic;

public class PlatformFactory : ListFactory
{
    #region Variables
    [SerializeField] private Transform platformStart;
    [SerializeField] private GameObject player;

    private Transform platformChunk;

    // Positions for Spawning Chunks
    [HideInInspector] public Vector3 platformEnd_Right;
    [HideInInspector] public Vector3 platformEnd_Left;

    #endregion

    #region Startup
    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Find the child EndPosition object in the GameStart parent
        platformEnd_Right = platformStart.Find("PlatformEnd_Right").position;
    }
    #endregion

    #region Spawn Platforms to the Right
    public void SpawnPlatformChunk_Right()
    {
        platformChunk = RandomChunkerizer();

        //Get the transform to refrence the next End Position
        Transform lastPlatformEnd_Right = SpawnPlatformChunk_Right(platformChunk, platformEnd_Right);
        platformEnd_Right = lastPlatformEnd_Right.Find("PlatformEnd_Right").position;

        Debug.Log("Platform Spawned: " + ListFactory.platformChunkActivated);

    }
    public Transform SpawnPlatformChunk_Right(Transform platformChunk, Vector3 nextChunk)
    {
        // Spawn the Platform Chunk and log to AI count
        Transform nextPlatformChunk_Right = Instantiate(platformChunk, nextChunk, Quaternion.identity);

        ListFactory.platformChunkActivated += 1;

        // Return the transform for sister method
        return nextPlatformChunk_Right;
    }
    #endregion

    #region Spawn Platforms to the Left
    public void SpawnPlatformChunk_Left()
    {
        //Get the transform to refrence the end of previous chunk
        Transform lastPlatformEnd_Left = SpawnPlatformChunk_Left(platformEnd_Left);
        platformEnd_Left = lastPlatformEnd_Left.Find("PlatformEnd_Left").position;
        Debug.Log("Platform Spawned: " + ListFactory.platformChunkActivated);

    }
    public Transform SpawnPlatformChunk_Left(Vector3 nextChunk)
    {
        // Get random Platform Chunk from List
        platformChunk = RandomChunkerizer();

        // Spawn the Platform chunk and log to AI count
        Transform nextPlatformChunk_Left = Instantiate(platformChunk, nextChunk, Quaternion.identity);
        ListFactory.platformChunkActivated += 1;

        // Return the transform for sister method
        return nextPlatformChunk_Left;
    }
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

