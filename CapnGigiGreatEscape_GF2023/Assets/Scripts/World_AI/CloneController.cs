using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 deleteMarker;
    private static float distanceFromPlayer;
    private float deactivationDistance;
    private EnemyGenerator enemyGenerator;
    //private ProceduralAI ai;
    //public static List<GameObject> enemySpawnMarkerList;
    private static List<Vector3> enemySpawnPositions = new List<Vector3>();

    void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        player = GameObject.Find("CapnGigi");
        deactivationDistance = 35f;

        // If this has been deactivated
        if (gameObject.activeSelf == false)
        {
            // Reactivate it!
            gameObject.SetActive(true);

            try
            {
                // Grab list of spawn positions
                enemySpawnPositions = enemyGenerator.GenerateEnemySpawnMarkerPositions();

                // For each spawn position, spawn an enemy and remove the spawn position from the list of available positions
                foreach (Vector3 spawnPosition in enemySpawnPositions)
                {
                    enemyGenerator.SpawnEnemies();
                    //enemySpawnPositions.Remove(spawnPosition);
                }
                enemySpawnPositions.Clear();

                // Initialise Deactivation Sequence, Cap'n!
                StartCoroutine(DeactivateClone());
            }
            // Computer says no?
            catch (NullReferenceException nre)
            {
                // Make computer say more.
                Debug.Log(nre.Message);
            }     
            // Make sure this runs lol
            StartCoroutine(DeactivateClone());
        }
        else
        {
            try
            {
                enemySpawnPositions = enemyGenerator.GenerateEnemySpawnMarkerPositions();

                foreach (Vector3 spawnPosition in enemySpawnPositions)
                {                  
                    enemyGenerator.SpawnEnemies();    
                }

                enemySpawnPositions.Clear();
                StartCoroutine(DeactivateClone());

            }
            catch (NullReferenceException nre)
            {
                Debug.Log(nre.Message);
            }
            StartCoroutine(DeactivateClone());
        }
    }

    private void Update()
    {
        StartCoroutine(DeactivateClone());
    }

    private IEnumerator DeactivateClone()
    {
        // After X seconds
        yield return new WaitForSeconds(1f);

        // Finds the players position
        playerPosition = player.transform.position;

        // Finds the distance between the player and the Game Object
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, playerPosition);

        // Logs distance to the console
        Debug.Log("Distance from player: " + distanceFromPlayer);

        // If the distance from the player is greater than the distance requirement to be deactivated
        if (distanceFromPlayer > deactivationDistance) 
        {
            // Log deactivation to controller + console
            ProceduralAI.chunksDeactivated += 1;
            Debug.Log("Chunks Deactivated: " + ProceduralAI.chunksDeactivated);

            // Deactivate the Game Object
            gameObject.SetActive(false);
        }
    }
}
