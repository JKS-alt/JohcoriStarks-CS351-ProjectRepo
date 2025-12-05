using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrolChase : MonoBehaviour
{
    // -------------------------------
    // Public references for patrol points
    // -------------------------------
    public GameObject[] patrolPoints;

    // -------------------------------
    // Movement values
    // -------------------------------
    public float speed = 2f;
    public float chaseRange = 3f;

    // -------------------------------
    // Enemy state types
    // -------------------------------
    public enum EnemyState { Patrolling, Chasing }
    public EnemyState currentState = EnemyState.Patrolling;

    // -------------------------------
    // Private internal references
    // -------------------------------
    private GameObject target;
    private GameObject player;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Current patrol index
    private int currentPatrolPointIndex = 0;

    // -------------------------------
    // START
    // -------------------------------
    void Start()
    {
        // Find player in scene
        player = GameObject.FindWithTag("Player");

        // Get Rigidbody2D (Flying uses velocity)
        rb = GetComponent<Rigidbody2D>();

        // Get SpriteRenderer for flipping
        sr = GetComponent<SpriteRenderer>();

        // Validate patrol points
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
        }

        // Set initial target point
        target = patrolPoints[currentPatrolPointIndex];
    }

    // -------------------------------
    // UPDATE
    // -------------------------------
    void Update()
    {
        // Update enemy state (patrol <-> chase)
        UpdateState();

        // Move based on state
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                break;
        }

        // Draw debug line to target
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    // -------------------------------
    // STATE MACHINE
    // -------------------------------
    void UpdateState()
    {
        if (IsPlayerInChaseRange() && currentState == EnemyState.Patrolling)
        {
            currentState = EnemyState.Chasing;
        }
        else if (!IsPlayerInChaseRange() && currentState == EnemyState.Chasing)
        {
            currentState = EnemyState.Patrolling;

            
            target = patrolPoints[currentPatrolPointIndex];
        }
    }

    bool IsPlayerInChaseRange()
    {
        if (player == null)
        {
            Debug.LogError("Player not found");
            return false;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }

    // -------------------------------
    // PATROL LOGIC
    // -------------------------------
    void Patrol()
    {
        // Check if reached current patrol point
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            // Go to next patrol point
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
            target = patrolPoints[currentPatrolPointIndex];
        }

        MoveTowardsTarget();
    }

    // -------------------------------
    // CHASE LOGIC
    // -------------------------------
    void ChasePlayer()
    {
        target = player;   // set target to player
        MoveTowardsTarget();
    }

    // -------------------------------
    // MOVE TOWARD TARGET
    // -------------------------------
    void MoveTowardsTarget()
    {
        // Direction vector
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        // Move enemy
        rb.velocity = direction * speed;

        // Flip sprite based on direction
        FaceForward(direction);
    }

    // -------------------------------
    // FACE ENEMY IN MOVEMENT DIRECTION
    // -------------------------------
    private void FaceForward(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sr.flipX = false;
        }
        else if (direction.x > 0)
        {
            sr.flipX = true;
        }
    }

    // -------------------------------
    // DRAW PATROL POINT GIZMOS
    // -------------------------------
    private void OnDrawGizmos()
    {
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;

            foreach (GameObject point in patrolPoints)
            {
                if (point != null)
                    Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}