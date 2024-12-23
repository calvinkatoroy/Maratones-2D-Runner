using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioSource buttonClickAudioSource;
    public AudioSource SUARAMONYETYESYES;
    public GameObject helpPanel; // Reference to the Help Panel
    public GameObject mainMenuButtons; // Reference to the Main Menu Buttons

    public void StartGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene("Level 1"); // Replace with your scene name
    }

    public void MONKEY()
    {
        MONYETYESYES();
        Debug.Log("Monkey sound activated!");
    }

    public void ExitGame()
    {
        PlayButtonSound();
        SceneController.instance.ExitScene("ExitMenu");
    }

    public void HelpMenu()
    {
        PlayButtonSound();

        if (helpPanel != null && mainMenuButtons != null)
        {
            // Toggle Help Panel and Main Menu Buttons
            helpPanel.SetActive(true);
            mainMenuButtons.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Help Panel or Main Menu Buttons are not assigned!");
        }
    }

    public void CancelHelpMenu()
    {
        PlayButtonSound();

        if (helpPanel != null && mainMenuButtons != null)
        {
            // Toggle Main Menu Buttons and hide Help Panel
            helpPanel.SetActive(false);
            mainMenuButtons.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Help Panel or Main Menu Buttons are not assigned!");
        }
    }

    private void PlayButtonSound()
    {
        if (buttonClickAudioSource != null && !AudioListener.pause) // Check global mute state
        {
            buttonClickAudioSource.Play();
        }
        else if (buttonClickAudioSource == null)
        {
            Debug.LogWarning("ButtonClick AudioSource is not assigned!");
        }
    }

    private void MONYETYESYES()
    {
        if (SUARAMONYETYESYES != null && !AudioListener.pause) // Check global mute state
        {
            SUARAMONYETYESYES.Play();
        }
        else if (SUARAMONYETYESYES == null)
        {
            Debug.LogWarning("ButtonClick AudioSource is not assigned!");
        }
    }

    public void ResetButtonState(GameObject button)
    {
        var buttonComponent = button.GetComponent<UnityEngine.UI.Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = false;
            buttonComponent.interactable = true;
        }
    }
}
