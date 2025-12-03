using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Range at which the enemy will chase the player
    public float chaseRange = 4f;

    // Speed of the enemy movement
    public float enemyMovementSpeed = 1.5f;

    // Reference to the player's transform
    private Transform playerTransform;

    // Rigidbody reference
    private Rigidbody2D rb;

    // Animator reference
    private Animator anim;

    void Start()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Get Animator
        anim = GetComponent<Animator>();

        // Get the player's transform using the "Player" tag
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Get direction from enemy to player
        Vector2 playerDirection = playerTransform.position - transform.position;

        // Distance between enemy and player
        float distanceToPlayer = playerDirection.magnitude;

        // Check if player is within chase range
        if (distanceToPlayer <= chaseRange)
        {
            // Only move on the X-axis
            playerDirection.Normalize();
            playerDirection.y = 0f;

            // Face the player
            FacePlayer(playerDirection);

            // If there is ground ahead → move
            if (IsGroundAhead())
            {
                MoveTowardsPlayer(playerDirection);
            }
            else
            {
                StopMoving();
            }
        }
        else
        {
            // Idle if player is out of range
            StopMoving();
        }
    }

    // -----------------------------------------
    // CHECK IF THERE IS GROUND AHEAD
    // -----------------------------------------
    bool IsGroundAhead()
    {
        // Distance to check for ground
        float groundCheckDistance = 2.0f;

        // Layer mask for ground
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // Determine facing direction based on rotation
        Vector2 enemyFacingDirection = transform.rotation == Quaternion.Euler(0, 0, 0)
            ? Vector2.left
            : Vector2.right;

        // Raycast from the enemy's position, outward
        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyFacingDirection, groundCheckDistance, groundLayer);

        // Return true if ground is detected
        return hit.collider != null;
    }

    // -----------------------------------------
    // FACE TOWARD PLAYER
    // -----------------------------------------
    private void FacePlayer(Vector2 playerDirection)
    {
        if (playerDirection.x < 0)
        {
            // Face left
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Face right
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // -----------------------------------------
    // MOVE TOWARD PLAYER
    // -----------------------------------------
    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        // Move enemy on X-axis
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);

        // Play walk animation
        anim.SetBool("isMoving", true);
    }

    // -----------------------------------------
    // STOP MOVING + IDLE ANIMATION
    // -----------------------------------------
    private void StopMoving()
    {
        // Stop X movement
        rb.velocity = new Vector2(0, rb.velocity.y);

        // Switch animation to idle
        anim.SetBool("isMoving", false);
    }
}
