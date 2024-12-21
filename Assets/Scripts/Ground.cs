using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight = 7f;   
    private BoxCollider2D groundCollider;

    private void Awake()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (groundCollider.size.y / 2f);
    }

    // If groundHeight is actually needed by other scripts, 
    // make sure those scripts access it through something like:
    // float currentHeight = groundInstance.groundHeight;
}
