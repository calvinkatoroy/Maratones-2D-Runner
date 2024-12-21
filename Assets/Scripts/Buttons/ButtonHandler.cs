using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonHandler : MonoBehaviour
{
    public UIDocument uiDocument; // Reference to the UI Document

    // Define a structure to handle button-specific properties
    [System.Serializable]
    public class ButtonSpriteConfig
    {
        public string buttonName; // Name of the button in UI Builder
        public Sprite defaultSprite; // Default sprite for the button
        public Sprite pressedSprite; // Pressed sprite for the button
    }

    public List<ButtonSpriteConfig> buttonConfigs = new List<ButtonSpriteConfig>(); // List of buttons
    public List<KeyCode> triggerKeys = new List<KeyCode>(); // List of keys to trigger the pressed sprite

    private Dictionary<string, VisualElement> buttonElements = new Dictionary<string, VisualElement>();
    private Dictionary<string, Sprite> currentSprites = new Dictionary<string, Sprite>();

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        // Initialize all buttons
        foreach (var config in buttonConfigs)
        {
            var button = root.Q<VisualElement>(config.buttonName);

            if (button != null)
            {
                // Save the button reference for later
                buttonElements[config.buttonName] = button;

                // Set the default sprite as the initial background
                button.style.backgroundImage = new StyleBackground(config.defaultSprite);

                // Store the current sprite state
                currentSprites[config.buttonName] = config.defaultSprite;
            }
            else
            {
                Debug.LogWarning($"Button with name '{config.buttonName}' not found in UI Document!");
            }
        }
    }

    void Update()
    {
        // Check for any key press
        foreach (var key in triggerKeys)
        {
            if (Input.GetKeyDown(key))
            {
                ChangeAllToPressedSprite();
            }
            if (Input.GetKeyUp(key))
            {
                RevertAllToDefaultSprite();
            }
        }
    }

    private void ChangeAllToPressedSprite()
    {
        foreach (var config in buttonConfigs)
        {
            if (buttonElements.TryGetValue(config.buttonName, out var button))
            {
                button.style.backgroundImage = new StyleBackground(config.pressedSprite);
                currentSprites[config.buttonName] = config.pressedSprite;
            }
        }
    }

    private void RevertAllToDefaultSprite()
    {
        foreach (var config in buttonConfigs)
        {
            if (buttonElements.TryGetValue(config.buttonName, out var button))
            {
                button.style.backgroundImage = new StyleBackground(config.defaultSprite);
                currentSprites[config.buttonName] = config.defaultSprite;
            }
        }
    }
}
