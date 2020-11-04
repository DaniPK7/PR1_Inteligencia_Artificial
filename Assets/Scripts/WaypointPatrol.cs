using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public Vector3 startpoint;

    public GameObject Player;

    public Grid grid;

    public bool chasePlayer;

    void Start()
    {
        startpoint=GetComponent<Transform>().position;
        chasePlayer = false;
        
    }

    void Update()
    {
        if (chasePlayer)
        {
            grid.Awake();
        }

        else
        {
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("chaseRange"))
        {
            print("Dentro de rango");
            chasePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("chaseRange"))
        {
            print("Saliendo de rango");
            chasePlayer = false;
        }
    }

}