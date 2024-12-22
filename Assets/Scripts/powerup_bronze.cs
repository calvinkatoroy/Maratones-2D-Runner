using UnityEngine;

public class powerup_bronze : MonoBehaviour
{
    public float effect = 2f;
    public float powerupTime = 5f;
    public float powerupTimeLeft = 0f;
    float internal_speed;
    float internal_jumpForce;
    private PlayerController playerController;

    void Update(){
        if (powerupTimeLeft > 0) powerupTimeLeft -= Time.deltaTime;
        else if(playerController != null){
            playerController.jumpForce = internal_jumpForce;
            playerController.speed = internal_speed;
            playerController = null; // Prevent further updates after reset
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerController = other.GetComponent<PlayerController>();
            int chance = Random.Range(0, 4);
            if (playerController != null && chance == 0){
                internal_jumpForce = playerController.jumpForce;
                playerController.jumpForce /= 2;
                playerController = null;
            }
            else if (playerController != null && chance == 1){
                internal_speed = playerController.speed;
                playerController.speed /= 2;
                playerController = null; 
            }
            else if(playerController != null && chance == 2){
                internal_jumpForce = playerController.jumpForce;
                playerController.jumpForce *= 2;
                playerController = null;
            }
            else if(playerController != null && chance == 3){
                internal_speed = playerController.speed;
                playerController.speed *= 2;
                playerController = null;
            }
            powerupTimeLeft = powerupTime;
        }
    }
}