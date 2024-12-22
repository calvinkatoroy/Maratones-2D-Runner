using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class powerup_silver : MonoBehaviour
{
    public float additionalTime = 10f; // Time to add in seconds
    public UIDocument timer;

    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
           // timer = other.GetComponent<Timer>();
            if (timer != null) timer.GetComponent<Timer>().currentTime += additionalTime;
        }
    }
}