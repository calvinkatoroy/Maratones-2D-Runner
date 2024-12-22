using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance to ensure one AudioManager

    public AudioSource audioSource; // Reference to the AudioSource that will play background music
    public AudioClip[] levelMusic;  // Array to store music clips for different levels

    private int currentLevel = -1;   // Track the current level
    private bool isMusicPlaying = false;

    void Awake()
    {
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

        // Do not destroy the AudioManager between scenes
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Try to get AudioSource if it's not set manually
        }
    }

    void Start()
    {
        // Start playing music for the first level or load default music if needed
        if (levelMusic.Length > 0)
        {
            PlayMusicForLevel(0); // Play music for level 0 initially
        }
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

    // Optionally, fade in music
    public void FadeInMusic(float fadeDuration = 1f)
    {
        StartCoroutine(FadeInCoroutine(fadeDuration));
    }

    private IEnumerator FadeInCoroutine(float fadeDuration)
    {
        audioSource.volume = 0f;
        audioSource.Play();
        float targetVolume = 1f;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;  // Ensure the volume is set to 1 at the end
    }

    // Optional: Fade out music
    public void FadeOutMusic(float fadeDuration = 1f)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        float startVolume = audioSource.volume;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();  // Stop the music after fading out
    }
}
