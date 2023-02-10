using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public void FireProjectile(){
        // Spawn the projectile(throwing sword in our case)
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        // Flip the vector direction (using localScale) based on the direction the character is facing at time of launch
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);
    }
}
