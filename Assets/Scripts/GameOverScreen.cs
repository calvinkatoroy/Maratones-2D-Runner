using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{
    public UIDocument uiDocument; // Reference to the UI Document
    private Label GameOverLabel;
    private VisualElement bg;
    public UIDocument timer;
    public LevelManager levelmanager;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        GameOverLabel = root.Q<Label>("GameOverText");
        bg = root.Q<VisualElement>("GameOverScreen");
        GameOverLabel.style.display = DisplayStyle.None;
        root.style.display = DisplayStyle.Flex;
    }

    void Update()
    {
        if(timer.GetComponent<Timer>().isGameOver == true)
        {
            GameOverLabel.style.display = DisplayStyle.Flex;
            bg.style.color = new Color(0, 0, 0, 0.5f);
            bg.style.backgroundColor = new Color(0, 0, 0, 0.5f);
            bg.style.display = DisplayStyle.Flex;
            Debug.Log("Script GameOver is working correcty. ");
        }
        else if(levelmanager.levelcompleted == true)
        {
            GameOverLabel.text = "Level " + levelmanager.level + " completed!";
            GameOverLabel.style.display = DisplayStyle.Flex;
            bg.style.color = new Color(0, 0, 0, 0.5f);
            bg.style.backgroundColor = new Color(0, 0, 0, 0.5f);
            bg.style.display = DisplayStyle.Flex;
            Debug.Log("Move to next stage here.");
        }
    }
}
