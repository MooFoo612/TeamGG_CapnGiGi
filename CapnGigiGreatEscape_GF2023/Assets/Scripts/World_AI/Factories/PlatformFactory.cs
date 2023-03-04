using UnityEngine;
using System.Collections.Generic;

public class PlatformFactory : Factory
{
    #region Variables
<<<<<<< HEAD
    [SerializeField] private Transform platformStart;
=======
    [SerializeField] Transform platformStart;
>>>>>>> 1d764ea (Tidied up factory scripts)
    [SerializeField] private GameObject player;

    private Transform platformChunk;
    private Transform platformParent;

    // Positions for Spawning Chunks
    [HideInInspector] Vector3 platformEnd_Right;
    [HideInInspector] Vector3 platformEnd_Left;

    #endregion

    #region Startup
    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");

        // Nest in hierarchy
        platformParent = GameObject.Find("PlatformChunks_Active").transform;

        // Find the child EndPosition object in the GameStart parent
        platformEnd_Right = platformStart.Find("PlatformEnd_Right").position;
<<<<<<< HEAD
        //platformEnd_Left = platformStart.Find("PlatformEnd_Left").position;


    }
=======
    }

>>>>>>> 1d764ea (Tidied up factory scripts)
    #endregion

    #region Spawn Platforms to the Right
    public void SpawnPlatformChunk_Right()
    {
        // Select random chunk from list
        platformChunk = RandomChunkerizer();

<<<<<<< HEAD
        //Get the transform to refrence the next End Position
        Transform lastPlatformEnd_Right = SpawnPlatformChunk_Right(platformChunk, platformEnd_Right, platformParent);
        platformEnd_Right = lastPlatformEnd_Right.Find("PlatformEnd_Right").position;
=======
        //Get the transform of instantiated object to refrence the next End Position
        Transform spawnedPlatform_Right = SpawnPlatformChunk_Right(platformChunk, platformEnd_Right, platformParent);

        // Get marker positions from spawned game object
        platformEnd_Right = spawnedPlatform_Right.Find("PlatformEnd_Right").position;
        platformEnd_Left = spawnedPlatform_Right.Find("PlatformEnd_Left").position;
>>>>>>> 1d764ea (Tidied up factory scripts)

        // Debug message
        if (platformEnd_Left != null) { Debug.Log("Left:" + platformEnd_Left + " |  Right: " + platformEnd_Right); }

    }
    public Transform SpawnPlatformChunk_Right(Transform platformChunk, Vector3 nextChunk, Transform platformParent)
    {
        // Spawn the Platform Chunk and log to Factory 
        Transform nextSpawn_Right = Instantiate(platformChunk, nextChunk, Quaternion.identity, platformParent);
        platformChunkActivated += 1;

        // Return the transform for sister method
        return nextSpawn_Right;
    }

    #endregion

    #region Spawn Platforms to the Left
    public void SpawnPlatformChunk_Left()
    {
        // Select random chunk from the list
        platformChunk = RandomChunkerizer();

        //Get the transform to refrence the next End Position
<<<<<<< HEAD
        Transform lastPlatformEnd_Left = SpawnPlatformChunk_Left(platformChunk, platformEnd_Left, platformParent);
        platformEnd_Left = lastPlatformEnd_Left.Find("PlatformEnd_Left").position;
=======
        Transform spawnedPlatform_Left = SpawnPlatformChunk_Left(platformChunk, platformEnd_Left, platformParent);
        
        // Get marker positions for the spawned game objects
        platformEnd_Left = spawnedPlatform_Left.Find("PlatformEnd_Left").position;
        platformEnd_Right = spawnedPlatform_Left.Find("PlatformEnd_Right").position;
>>>>>>> 1d764ea (Tidied up factory scripts)

        // Debug message
        if (platformEnd_Left != null) { Debug.Log("Left:" + platformEnd_Left + " |  Right: " + platformEnd_Right); }

    }

    public Transform SpawnPlatformChunk_Left(Transform platformChunk, Vector3 nextChunk, Transform platformParent)
    {
        // Spawn the Platform Chunk and log to factory
        Transform nextSpawn_Left = Instantiate(platformChunk, nextChunk, Quaternion.identity, platformParent);
        platformChunkActivated += 1;

        // Return the transform for sister method
        return nextSpawn_Left;
    }

    #endregion

    #region Random "Chunkerizer"
    private Transform RandomChunkerizer()
    {
        // Integer for random index
        int randomPick = UnityEngine.Random.Range(0, platformChunks.Count);

        // Grab random platform chunk
        GameObject randomChunk = platformChunks[randomPick];

        // Get the objects transform and return it
        Transform platformChunk = randomChunk.transform;
        return platformChunk;
    }
    #endregion
}

