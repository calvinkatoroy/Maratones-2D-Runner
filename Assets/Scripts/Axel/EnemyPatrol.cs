using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject PointA;
    public GameObject PointB;

    private Vector3 pointAInitialPos; // Store the initial position of Point A (world position)
    private Vector3 pointBInitialPos; // Store the initial position of Point B (world position)

    private Vector3 currentTargetPosition; // The current target position (either pointA or pointB)
    private Animator animator;
    private Transform player;
    private CapsuleCollider2D enemyCollider;

    public int speed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider2D>();

        //Biar musuhnya gak stop pergerakan player, tapi harus ngurangin timer. (BLM WORK)
        Physics2D.IgnoreCollision(enemyCollider, player.GetComponent<BoxCollider2D>(), true);
        // Store the world positions of Point A and Point B
        pointAInitialPos = PointA.transform.position;
        pointBInitialPos = PointB.transform.position;

        // Start by moving towards Point B
        currentTargetPosition = pointBInitialPos;

        animator.SetInteger("WalkSpeed", speed);
        flip();
    }

    void Update()
    {
        // Calculate the direction towards the target position
        Vector2 direction = currentTargetPosition - transform.position;

        // Set velocity based on direction (move right or left)
        if (currentTargetPosition == pointBInitialPos)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);  // Move right towards PointB
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);  // Move left towards PointA
        }

        // When the enemy is close enough to the current target position, flip direction and set next target
        if (Vector2.Distance(transform.position, currentTargetPosition) < 0.5f)
        {
            flip();
            
            // Switch to the next target point
            if (currentTargetPosition == pointBInitialPos)
            {
                currentTargetPosition = pointAInitialPos; // Set to initial position of Point A
            }
            else
            {
                currentTargetPosition = pointBInitialPos; // Set to initial position of Point B
            }
        }
    }

    private void flip()
    {
        // Flip the sprite's X scale to change direction
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }

    private void OnDrawGizmos()
    {
        // Draw Gizmos for visual reference in the editor
        Gizmos.DrawWireSphere(pointAInitialPos, 0.5f); 
        Gizmos.DrawWireSphere(pointBInitialPos, 0.5f); 
        Gizmos.DrawLine(pointAInitialPos, pointBInitialPos); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is hit by enemy");
        }
    }
}
