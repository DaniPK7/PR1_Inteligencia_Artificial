using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer_Speed : MonoBehaviour
{
    public Transform player;
    bool m_IsPlayerInRange;
    public GameEnding gameEnding;
    public GlobalShit globalSC;
    public Transform wpTransform;
    public Material light_Cone;



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            light_Cone.SetColor("_EmissionColor", Color.yellow);
            print("Cambiando color de " + light_Cone.name + "a amarillo");

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        light_Cone.SetColor("_EmissionColor", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    globalSC.SpeedChange(wpTransform, light_Cone);

                    light_Cone.SetColor("_EmissionColor", Color.red);
                    print("Cambiando color de " + light_Cone.name + " a red");


                }
            }
        }
       
    }

    public void ResetColor(Material l) 
    {
        l.SetColor("_EmissionColor", Color.green);
        print("Cambiando color de "+ l.name + " a verde");
    }
    
}
