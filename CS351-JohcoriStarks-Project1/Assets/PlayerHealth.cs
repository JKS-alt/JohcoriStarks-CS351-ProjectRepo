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
    public int health = 100;
    public DisplayBar healthBar;

    private Rigidbody2D rb;
    public float knockbackForce = 5f;

    public GameObject playerDeathEffect;

    public static bool hitRecently = false;
    public float hitRecoveryTime = 0.2f;

    private Animator anim;
    private AudioSource playerAudio;

    public AudioClip playerHitSound;
    public AudioClip playerDeathSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (healthBar != null)
            healthBar.SetMaxValue(health);

        hitRecently = false;

        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    public void Knockback(Vector3 enemyPosition)
    {
        if (hitRecently)
            return;

        hitRecently = true;

        if (gameObject.activeInHierarchy)
            StartCoroutine(RecoverFromHit());

        Vector2 direction = transform.position - enemyPosition;
        direction.Normalize();
        direction.y = 0.8f;

        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        // 🔥 Play Hit animation using Trigger
        if (anim != null)
            anim.SetTrigger("Hit");
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(hitRecoveryTime);
        hitRecently = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthBar != null)
            healthBar.SetValue(health);

        if (playerAudio != null && playerHitSound != null)
            playerAudio.PlayOneShot(playerHitSound);

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        ScoreScript.gameOver = true;

        if (playerAudio != null && playerDeathSound != null)
            playerAudio.PlayOneShot(playerDeathSound);

        if (playerDeathEffect != null)
        {
            GameObject effect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(effect, ps.main.duration);
            else
                Destroy(effect, 0.59f);
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
