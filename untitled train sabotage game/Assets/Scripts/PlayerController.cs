using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float pitch;
    public float yaw;
    public float xaxis;
    public float yaxis;
    public float mouseSensitivity;

    //Rigidbody attached to the player
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * 3;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * 3;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * 3;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * 3;
        }
    }
    public void Rotate()
    {
        //stores initial pitch, yaw, and roll of the player
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
        float roll = 0;

        xaxis = Input.GetAxis("Mouse X");
        yaxis = Input.GetAxis("Mouse Y");
        //adds mouse movement to pitch and yaw
        yaw +=Input.GetAxis("Mouse X")*mouseSensitivity;
        pitch+=Input.GetAxis("Mouse Y")*mouseSensitivity;

        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
