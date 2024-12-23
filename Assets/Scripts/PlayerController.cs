using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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

    public float speed = 5f;

    [Header("Crouch Variables")]
    public float crouchHeight = 0.5f; // Adjusted to match collider size
    private Vector2 normalHeight;
    private Vector2 normalOffset;
    private float yInput;

    [Header("Double Jump Variables")]
    public GameObject KentutPrefab;
    public GameObject groundRayObject;

    private Animator animator;
    private Transform playerSprite; // Reference to PlayerSprite
    private CapsuleCollider2D capsuleCollider; // Reference to Player's capsuleCollider

    public LevelManager levelmanager;

    [Header("Powerup Variables")]
    public bool isInvisible = false;

    [Header("Timer")]
    public TextMeshProUGUI timer;
    AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        isGrounded = false;
        playerSprite = transform.Find("PlayerSprite"); // Find the child GameObject
        if (playerSprite != null)
        {
            animator = playerSprite.GetComponent<Animator>(); // Get the Animator on PlayerSprite
        }
        else
        {
            Debug.LogError("PlayerSprite GameObject not found!");
        }
        normalHeight = new Vector2(capsuleCollider.size.x, capsuleCollider.size.y);
        normalOffset = new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y);
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        RaycastHit2D hitGround = Physics2D.Raycast(groundRayObject.transform.position, -Vector2.up);
        if (hitGround.collider != null)
        {
            if (hitGround.distance < 0.1f)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (hitGround.collider.CompareTag("Enemy") && rb.velocity.y < 0 && hitGround.collider.gameObject.layer != 10)
            {
                // Player is falling down and hit an enemy
                Destroy(hitGround.collider.gameObject);  // Destroy the enemy
                rb.velocity = new Vector2(rb.velocity.x, 2f);
                // isGrounded = true;  
            }
        }

        // Walking logic
        // bool isWalking = Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded;

        // Sliding logic
        yInput = Input.GetAxisRaw("Vertical");
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isGrounded)
        {
            capsuleCollider.size = new Vector2(normalHeight.x, crouchHeight);
            capsuleCollider.offset = new Vector2(normalOffset.x, 0.253f); // Adjusted offset for sliding
            isSliding = true;
        }
        else
        {
            capsuleCollider.size = normalHeight;
            capsuleCollider.offset = normalOffset;
            isSliding = false;
        }

        if (rb.velocity.y == 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = false;
            canDoubleJump = false; 
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
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

        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        audioManager = timer.GetComponent<Timer>().audioManager;
        audioManager.PlaySFX(audioManager.jump);
        isJumping = true;
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            levelmanager.levelcompleted = true;
            levelmanager.level++;
            audioManager = timer.GetComponent<Timer>().audioManager;
            audioManager.PlaySFX(audioManager.StageClear);
            Destroy(levelmanager.Pisang);
            Time.timeScale = 0;
        }

        if(!isInvisible && collision.gameObject.CompareTag("Enemy"))
        {
            audioManager = timer.GetComponent<Timer>().audioManager;
            audioManager.PlaySFX(audioManager.hurt);
            timer.GetComponent<Timer>().currentTime -= 2;
            Debug.Log("Skill Issue lu");
            Debug.Log("Player Hit an Enemy");
        }
        else if(collision.gameObject.CompareTag("Ranjau"))
        {
            timer.GetComponent<Timer>().GameOver();
        }
    }
}