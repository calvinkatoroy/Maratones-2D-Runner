using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes

public class PISANGAPISANGA : MonoBehaviour
{
    public GameObject player; // Assign the player GameObject in the Inspector
    public string sceneToLoad; // Assign the name of the scene to load in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the assigned player
        if (other.gameObject == player)
        {
            Debug.Log("Player collided with PISANGAPISANGA. Loading scene: " + sceneToLoad);

            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
