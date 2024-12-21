using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController playerMovement;
    Animator animator;
    private float CameraBottomEdge;

    void Start()
    {
        playerMovement = GetComponent<PlayerController>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        CameraBottomEdge = Camera.main.transform.position.y - Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        if(playerMovement.isGrounded == true)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsWalking", false);
        }

        if(transform.position.y <= CameraBottomEdge)
        {
            Destroy(gameObject);
        }
    }

    #region Singleton
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    #endregion
}
