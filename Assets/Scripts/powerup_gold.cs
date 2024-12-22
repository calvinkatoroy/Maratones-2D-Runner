using UnityEngine;

public class powerup_gold : MonoBehaviour
{
    public float powerupTime = 5f;
    public float powerupTimeLeft = 0f;
    private BoxCollider2D playerCollider;

    void Update()
    {
        if (powerupTimeLeft > 0)
        {
            powerupTimeLeft -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            BoxCollider2D playerBoxCollider = other.GetComponent<BoxCollider2D>();
            if (playerBoxCollider != null)
            {
                //kode biar dia invisible ke enemy
                powerupTimeLeft = powerupTime;
            }
        }
    }
}