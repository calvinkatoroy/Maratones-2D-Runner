using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public float crouchHeight = 0.6f;
    public PlayerController player;

    private Vector2 normalHeight;
    private float yInput;

    // Start is called before the first frame update
    void Start()
    {
        normalHeight = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        yInput = Input.GetAxisRaw("Vertical");

        if (yInput < 0 && player.isGrounded)
        {
            transform.localScale = new Vector2(normalHeight.x, crouchHeight);
        }
        else
        {
            if (transform.localScale.y != normalHeight.y)
            transform.localScale = normalHeight;
        }
    }
}
