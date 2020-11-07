using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public Transform startpoint;

    public GameObject Player;

    public bool chasePlayer;

    public bool goToStart;

    private Pathfinding pathfinding;

    public List<Node> finalPath;
    
    //[SerializeField]
    public List<Transform> patrolPoints;
    int currentIndex;

    private Vector3 nodePosition;

    public int currentPosition;

    public GlobalShit globalSC;

    private float speed;
    

    void Start()
    {

        currentPosition = 0;
        startpoint = GetComponent<Transform>();
        pathfinding = GetComponent<Pathfinding>();

        
        chasePlayer = false;
        goToStart = true;

        if (patrolPoints != null && patrolPoints.Count >= 2)
        {
            currentIndex = 0;

            pathfinding.targetPosition = patrolPoints[currentIndex];
        }
    }

    void Update()
    {
        speed = globalSC.ghostSpeed;
        //print("v: "+ speed);

        if (globalSC.playerSeen && !chasePlayer) 
        { 
            pathfinding.targetPosition = globalSC.gargyole;

        } //Si una gargola ve al jugador, los fantasman van a esa posicion
        else if (!globalSC.playerSeen && !chasePlayer) { goToStart=true; pathfinding.targetPosition = patrolPoints[currentIndex]; } //

        if (chasePlayer && finalPath!=null)
        {
            if (currentPosition >= finalPath.Count)
                currentPosition = 0;
            //stops one node before the final position
            if (finalPath[currentPosition] != finalPath[finalPath.Count - 2])
            {
                nodePosition = finalPath[currentPosition].position;
                Walk();
            }
            
        }

        else if(goToStart)
        {
            //return to the start point

            //print("target: " + pathfinding.targetPosition.name);
            //print("n: " + patrolPoints.Count);

            //if(Vector3.Distance(finalPath[currentPosition].position, patrol1.transform.position) < 1.5f) { pathfinding.targetPosition = patrol2.transform; }
            //if(Vector3.Distance(finalPath[currentPosition].position, patrol2.transform.position) < 1.5f) { pathfinding.targetPosition = patrol1.transform; }

            SetWayPoint();

            if (finalPath[currentPosition] != finalPath[finalPath.Count - 1])
            {
                nodePosition = finalPath[currentPosition].position;
                Walk();
            }
            else
            {
                goToStart = false;
            }
        }
    }

   private void Walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, nodePosition - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, nodePosition, speed * Time.deltaTime);

        if (transform.position == nodePosition)
        {
            currentPosition++;
            nodePosition = finalPath[currentPosition].position;
        }
    }

    private void SetWayPoint()
    {
        if (Vector3.Distance(finalPath[currentPosition].position, patrolPoints[currentIndex ].position) < 1)
        {
            if (currentIndex + 1 > patrolPoints.Count-1) { currentIndex = 0; }
            else { currentIndex += 1; }
            pathfinding.targetPosition = patrolPoints[currentIndex];
        }
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("chaseRange"))
        {
            print("Dentro de rango");
            goToStart = false;
            chasePlayer = true;
            pathfinding.targetPosition = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("chaseRange"))
        {
            currentPosition = 0;
            print("Saliendo de rango");
            chasePlayer = false;
            goToStart = true;
            pathfinding.targetPosition = patrolPoints[currentIndex];

        }
    }

}