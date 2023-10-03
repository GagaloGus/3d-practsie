using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed, jumpForce;
    public bool isGrounded;
    public float distance;
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
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        #region raycast

        isGrounded = 
            Physics.Raycast(
                transform.position, 
                Vector3.down, 
                distance, 
                LayerMask.GetMask("Ground"));

        //Debug.DrawRay(transform.position,
        //    Vector3.down * distance, 
        //    Color.yellow);
        #endregion
    }

    private void OnTriggerEnter(Collider triggr)
    {
        if (triggr.GetComponent<RainbowColorChange>())
        {
            int score = triggr.GetComponent<RainbowColorChange>().score;
            GameManager.instance.UpdateScore(score);
            Destroy(triggr.gameObject);
        }
    }


}
