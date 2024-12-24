using UnityEngine;

public class powerup_silver : MonoBehaviour
{
    public float additionalTime = 10f; // Time to add in seconds
    public GameObject timerGameObject; // Assign the Timer GameObject in the Inspector

    private Timer timer; // Internal reference to the Timer script

    void Start()
    {
        if (timerGameObject != null)
        {
            timer = timerGameObject.GetComponent<Timer>();
            if (timer == null)
            {
                Debug.LogError("No Timer component found on the assigned Timer GameObject!");
            }
        }
        else
        {
            Debug.LogError("Timer GameObject not assigned!");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && timer != null)
        {
            // Add additional time to the timer
            timer.AddTimer(additionalTime);
            Debug.Log($"Added {additionalTime} seconds to the timer.");

            // Optionally destroy the power-up after use
            Destroy(gameObject);
        }
    }
}
