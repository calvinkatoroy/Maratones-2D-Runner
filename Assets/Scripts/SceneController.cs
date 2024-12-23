using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance; // Singleton instance
    [SerializeField] private Animator transitionAnim; // Reference to the Animator
    [SerializeField] private float transitionDuration = 1f; // Duration of the transition

    private void Awake()
    {
        // Ensure only one instance of SceneController exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void NextLevel()
    {
        // Load the next scene based on the build index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneByIndex(nextSceneIndex));
    }

    public void LoadSpecificScene(string sceneName)
    {
        // Load a specific scene by name
        StartCoroutine(LoadSceneByName(sceneName));
    }

    public void ExitScene(string sceneName)
    {
        // Start the coroutine to play the exit animation and load the target scene
        StartCoroutine(LoadSceneByName(sceneName));
    }

    private IEnumerator LoadSceneByIndex(int sceneIndex)
    {
        yield return StartCoroutine(PlaySceneEndAnimation());

        // Load the scene by index
        SceneManager.LoadScene(sceneIndex);

        // Optionally trigger the scene-start animation after the scene is loaded
        yield return StartCoroutine(PlaySceneStartAnimation());
    }

    private IEnumerator LoadSceneByName(string sceneName)
    {
        yield return StartCoroutine(PlaySceneEndAnimation());

        // Load the scene by name
        SceneManager.LoadScene(sceneName);

        // Optionally trigger the scene-start animation after the scene is loaded
        yield return StartCoroutine(PlaySceneStartAnimation());
    }

    private IEnumerator PlaySceneEndAnimation()
    {
        if (transitionAnim != null)
        {
            // Reset the "Start" trigger to avoid conflict
            transitionAnim.ResetTrigger("Start");

            // Set the "End" trigger
            transitionAnim.SetTrigger("End");
        }

        // Wait for the animation to complete
        yield return new WaitForSeconds(transitionDuration);
    }

    private IEnumerator PlaySceneStartAnimation()
    {
        if (transitionAnim != null)
        {
            // Reset the "End" trigger to avoid conflict
            transitionAnim.ResetTrigger("End");

            // Set the "Start" trigger
            transitionAnim.SetTrigger("Start");
        }

        yield return null;
    }
}
