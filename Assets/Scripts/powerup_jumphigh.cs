using UnityEngine;

public class powerup_jumphigh : MonoBehaviour
{
    public float jumpMultiplier = 2f;
    public float powerupTime = 5f;
    public float powerupTimeLeft = 0f;
    private PlayerController playerController; // Store the player's controller reference

    void Update()
    {
        if (powerupTimeLeft > 0)
        {
            powerupTimeLeft -= Time.deltaTime;
        }
        else if (playerController != null)
        {
            // Reset the jump force to its original value and clear the reference
            playerController.jumpForce /= jumpMultiplier;
            playerController = null; // Prevent further updates after reset
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.jumpForce *= jumpMultiplier;
                powerupTimeLeft = powerupTime;
            }
        }
    }
}
