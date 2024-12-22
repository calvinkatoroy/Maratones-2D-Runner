using UnityEngine;

public class powerup_gold : MonoBehaviour
{
    public float powerupTime = 5f;
    public float powerupTimeLeft = 0f;
    private BoxCollider2D playerCollider;
    public PlayerController playerController;

    void Update()
    {
        if (powerupTimeLeft > 0)
        {
            powerupTimeLeft -= Time.deltaTime;
        }
        else
        {
            playerController.isInvisible = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        powerupTimeLeft = powerupTime; // Reset powerup time
        if(other.CompareTag("Player"))
        {
            Debug.Log("Powerup Gold Collected!");
            BoxCollider2D playerBoxCollider = other.GetComponent<BoxCollider2D>();
            if (playerBoxCollider != null)
            {
                //kode biar dia invisible ke enemy
                playerController.isInvisible = true;
                powerupTimeLeft = powerupTime;
            }
        }
    }
}