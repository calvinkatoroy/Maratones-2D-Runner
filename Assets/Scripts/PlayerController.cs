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
    public bool isSliding;

    [Header("Crouch Variables")]
    public float crouchHeight = 0.5f; // Adjusted to match collider size
    private Vector2 normalHeight;
    private Vector2 normalOffset;
    private float yInput;

    [Header("Double Jump Variables")]
    public GameObject KentutPrefab;

    private Animator animator;
    private Transform playerSprite; // Reference to PlayerSprite
    private BoxCollider2D boxCollider; // Reference to Player's BoxCollider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerSprite = transform.Find("PlayerSprite"); // Find the child GameObject
        if (playerSprite != null)
        {
            animator = playerSprite.GetComponent<Animator>(); // Get the Animator on PlayerSprite
        }
        else
        {
            Debug.LogError("PlayerSprite GameObject not found!");
        }
        normalHeight = new Vector2(boxCollider.size.x, boxCollider.size.y);
        normalOffset = new Vector2(boxCollider.offset.x, boxCollider.offset.y);
    }

    void Update()
    {
        // Ground Check
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            isGrounded = true;
            isJumping = false;
            isFalling = false;
        }
        else if (rb.velocity.y > 0)
        {
            isJumping = true;
            isFalling = false;
            isGrounded = false;
        }
        else if (rb.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
        }

        // Walking logic
        bool isWalking = Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded;

        // Sliding logic
        yInput = Input.GetAxisRaw("Vertical");
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isGrounded)
        {
            boxCollider.size = new Vector2(normalHeight.x, crouchHeight);
            boxCollider.offset = new Vector2(normalOffset.x, 0.253f); // Adjusted offset for sliding
            isSliding = true;
        }
        else
        {
            boxCollider.size = normalHeight;
            boxCollider.offset = normalOffset;
            isSliding = false;
        }

        // Update Animator parameters
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
            animator.SetBool("IsJumping", isJumping);
            animator.SetBool("IsFalling", isFalling);
            animator.SetBool("IsSliding", isSliding);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true; // Allow double jump
            }
            else if (canDoubleJump)
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
        if (!isGrounded && isHoldingJump)
        {
            holdJumpTimer += Time.deltaTime;
            if (holdJumpTimer >= maxHoldJumpTime)
            {
                isHoldingJump = false;
            }
        }

        // Move horizontally if grounded
        if (isGrounded)
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
