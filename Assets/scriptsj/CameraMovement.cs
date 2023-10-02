using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSentitivity = 2;
    private float verticalRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X") * mouseSentitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSentitivity;

            verticalRotation += mouseY;
            //verticalRotation = Mathf.Clamp(verticalRotation, -30, -150);
            transform.localEulerAngles = Vector3.right * -1 * verticalRotation;

            playerTransform.Rotate(Vector3.up * mouseX);
        }
    }
}
