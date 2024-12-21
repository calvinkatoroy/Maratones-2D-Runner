using UnityEngine;

public class Kentut : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f;

    void Start()
    {
        // Destroy this object after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}
