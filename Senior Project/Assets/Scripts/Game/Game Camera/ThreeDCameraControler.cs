using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDCameraControler : MonoBehaviour
{

    private float mouseX;
    private float mouseY;

    private float speedH = 2.0f;
    private float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
   

    // Update is called once per frame
    void Update()
    {


        moveCamera();

        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

        handelMouseRotation();

        
    }

    // allows user to move camera around the screen with the keys
    void moveCamera()
    {
        Vector3 currentPostion = transform.position;

        float panSpeed = currentPostion.y / 2;

        if (Input.GetKey("left shift"))
        {
            panSpeed *= 4;
        }

        if (Input.GetKey("w"))
        {
            transform.position += this.transform.forward * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= this.transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= this.transform.right * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            transform.position += this.transform.right * panSpeed * Time.deltaTime;
        }

        





        
    }

    // handles mouse rotation
    void handelMouseRotation()
    {

        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

    }
        

}
