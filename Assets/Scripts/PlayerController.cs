using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isJumping;
    public bool canDoubleJump;
    public bool spacePressed;
    [SerializeField] public float jumpForce = 8;
    public float maxHoldJumpTime = 0.2f;
    public float holdJumpTimer = 0;
    public bool isHoldingJump = false;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(rb.velocity.y == 0 && !Input.GetKeyDown(KeyCode.Space))
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
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

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

        if(isGrounded)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        isGrounded = false;
    }
}
