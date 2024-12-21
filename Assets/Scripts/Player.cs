using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = -200f;
    public Vector2 velocity;
    public float maxAcceleration = 10f;
    public float acceleration = 10f;
    public float distance = 0f;
    public float jumpVelocity = 40f;
    public float maxVelocity = 100f;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.2f;
    public float holdJumpTimer = 0f;

    public bool canDoubleJump = true; // Allow double jump

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Find the child's "Body" GameObject and get the BoxCollider2D from it
        Transform bodyTransform = transform.Find("Body");
        if (bodyTransform != null)
        {
            boxCollider = bodyTransform.GetComponent<BoxCollider2D>();
        }
        else
        {
            Debug.LogWarning("Body child not found. Please ensure the child's name is 'Body' and it has a BoxCollider2D.");
        }
    }

    private void Update()
    {
        // Handle Jump Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) // First jump
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump) // Double jump
            {
                Jump();
                canDoubleJump = false;
            }
        }

        // Stop jump hold when key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;

        // If the collider isn't found, just return to avoid errors
        if (boxCollider == null)
        {
            rb.MovePosition(pos);
            return;
        }

        // Calculate the ray origin based on the collider's size and offset
        // pos is the player's parent transform position. We adjust by collider offset and half its height to get the bottom.
        float colliderBottom = pos.y + boxCollider.offset.y - (boxCollider.size.y / 2f);
        Vector2 rayOrigin = new Vector2(pos.x + boxCollider.offset.x, colliderBottom);

        float rayDistance = 0.2f; // Small value to detect ground just below feet
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance);

        if (hit2D.collider != null && hit2D.collider.GetComponent<Ground>() != null)
        {
            // Player is on ground
            isGrounded = true;
            // Align the player with the top of the ground collider
            pos.y = hit2D.collider.bounds.max.y - (boxCollider.offset.y - (boxCollider.size.y / 2f));
            
            // Apply horizontal acceleration while on ground
            float velocityRatio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x > maxVelocity)
            {
                velocity.x = maxVelocity;
            }

            // Reset vertical velocity if grounded
            velocity.y = 0f;
        }
        else
        {
            // Not grounded, apply gravity and vertical movement
            isGrounded = false;

            // Handle jump hold timer
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            // If no longer holding jump, apply gravity
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            // Move the player vertically
            pos.y += velocity.y * Time.fixedDeltaTime;
        }

        // Move the player horizontally
        distance += velocity.x * Time.fixedDeltaTime;
        pos.x += velocity.x * Time.fixedDeltaTime;

        // Apply the updated position to the Rigidbody2D
        rb.MovePosition(pos);

        // Debug ray
        Debug.DrawRay(rayOrigin, Vector2.down * rayDistance, Color.green);
    }

    private void Jump()
    {
        velocity.y = jumpVelocity; // Set jump velocity
        isHoldingJump = true;      // Enable hold jump
        holdJumpTimer = 0f;        // Reset hold timer
    }
}
