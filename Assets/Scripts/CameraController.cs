using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float initialcameraY;
    private Transform player; // Cache the player's Transform

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player"); // Adjust to match the correct name
        if (playerObject != null)
        {
            player = playerObject.transform; // Cache the Transform
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }

        initialcameraY = -0.38f; // Example initial camera Y position
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + 5f, player.position.y + 7f, -10);
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                player.position.x + 5f,
                Mathf.Clamp(player.position.y, initialcameraY + 3f, player.position.y),
                -10
            );
        }
    }
}
