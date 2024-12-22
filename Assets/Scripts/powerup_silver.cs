using System.Threading;
using UnityEngine;

public class powerup_silver : MonoBehaviour
{
    public float additionalTime = 10f; // Time to add in seconds
    public Timer timer;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            timer = other.GetComponent<Timer>();
            if (timer != null) timer.addTimer(additionalTime);
        }
    }
}