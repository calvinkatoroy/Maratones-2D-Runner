using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private PlayerController playerMovement;
    private Animator animator;
    private float CameraBottomEdge;
    private bool isDead = false; // To prevent multiple triggers

    // The intended spawn position for the player
    private Vector3 spawnPosition = new Vector3(-2.5f, 0f, 0f);

    void Start()
    {
        playerMovement = GetComponent<PlayerController>();
        animator = transform.Find("PlayerSprite").GetComponent<Animator>();

        // Set the player's initial position to (-2.5, 0)
        transform.position = spawnPosition;

        // Calculate the initial CameraBottomEdge based on the main camera
        if (Camera.main != null)
        {
            CameraBottomEdge = Camera.main.transform.position.y - Camera.main.orthographicSize;
        }
        else
        {
            Debug.LogError("Main Camera not found! Ensure there is a camera in the scene.");
        }
    }

    void LateUpdate()
    {
        // Recalculate the CameraBottomEdge dynamically in case the camera moves
        if (Camera.main != null)
        {
            CameraBottomEdge = Camera.main.transform.position.y - Camera.main.orthographicSize;
        }

        if (animator != null)
        {
            animator.SetBool("IsJumping", playerMovement.isJumping);
            animator.SetBool("IsFalling", playerMovement.isFalling);
            animator.SetBool("IsSliding", playerMovement.isSliding);
        }

        if (playerMovement.isJumping)
        {
            if (playerMovement.isFalling)
            {
                animator.SetBool("IsFalling", true);
            }
            else
            {
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsJumping", true);
            }
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsWalking", true);
        }

        // Destroy the player if it falls below the bottom edge
        if (transform.position.y <= CameraBottomEdge && !isDead)
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {
        isDead = true; // Prevent multiple triggers
        Debug.LogError("Player fell below the camera bounds!");
        
        // Transition to the death screen scene
        SceneManager.LoadScene("DeathScreen");
    }

    #region Singleton
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate player GameObjects
        }
        else
        {
            Instance = this;
        }

        // Ensure the player starts at the intended spawn position during initialization
        transform.position = spawnPosition;
    }
    #endregion
}
