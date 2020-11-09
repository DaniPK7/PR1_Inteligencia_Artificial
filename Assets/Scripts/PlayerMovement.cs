using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    AudioSource m_AudioSource;

    public bool Sprint;
    float normalSpeed;

    public SphereCollider detectionRange;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        //Sprint
        normalSpeed = m_Animator.speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            Sprint = true; 
            m_Animator.speed = 1.5f * normalSpeed;

            detectionRange.radius = 3.5f;
        }            
        else { Sprint = false; m_Animator.speed = normalSpeed; detectionRange.radius = 1.5f;
        }

        m_Animator.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }

            if (Sprint) 
            {
                m_AudioSource.volume = 1;
            }
            else { m_AudioSource.volume = 0.4f; }
        }
        else
        {
            m_AudioSource.Stop();
            m_AudioSource.volume = 0.4f;
        }


        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    
}
