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
    void Awake()
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
            isActive = false;
        }

    }

    public void StartTimer(){
        isActive = true;
        timeLeft = timeLimit;
    }

    public void EndTimer(){
        isActive = false;
        timeLeft = timeLimit;
    }

    public bool IsActive(){
        return isActive;
    }

    public float timeRatio(){
        return timeLeft/timeLimit;
    }
}
