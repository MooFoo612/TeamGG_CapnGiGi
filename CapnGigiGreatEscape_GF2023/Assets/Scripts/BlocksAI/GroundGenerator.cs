using System;
using Unity.VisualScripting;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform groundSection;
    [SerializeField] private Transform groundStart;
    [SerializeField] private GameObject player;

    private const float DISTANCE_TO_SPAWN_SECTION = 25f;
    private const float DISTANCE_TO_DESTROY_SECTION = 25f;
    private Vector3 lastEndPosition;
    //private Vector3 delPosition;
    //private GameObject deleteMarker;
    //private Vector3 groundDelPosition;
    //private Transform groundToDelete;
    #endregion
    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");
        // Find the child EndPosition object in the GameStart parent
        lastEndPosition = groundStart.Find("EndPosition").position;

        //deleteMarker = GameObject.Find("DeletePosition");
        // Find the child DeletePosition object in GameStart parent
        //delPosition = groundStart.Find("DeletePosition").position;

        //groundToDelete = groundToDelete.Find("[DO_NOT_EDIT] PlatformSection_1(Clone)");
        //groundDelPosition = groundToDelete.Find("DeletePosition").position;

    }
    private void Update()
    {
        // If the player is close enough to the next reference spawn point
        if (Vector3.Distance(player.transform.position, lastEndPosition) < DISTANCE_TO_SPAWN_SECTION)
        {
            // Spawn another section
            SpawnSection();
            Debug.Log("Ground section generated");
        }
    }
    #region Spawner/Despawner
    private void SpawnSection()
    {
        //Get the transform to refrence the next End Position
        Transform lastSectionTransform = SpawnSection(lastEndPosition);
        lastEndPosition = lastSectionTransform.Find("EndPosition").position;
    }
    private Transform SpawnSection(Vector3 firstSection)
    {
        Transform lastSectionTransform = Instantiate(groundSection, firstSection, Quaternion.identity);
        return lastSectionTransform;
    }
    
    private void KillSection()
    {
        // Destroy section
    }
    private Vector3 SectionToKill(Vector3 lastDelPosition)
    {
        // Find section to destroy
        return new Vector3(0,0,0);
    }
    #endregion
}

