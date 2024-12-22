using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Variables")]
    public Rigidbody2D rb;
    public bool isJumping;
    public bool canDoubleJump;
    public bool spacePressed;
    [SerializeField] public float jumpForce = 8;
    public float maxHoldJumpTime = 0.2f;
    public float holdJumpTimer = 0;
    public bool isHoldingJump = false;
    public bool isGrounded;
    public bool isFalling;

    [Header("Crouch Variables")]
    public float crouchHeight = 0.6f;
    private Vector2 normalHeight;
    private float yInput;

    [Header("Double Jump Variables")]
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
        if (rb.velocity.y == 0 && !Input.GetKeyDown(KeyCode.Space) || yInput < 0)
        {
            isJumping = false;
            canDoubleJump = false;
            isGrounded = true;
        }

        if (yInput > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) // First Jump
            {
                Jump();
                isGrounded = false;
                canDoubleJump = true; // Reset double jump ability
            }
            else if (canDoubleJump || yInput > 0) // Double Jump
            {
                Jump();
                canDoubleJump = false; // Consume double jump
                Instantiate(KentutPrefab, transform.position, Quaternion.identity);
            }
        }

        // Stop "holding" the jump if space key is released
        if (yInput > 0 || Input.GetKeyUp(KeyCode.Space))
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
            rb.velocity = new Vector2(5, rb.velocity.y);
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
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || yInput < 0) && isGrounded)
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
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        isGrounded = false;
    }
}
