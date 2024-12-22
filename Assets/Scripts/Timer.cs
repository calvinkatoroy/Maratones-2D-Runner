using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public int gameDurationInSeconds = 10; // Total game duration
    public Color normalColor = Color.white; // Default label color
    public Color warningColor = Color.red; // Label color when 10 seconds remain

    private Label timerLabel;
    private float currentTime;
    public bool isGameOver = false;

    public GameObject player; 

    void Start()
    {
        // Get the root UI document
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Find the label
        timerLabel = root.Q<Label>("PlayTimerLabel");

        // Initialize time
        currentTime = gameDurationInSeconds;

        // Set initial color
        timerLabel.style.color = normalColor;

        // Update the timer label initially
        UpdateTimerLabel();
    }

    void Update()
    {
        if(player == null)
        {
            isGameOver = true;
            GameOver();
        }

        if (isGameOver) return; // Stop updating if game is over

        // Decrease time
        currentTime -= Time.deltaTime;

        // Check if time is up
        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

        // Check if warning time is reached
        if (currentTime <= 10 && timerLabel.style.color != warningColor)
        {
            timerLabel.style.color = warningColor; // Change label color to red
        }

        // Update the label
        UpdateTimerLabel();
    }

    private void UpdateTimerLabel()
    {
        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerLabel.text = $"{minutes:D2}:{seconds:D2}";
    }

    private void GameOver()
    {
        isGameOver = true;

        // Trigger game over logic
        Debug.Log("Game Over!");
        // Optionally: Call another method to handle game over, e.g., show Game Over screen
    }
}
