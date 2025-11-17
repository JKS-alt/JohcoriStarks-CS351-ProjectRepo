using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Variable to store the health of the player
    public int health = 100;

    // A reference to the health bar
    // This must be set in the inspector
    public DisplayBar healthBar;

    private Rigidbody2D rb;

    // Knockback force when the player collides with an enemy
    public float knockbackForce = 5f;

    // A prefab to spawn when the player dies
    // This must be set in the inspector
    public GameObject playerDeathEffect;

    // Bool to keep track if the player has been hit recently
    public static bool hitRecently = false;

    // Time in seconds to recover from a hit
    public float hitRecoveryTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        // Set the RigidBody2D reference
        rb = GetComponent<Rigidbody2D>();

        // Check if the RigidBody2D reference is null
        if (rb == null)
        {
            // Log an error message if the rb is null
            Debug.LogError("Rigidbody2D not found on player");
        }

        // Set the healthBar max value to the player's max health
        //healthBar.SetMaxHealth(maxHealth);

        hitRecently = false;

    }
    // A function to knock the player back when they collide with an enemy
    void Knockback(Vector2 enemyPosition)
    {
        // Check if the player was hit recently
        if (hitRecently)
        {
            return;
        }

        // Get the direction of the knockback
        Vector2 knockbackDirection = (rb.position - enemyPosition).normalized;

        // Apply the knockback force
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Reduce the player's health
      // currentHealth -= damageAmount;

        // Update the health bar
        //healthBar.SetHealth(currentHealth);

        // Set hitRecently to true to prevent multiple hits in quick succession
        hitRecently = true;

        // Start the coroutine to reset hitRecently after a delay
        StartCoroutine(ResetHitRecently());
    }
    // Coroutine to reset hitRecently after hitRecoveryTime seconds
    IEnumerator ResetHitRecently()
    {
        // Wait for hitRecoveryTime seconds
        yield return new WaitForSeconds(hitRecoveryTime);

        // Reset hitRecently to false
        hitRecently = false;
    }
    // A function to take damage
    public void TakeDamage(int damage)
    {
        // Subtract the damage from the health
        health -= damage;

        // Update the health bar
        healthBar.SetValue(health);

        // TODO: Play a sound effect when the player takes damage
        // TODO: Play an animation when the player takes damage

        // If the health is less than or equal to 0
        if (health <= 0)
        {
            // Call the Die method
           Die();
        }
    }
    // A function to die
    public void Die()
    {
        // Set gameOver to true
       // ScoreManager.gameOver = true;

        // TODO: Play a sound effect when the player dies
        // TODO: Add the player death effect when the player dies
         GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        // Destroy the death effect after 2 seconds
        Destroy(deathEffect, 2f);

        // Disable the player object
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
