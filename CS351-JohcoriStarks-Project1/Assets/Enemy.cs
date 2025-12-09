using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject deathEffect;
    public DisplayBar HealthBar;
    public int damage = 10;

    public AudioClip enemyHitSound;
    public AudioClip enemyDeathSound;
    private AudioSource enemyAudio;

    void Start()
    {
        currentHealth = maxHealth;

        if (HealthBar != null)
        {
            HealthBar.SetMaxValue(1f);
            HealthBar.SetValue(1f);
        }

        enemyAudio = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float percent = (float)currentHealth / maxHealth;
        HealthBar.SetValue(percent);

        // HIT SOUND
        if (enemyAudio != null && enemyHitSound != null)
            enemyAudio.PlayOneShot(enemyHitSound);

        if (currentHealth <= 0)
            Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth == null)
            return;

        playerHealth.TakeDamage(damage);
        playerHealth.Knockback(transform.position);
    }

    void Die()
    {
        // PLAY DEATH SOUND
        if (enemyAudio != null && enemyDeathSound != null)
            enemyAudio.PlayOneShot(enemyDeathSound);

        // DEATH EFFECT
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.33f);
        }

        if (HealthBar != null)
            Destroy(HealthBar.gameObject);

        // 
        Destroy(gameObject, 0.15f);
    }
}
