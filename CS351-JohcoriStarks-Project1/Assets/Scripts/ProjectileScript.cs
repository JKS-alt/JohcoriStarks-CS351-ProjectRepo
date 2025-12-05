using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;
    public GameObject impactEffect;   // assign prefab in inspector

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
          //  enemy.TakeDamage(1);

            // spawn impact effect
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
        }

        Destroy(gameObject); // destroy projectile on hit
    }

}
