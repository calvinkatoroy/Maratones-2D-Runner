using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButtonManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip slideSound;
    public AudioClip pauseSound;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Buttons")]
    public Button pauseButton;
    public Toggle muteToggle;
    public Button jumpButton;
    public Button slideButton;

    [Header("Player Reference")]
    public PlayerController playerController; // Reference to the PlayerController script

    [Header("UI Elements")]
    public GameObject pauseUI; // Assign the GameObject to activate/deactivate on pause

    private bool isPaused = false;

    void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is not assigned in PlayButtonManager.");
            return;
        }

        // Assign button click listeners
        pauseButton.onClick.AddListener(() => { TogglePause(); });
        muteToggle.onValueChanged.AddListener((isMuted) => { ToggleMute(isMuted); });
        jumpButton.onClick.AddListener(() => { TriggerJump(); });
        slideButton.onClick.AddListener(() => { TriggerSlide(); });
    }

    void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            SimulateButtonPress(jumpButton);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SimulateButtonPress(slideButton);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SimulateButtonPress(pauseButton);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            muteToggle.isOn = !muteToggle.isOn; // Flip toggle state programmatically
        }
    }

    // Simulate a button press with visual feedback
    private void SimulateButtonPress(Button button)
    {
        // Highlight the button to trigger the visual state transition
        EventSystem.current.SetSelectedGameObject(button.gameObject);

        // Simulate the button click
        button.onClick.Invoke();

        // Unselect the button after a short delay to allow sprite transition
        Invoke(nameof(ClearSelectedObject), 0.1f);
    }

    // Clear the selected object in the EventSystem
    private void ClearSelectedObject()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void TriggerJump()
    {
        PlaySound(jumpSound);
        Debug.Log("Jump triggered");
    }

    private void TriggerSlide()
    {
        PlaySound(slideSound);
        Debug.Log("Slide triggered");
    }

    private void TogglePause()
{
    isPaused = !isPaused;
    Time.timeScale = isPaused ? 0 : 1;

    // Activate or deactivate the pause UI
    if (pauseUI != null)
    {
        pauseUI.SetActive(isPaused);
    }
    else
    {
        Debug.LogWarning("Pause UI is not assigned in the PlayButtonManager.");
    }

    PlaySound(pauseSound);
    Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }


    private void ToggleMute(bool isMuted)
    {
        if (audioSource != null)
        {
            audioSource.mute = isMuted;
        }
        Debug.Log(isMuted ? "Muted" : "Unmuted");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
