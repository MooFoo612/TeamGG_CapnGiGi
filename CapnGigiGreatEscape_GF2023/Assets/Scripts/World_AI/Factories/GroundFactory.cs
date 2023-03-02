using UnityEngine;
using System;
using System.Collections.Generic;

public class GroundFactory : Factory
{
    #region Variables
    [SerializeField] private Transform groundStart;
    [SerializeField] private GameObject player;

    // Variables for the objects
    private GameObject groundObj;
    private Transform groundChunk;

    // Positions for Spawning Chunks
    public Vector3 groundEnd_Right;
    public Vector3 groundEnd_Left;

    // Hierarchy Parent
    private Transform groundParent;
    #endregion

    #region Startup
    void Awake()
    {
        // Hierarchy Parent
        groundParent = GameObject.Find("GroundChunks_Active").transform;

        // Access the player
        player = GameObject.Find("CapnGigi");

        // Find the child EndPosition object in the GameStart parent
        groundEnd_Right = groundStart.Find("GroundEnd_Right").transform.position;
        //groundEnd_Left = groundStart.Find("GroundEnd_Left").transform.position;

    }
    #endregion

    #region Spawn Platforms to the Right
    public void SpawnGroundChunk_Right()
    {

        int randomPick = UnityEngine.Random.Range(0, groundChunks.Count);
        Transform randomChunk = groundChunks[randomPick].transform;
        //Transform groundChunk = randomChunk.transform;

        // Spawn the Transform at the last end of section location
        Transform lastGroundEnd_Right = SpawnGroundChunk_Right(randomChunk, groundEnd_Right, groundParent);

        // Find the next end of section in the new Transform
        groundEnd_Right = lastGroundEnd_Right.Find("GroundEnd_Right").position;

    }
    public Transform SpawnGroundChunk_Right(Transform groundChunk, Vector3 nextPosition, Transform groundParent)
    {
        // Spawn the Platform Chunk
        Transform nextGroundChunk_Right = Instantiate(groundChunk, nextPosition, Quaternion.identity, groundParent);
        groundChunkActivated += 1;

        // Return the transform for sister method
        return nextGroundChunk_Right;
    }
    #endregion

    #region Spawn Platforms to the Left
    public void SpawnGroundChunk_Left()
    {
        int randomPick = UnityEngine.Random.Range(0, groundChunks.Count -1);
        Transform randomChunk = groundChunks[randomPick].transform;

        // Spawn the Transform at the last end of section location
        Transform lastGroundEnd_Left = SpawnGroundChunk_Left(randomChunk, groundEnd_Left, groundParent);

        // Find the next end of section in the new Transform
        groundEnd_Left = lastGroundEnd_Left.Find("GroundEnd_Left").position;
    }
    //public Transform SpawnGroundChunk_Left(Vector3 nextChunk)
    //{
    //    // Set Transform to random Platform Chunk from List
    //    groundChunk = RandomChunkerizer(0, groundChunks.Count-1);
//
    //    // Spawn the Platform chunk and log to AI count
    //    Transform nextGroundChunk_Left = Instantiate(groundChunk, nextChunk, Quaternion.identity);
    //    Factory.groundChunkActivated += 1;
//
    //    // Return the transform for sister method
    //    return nextGroundChunk_Left;
    //}
    public Transform SpawnGroundChunk_Left(Transform groundChunk, Vector3 nextPosition, Transform groundParent)
    {
        // Spawn the Platform Chunk
        Transform nextGroundChunk_Left = Instantiate(groundChunk, nextPosition, Quaternion.identity, groundParent);
        groundChunkActivated += 1;

        // Return the transform for sister method
        return nextGroundChunk_Left;
    }
    #endregion

    #region Random "Chunkerizer"
    private Transform RandomChunkerizer(int floor, int ceiling)
    {
        // Variable to hold the index of random list element
        int randomChunk = 0;

        // Get random index for list
        int randomPick = UnityEngine.Random.Range(floor, ceiling-1);

        // Call GameObject from list and get its transform
        groundObj = groundChunks[randomChunk];
        //groundObj.SetActive(true);
        groundChunk = groundObj.transform;

        // Return the randomly-chosen Platform Chunk
        return groundChunk;
    }
    #endregion
}

