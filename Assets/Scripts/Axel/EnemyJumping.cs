using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public GameObject pointA; // The target point (Point A)
    public float jumpForce = 5f; // The force with which the enemy jumps
    public float moveSpeed = 3f; // Horizontal speed to move towards Point A
    public float detectionRange = 3f; // Range within which the enemy will jump towards the player

    private Rigidbody2D rb;
    private Vector3 targetPosition; // Target position for the enemy on the X axis
    private bool isJumping = false; // Whether the enemy is jumping or not
    private bool hasJumped = false; // To check if the jump has already started
    private Transform player; // Reference to the player
    private CapsuleCollider2D enemyCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        targetPosition = new Vector3(pointA.transform.position.x, transform.position.y, transform.position.z);

        // Find the player object
        player = GameObject.Find("Player").transform;

        Physics2D.IgnoreCollision(enemyCollider, player.GetComponent<BoxCollider2D>(), true);
    }

    void Update()
    {
        // Check if the player is within detection range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange && !hasJumped) // If within detection range and hasn't jumped yet
        {
            JumpToTarget();
        }

        // Only move if the enemy hasn't jumped yet
        if (!hasJumped)
        {
            // Move towards the X position of Point A
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        // Move horizontally towards the target position
        float step = moveSpeed * Time.deltaTime; // Calculate the movement step based on moveSpeed
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    void JumpToTarget()
    {
        // Start the jump by applying a force upwards
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force vertically

        // Set the flag to prevent further horizontal movement
        hasJumped = true;

        // Optionally, set the X velocity to ensure it keeps moving towards the target without flipping
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    // You can use this method to check for landing and reset behavior (if needed)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            // Reset the jumping flag when landing on the ground
            isJumping = false;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is hit by enemy");
        }
    }
}
