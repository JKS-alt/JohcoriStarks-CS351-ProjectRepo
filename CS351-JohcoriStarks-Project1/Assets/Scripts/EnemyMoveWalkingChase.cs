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
        Debug.Log("CRAB DEBUG: rb = " + rb);

        anim = GetComponent<Animator>();
        Debug.Log("CRAB DEBUG: anim = " + anim);

        sr = GetComponent<SpriteRenderer>();
        Debug.Log("CRAB DEBUG: SpriteRenderer = " + sr);

        GameObject p = GameObject.FindWithTag("Player");
        Debug.Log("CRAB DEBUG: Player object found? " + (p != null));

        if (p != null)
            playerTransform = p.transform;

        Debug.Log("CRAB DEBUG: playerTransform = " + playerTransform);
    }

    void Update()
    {
        Debug.Log("CRAB DEBUG: Update() running");

        if (playerTransform == null)
        {
            Debug.LogError("CRAB ERROR: playerTransform is NULL. Did you tag your player?");
            return;
        }

        Vector2 playerDirection = playerTransform.position - transform.position;
        float distanceToPlayer = playerDirection.magnitude;

        Debug.Log("CRAB DEBUG: Distance to player = " + distanceToPlayer);

        if (distanceToPlayer <= chaseRange)
        {
            Debug.Log("CRAB DEBUG: Player IN chase range");

            playerDirection.Normalize();
            playerDirection.y = 0;

            FacePlayer(playerDirection);

            if (IsGroundAhead())
            {
                Debug.Log("CRAB DEBUG: Ground ahead → move toward player");
                MoveTowardsPlayer(playerDirection);
            }
            else
            {
                Debug.Log("CRAB DEBUG: NO ground ahead → stop");
                StopMoving();
            }
        }
        else
        {
            Debug.Log("CRAB DEBUG: Player NOT in range → stop moving");
            StopMoving();
        }
    }

    bool IsGroundAhead()
    {
        float groundCheckDistance = 2.0f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        Vector2 enemyFacingDirection = (sr.flipX == false) ? Vector2.left : Vector2.right;
        Vector2 rayDir = Vector2.down + enemyFacingDirection;

        Debug.Log("CRAB DEBUG: Raycast direction = " + rayDir);

        Debug.DrawRay(transform.position, rayDir * groundCheckDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, groundCheckDistance, groundLayer);

        if (hit.collider == null)
        {
            Debug.LogWarning("CRAB DEBUG: Raycast did NOT hit ground!");
            return false;
        }
        else
        {
            Debug.Log("CRAB DEBUG: Raycast HIT ground collider: " + hit.collider.name);
            return true;
        }
    }

    void FacePlayer(Vector2 playerDirection)
    {
        Debug.Log("CRAB DEBUG: Facing player. X = " + playerDirection.x);

        if (playerDirection.x < 0)
            sr.flipX = false;
        else
            sr.flipX = true;
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
