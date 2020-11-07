using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalShit : MonoBehaviour
{

    public int ghostSpeed;
    private float timeRemaining;
    private bool timerON;

    public bool playerSeen;//si Soy
    public Transform gargyole;


    // Start is called before the first frame update
    void Start()
    {
        timerON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerON)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else 
            {
                ghostSpeed = 1;
                timerON = false;
                playerSeen = false;
            }
        }
    }
    public void SpeedChange(Transform garg)
    {
        playerSeen = true;
       
        gargyole = garg;
        ghostSpeed = 2;
        timeRemaining = 10f;
        timerON = true;
    }
}
