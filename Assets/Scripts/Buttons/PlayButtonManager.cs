using UnityEngine;
using UnityEngine.UI;

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
    public Button muteButton;
    public Button jumpButton;
    public Button slideButton;

    private bool isPaused = false;
    private bool isMuted = false;

    void Start()
    {
        // Assign button click listeners for UI interactions
        pauseButton.onClick.AddListener(() => HandleButtonClick(pauseButton, TogglePause));
        muteButton.onClick.AddListener(() => HandleButtonClick(muteButton, ToggleMute));
        jumpButton.onClick.AddListener(() => HandleButtonClick(jumpButton, PlayJump));
        slideButton.onClick.AddListener(() => HandleButtonClick(slideButton, PlaySlide));
    }

    void Update()
    {
        HandleKeyInput();
    }

    private void HandleKeyInput()
    {
        // Handle jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            PlayJump();
            ResetButtonState(jumpButton.gameObject);
        }

        // Handle slide
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlaySlide();
            ResetButtonState(slideButton.gameObject);
        }

        // Handle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            ResetButtonState(pauseButton.gameObject);
        }

        // Handle mute
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
            ResetButtonState(muteButton.gameObject);
        }
    }

    private void HandleButtonClick(Button button, System.Action action)
    {
        action.Invoke();
        ResetButtonState(button.gameObject);
    }

    private void PlayJump()
    {
        PlaySound(jumpSound);
        Debug.Log("Jump triggered");
    }

    private void PlaySlide()
    {
        PlaySound(slideSound);
        Debug.Log("Slide triggered");
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        PlaySound(pauseSound);
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;
        if (audioSource != null)
        {
            audioSource.mute = isMuted;
        }
        Debug.Log(isMuted ? "Muted" : "Unmuted");
    }

    private void ResetButtonState(GameObject button)
    {
        var buttonComponent = button.GetComponent<Button>();
        if (buttonComponent != null)
        {
            // Force the button to reset its state
            buttonComponent.interactable = false;
            buttonComponent.interactable = true;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
