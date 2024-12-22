using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Countdown : MonoBehaviour
{
    public UIDocument uidocument;
    private bool gameStart = false;
    private int countdownValue = 3; // Start countdown at 3
    private float countdownTimer = 1f; // Countdown update every 1 second

    public Label countdownLabel;

    void Start()
    {
        // You can keep this for DontDestroyOnLoad if you want to persist this object
        // DontDestroyOnLoad(gameObject);
        countdownValue = 3;
    }

    void OnEnable()
    {
        var root = uidocument.rootVisualElement;
        countdownLabel = root.Q<Label>("Countdown"); // Get the label by name
        countdownLabel.style.display = DisplayStyle.Flex; // Make sure the label is visible
    }

    public IEnumerator StartCountdown(Label countdownLabel)
    {
        // Loop for the countdown
        while (countdownValue > 0)
        {
            Time.timeScale = 0; // Pause the game while countdown is running
            countdownLabel.text = countdownValue.ToString(); // Update the text of the countdown
            countdownValue--; // Decrement the countdown

            // Wait for 1 second in real-time, ignoring timeScale
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownLabel.text = "Go!"; // Once countdown finishes, display "Go!"
        yield return new WaitForSecondsRealtime(1f); // Wait 1 second before starting the game

        gameStart = true; // Set gameStart to true to trigger game start logic

        // Optional: You can hide the countdown after it finishes
        countdownLabel.style.display = DisplayStyle.None;

        Time.timeScale = 1; // Resume the game after countdown finishes
    }
}
