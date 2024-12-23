using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance to ensure one AudioManager

    public AudioSource audioSource; // Reference to the AudioSource that will play background music
    public AudioClip[] levelMusic;  // Array to store music clips for different levels

    public AudioSource SFXSource; // Reference to the AudioSource that will play sound effects
    public AudioClip SFXClips; // Array to store sound effect clips

    public LevelManager levelManager; // Reference to the LevelManager to get the current level
    private bool isMusicPlaying = false;
    private int currentLevel = -1; // Default value to ensure the first level music is played

    public AudioClip GameOver;
    public AudioClip StageClear;
    public AudioClip getItem;
    public AudioClip jump;
    public AudioClip hurt;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>(); // Find the LevelManager in the scene
        currentLevel = levelManager.level;
        Debug.Log("Level: " + currentLevel);
        // Ensure there is only one AudioManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager
            return;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Try to get AudioSource if it's not set manually
        }
    }

    void Start()
    {
        // Start playing music for the first level or load default music if needed
        audioSource.clip = levelMusic[currentLevel];
        audioSource.Play();
        isMusicPlaying = true;
    }

    // Method to update the music based on the level
    public void PlayMusicForLevel(int level)
    {
        if (level < 0 || level >= levelMusic.Length)
        {
            Debug.LogWarning("Music not assigned for this level.");
            return;
        }

        // If the level has changed, stop the current music and play the new one
        if (level != currentLevel)
        {
            currentLevel = level;

            // Stop the current music and start playing the new music
            audioSource.clip = levelMusic[level];
            audioSource.Play();
            isMusicPlaying = true;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
