using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AllButtonHandler : MonoBehaviour
{
    [System.Serializable]
    public class ButtonSettings
    {
        public Button button;            // Reference to the button
        public bool isToggle;            // Is this button a toggle?
        public bool hasSound;            // Does this button play a sound?
        public AudioClip buttonClip;     // The sound clip for this button
        public Toggle toggle;            // Reference to the toggle (if applicable)
        public Image toggleImage;        // The image for the toggle (if applicable)
        public Sprite spriteOn;          // Sprite for the "On" state (if toggle)
        public Sprite spriteOff;         // Sprite for the "Off" state (if toggle)
        public UnityEngine.Events.UnityEvent onClickEvent; // Events to invoke on button press
    }

    public AudioSource sharedAudioSource; // Shared AudioSource for button sounds
    public ButtonSettings[] buttonSettings; // Array to hold all button settings

    private void Start()
    {
        foreach (var setting in buttonSettings)
        {
            if (setting.isToggle && setting.toggle != null)
            {
                // Initialize the toggle state based on PlayerPrefs (if it's a mute toggle)
                bool isMuted = PlayerPrefs.GetInt("MuteState", 0) == 1;
                setting.toggle.isOn = !isMuted;
                UpdateToggleVisual(setting);

                // Add a listener for the toggle state change
                setting.toggle.onValueChanged.AddListener((isOn) => OnToggleChange(setting, isOn));
            }
            else if (setting.button != null)
            {
                // Add listener for non-toggle buttons
                setting.button.onClick.AddListener(() => OnButtonClick(setting));
            }
        }
    }

    private void OnButtonClick(ButtonSettings setting)
    {
        // Play sound if enabled
        if (setting.hasSound && sharedAudioSource != null && setting.buttonClip != null && !AudioListener.pause)
        {
            sharedAudioSource.clip = setting.buttonClip;
            sharedAudioSource.Play();
        }

        // Invoke any assigned event
        setting.onClickEvent?.Invoke();

        // Clear button selection to avoid UI issues
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnToggleChange(ButtonSettings setting, bool isOn)
    {
        // Handle mute toggle functionality
        if (setting.toggle != null)
        {
            AudioListener.pause = !isOn;

            // Update PlayerPrefs for mute state
            PlayerPrefs.SetInt("MuteState", isOn ? 0 : 1);
            PlayerPrefs.Save();
        }

        // Update the toggle's visual (sprite)
        UpdateToggleVisual(setting);
    }

    private void UpdateToggleVisual(ButtonSettings setting)
    {
        if (setting.toggleImage != null)
        {
            setting.toggleImage.sprite = setting.toggle.isOn ? setting.spriteOn : setting.spriteOff;
        }
    }
}
