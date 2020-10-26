using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    public GameObject Player;

    int m_CurrentWaypointIndex;

    public bool chasePlayer;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        chasePlayer = false;
    }

    void Update()
    {
        if (chasePlayer)
        {
            navMeshAgent.SetDestination(Player.transform.position);
        }

        else
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
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