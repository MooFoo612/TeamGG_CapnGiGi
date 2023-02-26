using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    //private float distanceFromPlayer = 0f;
    private GameObject player;
    private Vector3 playerPosition;
    private EnemyGenerator enemyGenerator;
    private ProceduralAI ai;
    private List<GameObject> enemySpawnMarkerList;

    void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        ai = GetComponent<ProceduralAI>();
        player = GameObject.Find("CapnGigi");

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            try
            {
                enemySpawnMarkerList = ai.GenerateEnemySpawnMarkerList();
                foreach (GameObject marker in enemySpawnMarkerList)
                {
                    enemyGenerator.SpawnEnemies();

                }
                player = GameObject.Find("CapnGigi");
                StartCoroutine(DeactivateClone());
            }
            catch (NullReferenceException nre)
            {
                Debug.LogError(nre.Message);
            }

            
        }
        else
        {
            try
            {
                foreach (GameObject marker in enemySpawnMarkerList)
                {
                    enemyGenerator.SpawnEnemies();

                }

            }
            catch (NullReferenceException nre)
            {
                Debug.Log(nre.Message);
            }

            

            StartCoroutine(DeactivateClone());
        }


        //enemyGenerator.SpawnEnemies();
        //StartCoroutine(DeactivateClone());
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
