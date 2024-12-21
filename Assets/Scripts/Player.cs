using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = -200;
    public Vector2 velocity;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 40;
    public float maxVelocity = 100;
    public float groundHeight = 6;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.2f;
    public float holdJumpTimer = 0;

    public bool canDoubleJump = true; // Allow double jump

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to Rigidbody2D and BoxCollider2D
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) // First Jump
            {
                Jump();
                isGrounded = false;
                canDoubleJump = true; // Reset double jump ability
            }
            else if (canDoubleJump) // Double Jump
            {
                Jump();
                canDoubleJump = false; // Consume double jump
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
        Vector2 pos = transform.position;

        // Apply gravity manually if not grounded
        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            // Apply velocity changes
            pos.y += velocity.y * Time.fixedDeltaTime;

            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            // Perform a raycast to check for collisions
            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y); // Correct offset calculation
            Vector2 rayDirection = Vector2.down;
            float rayDistance = Mathf.Abs(velocity.y * Time.fixedDeltaTime); // Correct ray distance calculation
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (hit2D.collider != null)
            {
                // Player is grounded
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    pos.y = groundHeight;
                    isGrounded = true;
                }
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
            }
            // Check if the player has landed on the ground
            //if (pos.y <= groundHeight)
            //{
            //    pos.y = groundHeight;
            //    isGrounded = true;
            //    holdJumpTimer = 0;  // Reset jump timer when grounded
            //    velocity.y = 0;     // Reset vertical velocity
            //}
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if(isGrounded)
        {
            float velocityRatio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }

        // Apply the updated position to the Rigidbody2D
        rb.MovePosition(pos);
    }

    // Jump Function
    void Jump()
    {
        velocity.y = jumpVelocity; // Apply jump velocity
        isHoldingJump = true;      // Allow holding the jump key
        holdJumpTimer = 0;         // Reset hold timer
    }
}
