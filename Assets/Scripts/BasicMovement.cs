using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }

        Camera cam = Camera.main;
        // Converting the mouse position to a point in 3D-space
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        // Using some math to calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
        float t = cam.transform.position.y / (cam.transform.position.y - point.y);
        Vector3 finalPoint = new Vector3(t*(point.x - cam.transform.position.x) + cam.transform.position.x, transform.position.y, t*(point.z - cam.transform.position.z) + cam.transform.position.z);
        //Rotating the object to that point
        transform.LookAt(finalPoint, Vector3.up);


    }
}
