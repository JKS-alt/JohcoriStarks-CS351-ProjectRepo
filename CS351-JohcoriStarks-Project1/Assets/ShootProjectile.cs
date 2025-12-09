using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;

    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("FIRE BUTTON PRESSED");
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("SHOOT FUNCTION CALLED");

        // Play sound
        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);

        // Spawn the projectile
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        if (bullet != null)
        {
            Debug.Log("Bullet spawned at: " + bullet.transform.position);
            bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, 0);
        }
        else
        {
            Debug.LogError("PROJECTILE FAILED TO SPAWN!");
        }

        Destroy(bullet, 3f);
    }
}
