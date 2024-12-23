using UnityEngine;
using UnityEngine.UI;

public class MuteButtonHandler : MonoBehaviour
{
    public Toggle muteButton;        // Reference to the Toggle button
    public Image buttonImage;        // Reference to the image component
    public Sprite audioOnSprite;     // Sprite for "Audio On"
    public Sprite audioOffSprite;    // Sprite for "Audio Off"

    private void Start()
    {
        // Load mute state from PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt("MuteState", 0) == 1;

        // Set the initial state of the toggle
        muteButton.isOn = !isMuted;

        // Initialize audio state
        UpdateAudioState(muteButton.isOn);

        // Add listener to toggle for state changes
        muteButton.onValueChanged.AddListener(UpdateAudioState);
    }

    private void UpdateAudioState(bool isOn)
    {
        // Handle audio muting
        AudioListener.pause = !isOn;

        // Update the button sprite
        buttonImage.sprite = isOn ? audioOnSprite : audioOffSprite;

        // Save mute state to PlayerPrefs
        PlayerPrefs.SetInt("MuteState", isOn ? 0 : 1); // Save "muted" as 1 when isOn is false
        PlayerPrefs.Save();
    }
}
