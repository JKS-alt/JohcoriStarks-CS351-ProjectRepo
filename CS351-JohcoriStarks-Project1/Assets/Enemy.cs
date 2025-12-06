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

        if (HealthBar != null)
        {
            HealthBar.SetMaxValue(1f);
            HealthBar.SetValue(1f);
        }
    }

    public void TakeDamage(int damage)
    {
         currentHealth -= damage;
        Debug.Log("PERCENT = " + ((float)currentHealth / maxHealth));

        if (HealthBar != null)
        {
            float percent = (float)currentHealth / maxHealth;
            HealthBar.SetValue(percent);
        }

        if (currentHealth <= 0)
            Die();
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
