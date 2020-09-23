using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDCameraControler : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0, 50, 0);
        transform.eulerAngles = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveCamera();
    }


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
            transform.position += this.transform.up * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= this.transform.up * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= this.transform.right * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            transform.position += this.transform.right * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("e"))
        {
            transform.position -= this.transform.forward * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("q"))
        {
            transform.position += this.transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("z"))
        {


            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y + 20 * Time.deltaTime, 0);
        }
        else if (Input.GetKey("x"))
        {
            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y - 20 * Time.deltaTime, 0);
        }



    }
}
