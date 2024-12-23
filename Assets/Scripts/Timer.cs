using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public int gameDurationInSeconds = 10; // Total game duration
    public Color normalColor = Color.white; // Default label color
    public Color warningColor = Color.red; // Label color when 10 seconds remain

    [Header("UI Components")]
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI for the timer

    [Header("Game State")]
    public float currentTime;
    public bool isGameOver = false;

    [Header("Audio Manager")]
    public AudioManager audioManager;

    [Header("Player")]
    public GameObject player; // Reference to the player GameObject

    void Start()
    {
        // Initialize time
        currentTime = gameDurationInSeconds;

        // Set initial color
        if (timerText != null)
        {
            timerText.color = normalColor;
        }

        // Update the timer label initially
        UpdateTimerLabel();
    }

    void Update()
    {
        if (player == null)
        {
            isGameOver = true;
            GameOver();
        }

        if (isGameOver) return; // Stop updating if the game is over

        // Decrease time
        currentTime -= Time.deltaTime;

        // Check if time is up
        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

        // Check if warning time is reached
        if (currentTime <= 10 && timerText.color != warningColor)
        {
            timerText.color = warningColor; // Change label color to red
        }

        // Update the timer text
        UpdateTimerLabel();
    }

    private void UpdateTimerLabel()
    {
        if (timerText == null) return;

        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");

        // Check if the player exists and call the HandleDeath method
        if (player != null)
        {
            player.GetComponent<Player>().HandleDeath();
        }
        else
        {
            Debug.LogError("Player GameObject is null! Cannot call HandleDeath.");
        }
    }

    public void AddTimer(float addTime)
    {
        currentTime += addTime;
    }
}
