using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// base camera movement script from https://www.youtube.com/watch?v=rxa4N4z65pg
public class CameraController : MonoBehaviour
{
    public float sensitivity;
    public float slowSpeed;
    public float normalSpeed;
    public float sprintSpeed;
    float currentSpeed;

    private void Start(){
        Camera cam = GetComponent<Camera>();
        // cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
    }


    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Movement();
            Rotation();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    public void Rotation()
    {
        Vector3 mouseInput = new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);
        transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public void Movement(){
        float verticalSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if(Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.Space)) {
            verticalSpeed = 1;
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            verticalSpeed = -1;
        }
        else {
            verticalSpeed = 0;
        }
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(input * currentSpeed * Time.deltaTime, Space.Self);
        transform.Translate(new Vector3(0f, verticalSpeed, 0f) * currentSpeed * Time.deltaTime, Space.World);
    }
}
