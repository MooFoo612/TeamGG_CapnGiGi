using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform lvlSection1;
    [SerializeField] private Transform lvlSection2;

    #endregion

    void Awake() 
    {
        //Vector3 newSection = lvlSection.Find("EndPosition").position;
        SpawnSection1(new Vector3(18,3));
        SpawnSection2(new Vector3(18,3) + new Vector3(24,3));
        SpawnSection1(new Vector3(18,3) + new Vector3(48,3));
    }

    #region Spawner
    private void SpawnSection1(Vector3 newSection)
    {
        Instantiate(lvlSection1, newSection, Quaternion.identity);
    }

    private void SpawnSection2(Vector3 newSection)
    {
        Instantiate(lvlSection2, newSection, Quaternion.identity);
    }
    #endregion
}
