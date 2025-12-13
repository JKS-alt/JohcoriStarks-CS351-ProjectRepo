using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMoveWalkingChase : MonoBehaviour
{
    public float chaseRange = 4f;
    public float enemyMovementSpeed = 1.5f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        Debug.Log("CRAB DEBUG: Start() called");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            playerTransform = p.transform;

        Debug.Log("CRAB DEBUG: playerTransform = " + playerTransform);
    }

    void Update()
    {
        if (ScoreScript.gameOver)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        if (playerTransform == null)
            return;

        Vector2 playerDirection = playerTransform.position - transform.position;
        float distanceToPlayer = playerDirection.magnitude;

        if (distanceToPlayer <= chaseRange)
        {
            Debug.Log("CRAB DEBUG: Player IN chase range");

            playerDirection.Normalize();
            playerDirection.y = 0;

            FacePlayer(playerDirection);

            // ⭐ NEW CLEAN LOGIC
            if (!IsGroundAhead())
            {
                Debug.Log("CRAB DEBUG: No ground ahead — stopping");
                StopMoving();
                return;  // <--- prevents crab from still moving this frame
            }

            // If ground exists, keep chasing
            MoveTowardsPlayer(playerDirection);
        }
        else
        {
            Debug.Log("CRAB DEBUG: Player NOT in range → stop moving");
            StopMoving();
        }
    }

    bool IsGroundAhead()
    {
        float groundCheckDistance = 1.2f;
        int groundLayer = LayerMask.GetMask("Ground");

        Vector2 forward = (sr.flipX == false) ? Vector2.left : Vector2.right;
        Vector2 rayDir = (forward + Vector2.down).normalized;

        Vector2 rayOrigin = (Vector2)transform.position + forward * 0.3f;

        Debug.DrawRay(rayOrigin, rayDir * groundCheckDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            rayDir,
            groundCheckDistance,
            groundLayer
        );

        if (hit.collider == null)
        {
            Debug.Log("CRAB DEBUG: No ground tile hit — ledge detected");
            return false;
        }

        Debug.Log("CRAB DEBUG: Ground tile detected: " + hit.collider.name);
        return true;
    }

    void FacePlayer(Vector2 playerDirection)
    {
        sr.flipX = playerDirection.x > 0;
    }

    void MoveTowardsPlayer(Vector2 playerDirection)
    {
        Debug.Log("CRAB DEBUG: MOVING. X velocity = " + (playerDirection.x * enemyMovementSpeed));
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);
        anim.SetBool("isMoving", true);
    }

    void StopMoving()
    {
        Debug.Log("CRAB DEBUG: STOPPING movement");
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.SetBool("isMoving", false);
    }
}
