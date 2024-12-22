using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Assign these in the Inspector
    public AudioSource buttonClickAudioSource; // General button click audio
    public AudioSource monkeyAudioSource;     // Exclusive audio for MONKEY button

    private bool isMuted = false;

    public void StartGame()
    {
        PlayButtonSound();
        // Load the game scene (replace "Level 1" with your scene name)
        // SceneManager.LoadScene("Level 1");
    }

    public void MONKEY()
    {
        if (monkeyAudioSource != null)
        {
            monkeyAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("MONKEY AudioSource is not assigned!");
        }
    }

    public void MuteSound()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        Debug.Log($"Sound muted: {isMuted}");
    }

    public void ExitGame()
    {
        PlayButtonSound();
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    public void HelpMenu()
    {
        PlayButtonSound();
        // Load the help menu scene (replace "HelpMenu" with your scene name)
        // SceneManager.LoadScene("HelpMenu");
    }

    private void PlayButtonSound()
    {
        if (buttonClickAudioSource != null)
        {
            buttonClickAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("ButtonClick AudioSource is not assigned!");
        }
    }

    public void ResetButtonState(GameObject button)
    {
        // Resets button interactable state to visually reset
        var buttonComponent = button.GetComponent<UnityEngine.UI.Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = false; // Temporarily disable
            buttonComponent.interactable = true;  // Re-enable to reset state
        }
    }
}
