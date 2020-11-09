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
    private Observer_Speed gargSC;
    private Material gargLight;


    // Start is called before the first frame update
    void Start()
    {
        timerON = false;
        gargSC = FindObjectOfType<Observer_Speed>();
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
                gargSC.ResetColor(gargLight);
            }
        }
    }
    public void SpeedChange(Transform garg, Material light_Cone)
    {
        playerSeen = true;
        gargLight = light_Cone;
        gargyole = garg;
        ghostSpeed = 2;
        timeRemaining = 10f;
        timerON = true;
    }
}
