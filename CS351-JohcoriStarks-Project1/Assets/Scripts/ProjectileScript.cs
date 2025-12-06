using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Rigidbody component of the projectile
    private Rigidbody2D rb;

    // Speed of the projectile
    public float speed = 20f;

    // Damage dealt to the enemy
    public int damage = 1;

    // Impact effect prefab
    public GameObject impact;

    void Start()
    {
        // Get the rigidbody component
        rb = GetComponent<Rigidbody2D>();

        // Set the velocity of the projectile
        rb.velocity = transform.right * speed;
    }

    // Function called when the projectile collides with another object
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Projectile HIT something: " + hitInfo.name);

        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            Debug.Log("Hit an ENEMY. Applying damage: " + damage);
            enemy.TakeDamage(damage);

            if (impact != null)
            {
                Debug.Log("Spawning impact effect at: " + hitInfo.transform.position);

                GameObject effect = Instantiate(impact, transform.position, Quaternion.identity);

                if (effect != null)
                {
                    Debug.Log("Impact effect spawned successfully: " + effect.name);

                    // Ensure visible
                    effect.transform.position = new Vector3(effect.transform.position.x, effect.transform.position.y, 0);

                    if (effect.GetComponent<SpriteRenderer>() != null)
                    {
                        Debug.Log("Impact HAS SpriteRenderer.");
                        effect.GetComponent<SpriteRenderer>().sortingOrder = 999;
                    }
                    else
                    {
                        Debug.LogWarning("Impact prefab is MISSING SpriteRenderer!");
                    }

                    Destroy(effect, 0.15f);
                }
                else
                {
                    Debug.LogError("Impact effect FAILED to instantiate!");
                }
            }
            else
            {
                Debug.LogError("Impact prefab is NULL. Assign it in Inspector!");
            }
        }
        else
        {
            Debug.Log("Hit NON-ENEMY object: " + hitInfo.name);
        }

        if (!hitInfo.CompareTag("Player"))
        {
            Debug.Log("Projectile DESTROYED.");
            Destroy(gameObject);
        }
    }

    }

