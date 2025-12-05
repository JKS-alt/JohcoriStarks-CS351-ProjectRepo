using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Transform firePoint;      // assign FirePoint empty object
    public GameObject projectile;    // assign projectile prefab

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // destroy after 3 seconds to avoid buildup
        Destroy(bullet, 3f);
    }
}



