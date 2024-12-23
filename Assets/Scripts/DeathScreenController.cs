using UnityEngine;

public class DeathScreenController : MonoBehaviour
{
    // Method to restart the level
    public void RestartLevel()
    {
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadSpecificScene("Level 1"); // Replace with your Level 1 scene name
        }
        else
        {
            Debug.LogError("SceneController instance is not found in the scene!");
        }
    }

    // Method to go back to the main menu
    public void ExitToMainMenu()
    {
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadSpecificScene("MainMenu"); // Replace with your Main Menu scene name
        }
        else
        {
            Debug.LogError("SceneController instance is not found in the scene!");
        }
    }
}
