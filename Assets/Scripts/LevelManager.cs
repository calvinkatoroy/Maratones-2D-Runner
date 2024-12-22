using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject Pisang;
    public GameObject Player;
    public bool levelcompleted = false;
    public int level = 0;
 
    void Start()
    {
        
    }

    void Update()
    {
        if(levelcompleted == true)
        {
            Debug.Log("Level " + level +  " completed.");
        }
        levelcompleted = false;

        //more levelcompleted logic di sini
        //PINDAHIN LEVEL TO NEXT SCENE DI SINI
        //Jangan lupa set levelcompleted = false
    }
}