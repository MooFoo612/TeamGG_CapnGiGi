using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CinemachineConfiner : MonoBehaviour
{
    WorldGenerator controller;
    [SerializeField] float camSpeed = 30f;

    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {
        StartCoroutine(ReverseTimer());
    }
    protected void MoveCamera_Right()
    {
        // Move the object right 1 unit/second.
        transform.Translate(camSpeed, 0,0);
    }

    protected void MoveCamera_Left()
    {
        // Move the object left 1 unit/second.
        transform.Translate(camSpeed, 0, 0);
    }

    protected IEnumerator ReverseTimer()
    {
        yield return new WaitForSeconds(30f);

        if (controller.reversedWorld == true)
        {
            controller.reversedWorld = false;

            MoveCamera_Left();
        }
        else
        {
            controller.reversedWorld = true;

            MoveCamera_Right();
        }
    }
}
