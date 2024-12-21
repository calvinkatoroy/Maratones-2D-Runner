using UnityEngine;

//make this code to gamble 50 50 chance to get debuff slow or getting small
public class debuff : MonoBehaviour
{
    float debuffTime = 5f; // Time left for the powerup
    public float debuffTimeLeft;
    float internal_speed;
    float internal_jumpForce;
    //if colide with enemy, debuff
    private PlayerController playerController; // Store the player's controller reference

    void update()
    {
        if (debuffTimeLeft > 0)
        {
            debuffTimeLeft -= Time.deltaTime;
        }
        else if (playerController != null)
        {
            // Reset the jump force to its original value and clear the reference
            playerController.jumpForce = internal_jumpForce;
            playerController.speed = internal_speed;
            playerController = null; // Prevent further updates after reset
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //randomize debuff
            int debuff = Random.Range(0, 2);
            if (debuff == 0)
            {
                internal_jumpForce = playerController.jumpForce;
                playerController.jumpForce /= 2;
                playerController = null; // Prevent further updates after reset
            }
            else
            {
                internal_speed = playerController.speed;
                playerController.speed /= 2;
                playerController = null; // Prevent further updates after reset
            }
            debuffTimeLeft = debuffTime;
        }
    }
}