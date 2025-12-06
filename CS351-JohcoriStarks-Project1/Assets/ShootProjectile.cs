using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Transform firePoint;             // Where the bullet spawns
    public GameObject projectilePrefab;     // The projectile prefab you drag in the Inspector

    void Update()
    {
       
        // Listen for left click or Fire1 input
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("FIRE BUTTON PRESSED");
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("SHOOT FUNCTION CALLED");


        // Spawn the projectile at the FirePoint position/rotation
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
        // Debug to verify the bullet actually spawned and where
        if (bullet != null)
        {
            Debug.Log("Bullet spawned at: " + bullet.transform.position);
            bullet.transform.position = new Vector3(
            bullet.transform.position.x,
            bullet.transform.position.y,
             0     // force Z to zero
            );
        }
        else
        {
            Debug.LogError("PROJECTILE FAILED TO SPAWN!");
        }

        // Destroy bullet after 3 seconds (prevents memory buildup)
        Destroy(bullet, 3f);
    }
}