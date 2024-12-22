using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] public float depth;
    public Vector3 spawnpoint;
    public CameraController cameraController;
    private float CameraLeftEdge;

    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnpoint = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraLeftEdge = Camera.main.transform.position.x - Camera.main.orthographicSize;
        spawnpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return; // If the game is paused, stop updating the parallax
        }
        
        float realVelocity = player.rb.velocity.x / depth;
        Vector3 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= CameraLeftEdge) { pos = spawnpoint; }

        transform.position = pos;
    }
}