using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Enemy : MonoBehaviour
{//enemy health
    public int health;

    //prefab to spawn when emeby die
    public GameObject DeathAffect;

    private DisplayBar HealthBar;

    public int damage = 10;
    public void TakeDamage(int damage)
    {
        health -= damage;
      
        if (health <= 0)
        {
            Die();
        }
    }
       
    void Die()
    {
       // Instantiate(DeathAffect, transform.position, Quaternion, identity);
            
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //fix what bleow FIXME
       // healthBar = GetComponentInChildren<>(HealthBar);
        if (HealthBar = null)
        {
            Debug.LogError("Healthabr not found");
                 return;
        }
    }
    // Damage the player when the enemy collides with them
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player health script from the player object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Check if the player health script is null
            if (playerHealth == null)
            {
                // Log an error if the player health script is null
                Debug.LogError("PlayerHealth script not found on Player");
                return;
            }

            // Damage the player
            playerHealth.TakeDamage(damage);

            //Knockback the player
           // playerHealth.Knockback(transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
