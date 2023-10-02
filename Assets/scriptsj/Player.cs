using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rb.velocity = (transform.right * x + transform.forward * z) * speed + transform.up * rb.velocity.y;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("boinj");
            rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
        }
    }
}
