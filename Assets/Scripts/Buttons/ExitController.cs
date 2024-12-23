using UnityEngine;

public class ExitController : MonoBehaviour
{
    public void OnYesButtonPressed()
    {
        // Exit the game or simulation
        #if UNITY_EDITOR
            // Exit Play Mode in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Exit the built game
            Application.Quit();
        #endif
    }

    public void OnNoButtonPressed()
    {
        // Use SceneController to handle the scene transition
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadSpecificScene("MainMenu");
        }
        else
        {
            Debug.LogError("SceneController instance is not found in the scene!");
        }
    }
}
