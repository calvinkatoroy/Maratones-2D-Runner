using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // --- Jump Variables ---
    public Rigidbody2D rb;
    public bool isJumping;
    public bool canDoubleJump;
    public bool spacePressed;
    public float jumpForce = 8f;
    public float maxHoldJumpTime = 0.2f;
    public float holdJumpTimer = 0;
    public bool isHoldingJump = false;
    public bool isGrounded;
    public bool isFalling;
    public float speed = 5f;

    // --- Crouch Variables ---
    public float crouchHeight = 0.6f;
    private Vector2 normalHeight;
    private float yInput;

    public GameObject KentutPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Record the initial scale so we can return to it after crouching
        normalHeight = transform.localScale;
    }

    void Update()
    {
        // -----------------------
        // Ground / Jump Handling
        // -----------------------
        if (rb.velocity.y == 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = false;
            canDoubleJump = false;
            isGrounded = true;
        }

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
                Instantiate(KentutPrefab, transform.position, Quaternion.identity);
            }
        }

        // Stop "holding" the jump if space key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        // Handle hold jump timer if player is in the air
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
        }

        // Move horizontally if grounded
        if (isGrounded)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        // Check if player is falling
        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        // ---------------
        // Crouch Handling
        // ---------------
        yInput = Input.GetAxisRaw("Vertical");

        // If pressing down while on the ground, crouch
        if (yInput < 0 && isGrounded)
        {
            transform.localScale = new Vector2(normalHeight.x, crouchHeight);
        }
        else
        {
            // Return to normal scale if not crouching
            if (transform.localScale.y != normalHeight.y)
            {
                transform.localScale = normalHeight;
            }
        }

        //make a list 1-3 for the powerups
        //if the player collides with the powerup, add the powerup to the list
        //if the player presses 1, activate the powerup
        //if the player presses 2, activate the powerup
        //if the player presses 3, activate the powerup
        
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        isGrounded = false;
    }
}
