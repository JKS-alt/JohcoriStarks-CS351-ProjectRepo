/*
 * Johcori Starks
 * CS 351 – Lesson 9
 * Handles player health, knockback, hurt animation, and death.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Player health
    public int health = 100;

    // UI Health Bar reference (set in Inspector)
    public DisplayBar healthBar;

    // Rigidbody2D reference
    private Rigidbody2D rb;

    // Knockback force
    public float knockbackForce = 5f;

    // Prefab to spawn when the player dies
    public GameObject playerDeathEffect;

    // Prevent multiple hits in quick succession
    public static bool hitRecently = false;

    // Time before knockback/hit resets
    public float hitRecoveryTime = 0.2f;

    // Animator for hit animation
    private Animator anim;

    // Audio for hit sound
    private AudioSource playerAudio;
    public AudioClip playerHitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
            Debug.LogError("Rigidbody2D not found on player");

        // Setup health bar
        if (healthBar != null)
            healthBar.SetMaxValue(health);

        hitRecently = false;

        // Animator reference
        anim = GetComponent<Animator>();

        // Audio reference
        playerAudio = GetComponent<AudioSource>();
    }

    // --------------------------------------------
    // KNOCKBACK (Must be PUBLIC or Enemy cannot call it)
    // --------------------------------------------
    public void Knockback(Vector3 enemyPosition)
    {
        if (hitRecently)
            return;

        hitRecently = true;

        // Start recovery IF object is still active
        if (gameObject.activeInHierarchy)
            StartCoroutine(RecoverFromHit());

        // Calculate knockback direction
        Vector2 direction = transform.position - enemyPosition;
        direction.Normalize();
        direction.y = 0.8f; // upward force

        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        // Hurt animation
        if (anim != null)
            anim.SetBool("hit", true);
    }

    // --------------------------------------------
    // RESET HIT FLAG
    // --------------------------------------------
    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(hitRecoveryTime);

        hitRecently = false;

        if (anim != null)
            anim.SetBool("hit", false);
    }

    // --------------------------------------------
    // TAKE DAMAGE
    // --------------------------------------------
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthBar != null)
            healthBar.SetValue(health);

        // Play hurt sound
        if (playerAudio != null && playerHitSound != null)
            playerAudio.PlayOneShot(playerHitSound);

        if (health <= 0)
            Die();
    }

    // --------------------------------------------
    // DIE
    // --------------------------------------------
    public void Die()
    {
        // Notify game-over manager
        ScoreScript.gameOver = true; // dont chnage because score manager is named ScoreScript

        // Spawn death effect
        if (playerDeathEffect != null)
            Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        // Disable player
        gameObject.SetActive(false);
    }
}
