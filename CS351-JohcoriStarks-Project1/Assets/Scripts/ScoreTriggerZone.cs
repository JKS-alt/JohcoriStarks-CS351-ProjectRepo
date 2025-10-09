using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool active = true;

    public AudioClip ScoreSound;
    private AudioSource playerAudio;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (active && collision.gameObject.CompareTag("Player"))
            {
                active = false;
                ScoreScript.score++;

                // Play sound from this object's AudioSource
                if (playerAudio != null && ScoreSound != null)
                {
                    playerAudio.PlayOneShot(ScoreSound, 1.0f);
                }

                // Hide visual
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }

                // Destroy this GameObject after sound finishes
                Destroy(gameObject, ScoreSound.length);
            }

        

    }
}

