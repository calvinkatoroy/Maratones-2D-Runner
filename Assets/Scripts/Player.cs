using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = -200;
    public Vector2 velocity;
    public float jumpVelocity = 40;
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

            // Check if the player has landed on the ground
            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
                holdJumpTimer = 0;  // Reset jump timer when grounded
                velocity.y = 0;     // Reset vertical velocity
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
