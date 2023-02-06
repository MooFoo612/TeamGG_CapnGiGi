using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables
    private const float DISTANCE_TO_SPAWN_SECTION = 25f;
    //private const float DISTANCE_TO_DESTROY_SECTION = 50f;

    #endregion

    void Awake() 
    {
        // Access the player
        player = GameObject.Find("CapnGigi");
        //clones = GameObject.Find("LvlBlock_2(Clone)").transform.position;
        //lastClone = GameObject.Find("LvlBlock_2(Clone)");

        // Find the child EndPosition object in the GameStart parent
        lastEndPosition = lvlStart.Find("EndPosition").position;
    }

    private void Update()
    {
        // If the player is close enough to the next reference spawn point
        if (Vector3.Distance(player.transform.position, lastEndPosition) < DISTANCE_TO_SPAWN_SECTION)
        {
            // Spawn another section
            SpawnSection();
        } 

    }

    #region Spawner
    private void SpawnSection(Vector3 newSection)
    {
        Instantiate(lvlSection, newSection, Quaternion.identity);
    }

    #endregion
}
