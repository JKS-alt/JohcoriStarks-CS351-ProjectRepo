using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject deathEffect;
    public DisplayBar HealthBar;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetValue(currentHealth);
        // Initialize health bar with RAW health values (as in slides)
        if (HealthBar != null)
        {
            HealthBar.SetMaxValue(maxHealth);
            HealthBar.SetValue(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar.SetMaxValue(currentHealth);
        Debug.Log("Enemy took damage: " + currentHealth + "/" + maxHealth);

        if (HealthBar != null)
        {
            HealthBar.SetValue(currentHealth);   // raw value, slide-correct
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.1f);
        }

        if (HealthBar != null)
            Destroy(HealthBar.gameObject);

        Destroy(gameObject);
    }
}
