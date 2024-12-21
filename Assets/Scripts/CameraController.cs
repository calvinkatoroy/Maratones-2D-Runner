using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private float initialcameraY;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        initialcameraY = -0.38f; //contoh initial camera position
        transform.position = new Vector3(player.transform.position.x + 5f, player.transform.position.y + 7f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        transform.position = new Vector3(player.transform.position.x + 5f, Mathf.Clamp(player.transform.position.y, initialcameraY +3f, player.transform.position.y), -10);
    }
}
