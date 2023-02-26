using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 deleteMarker;
    private float distanceFromPlayer;
    private float deactivationDistance;
    private EnemyGenerator enemyGenerator;
    private ProceduralAI ai;
    private List<GameObject> enemySpawnMarkerList;
    private static List<Vector3> enemySpawnPositions = new List<Vector3>();

    void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        ai = GetComponent<ProceduralAI>();
        player = GameObject.Find("CapnGigi");

        // If this has been deactivated
        if (gameObject.activeSelf == false)
        {
            // Reactivate it!
            gameObject.SetActive(true);

            try
            {
                // Grab list of spawn positions
                enemySpawnPositions = enemyGenerator.GenerateEnemySpawnMarkerPositions();

                // For each spawn position, spawn an enemy
                foreach (Vector3 spawnPosition in enemySpawnPositions)
                {
                    enemyGenerator.SpawnEnemies();

                }
                // Initialise Deactivation Sequence, Cap'n!
                StartCoroutine(DeactivateClone());
            }
            // Computer says no?
            catch (NullReferenceException nre)
            {
                // Make computer say more.
                Debug.LogError(nre.Message);
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
                StartCoroutine(DeactivateClone());

            }
            catch (NullReferenceException nre)
            {
                Debug.Log(nre.Message);
            }
            StartCoroutine(DeactivateClone());
        }
    }

    private IEnumerator DeactivateClone()
    {
        yield return new WaitForSeconds(45f);
        //playerPosition = player.transform.position;
        //distanceFromPlayer = Vector3.Distance(gameObject.transform.position, playerPosition);
        //Debug.Log("Distance " + distanceFromPlayer);

        ProceduralAI.chunksDeactivated += 1;
        Debug.Log("Chunks Deactivated: " + ProceduralAI.chunksDeactivated);
        gameObject.SetActive(false);
          
    }
}
