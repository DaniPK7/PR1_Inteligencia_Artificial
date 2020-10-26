using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateG : MonoBehaviour
{

    
    public float delta = 0.85f;  // Amount to move left and right from the start point
    public float speed = 0.55f;
    private Quaternion startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion v = startPos;
         v.y += delta * Mathf.Sin (Time.time * speed);
         transform.rotation = v;
        /*transform.Rotate(Vector3.down * Time.deltaTime * speed);

        if (transform.eulerAngles.y >= 160)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
        if (transform.eulerAngles.y <= 80)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * speed);
        }*/



    }

   
}
