using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables
    private const float DISTANCE_TO_SPAWN_SECTION = 25f;
    //private const float DISTANCE_TO_DESTROY_SECTION = 50f;
    [SerializeField] private Transform lvlSection;
    [SerializeField] private Transform lvlStart;
    [SerializeField] private GameObject player;
    private Vector3 lastEndPosition;
    #endregion
    void Awake()
    {
        // Access the player
        player = GameObject.Find("CapnGigi");
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
    private void SpawnSection()
    {
        //Get the transform to refrence the next End Position
        Transform lastSectionTransform = SpawnSection(lastEndPosition);
        lastEndPosition = lastSectionTransform.Find("EndPosition").position;
    }
    private Transform SpawnSection(Vector3 newSection)
    {
        Transform lastSectionTransform = Instantiate(lvlSection, newSection, Quaternion.identity);
        return lastSectionTransform;
    }
    #endregion
}






