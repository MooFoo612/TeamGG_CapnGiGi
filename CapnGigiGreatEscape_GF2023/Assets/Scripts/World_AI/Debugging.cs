using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    // For Debugging any generated Object Array | Length + Names
    public void DebugGeneratedObjectArray(Object[] prefabArray)
    {
        // Display the Length of the Array
        Debug.Log("Array Size: " + prefabArray.Length);

        // Print the name of each object in the Array
        foreach (GameObject value in prefabArray)
        {
            Debug.Log("Prefab in Array: " + value.name);
        }
    }
    // For Debugging any generated List | Length + Names
    public void DebugGeneratedList(List<GameObject> prefabList)
    {
        // Display the Length of the List
        Debug.Log("List Size: " + prefabList.Count);

        // Print the name of each object in the List
        foreach (GameObject value in prefabList)
        {
            Debug.Log("Prefab in List: " + value.name);
        }
    }

    public void DebugChunkDestruction(int chunksDestroyed)
    {
        if (chunksDestroyed > 0 && chunksDestroyed % 2 != 0)
        {
            Debug.Log("Inconsistent Chunk Destruction!");
        }
    }

}
