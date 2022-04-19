using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeLimit; //seconds

    private float timeLeft; //seconds
    public float TimeLeft()
    {
        return timeLeft;
    }
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeLimit;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
        timeLeft -= Time.deltaTime;
        }
        if(timeLeft<=0){
            timeLeft = timeLimit;
            isActive = false;
        }

    }

    public void StartTimer(){
        isActive = true;
    }

    public bool IsActive(){
        return isActive;
    }
}
