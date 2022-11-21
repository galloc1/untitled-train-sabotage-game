using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float horizontalSensitivity, verticalSensitivity;
    public float speed;

    float xRotation, yRotation;

    //A Transform representing the 'head' of the player (user's viewpoint and associated objects)
    public Transform head;

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
        Cursor.visible = false;
    }
    private void Move()
    {
        Vector3 movementDirection = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += head.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection -= head.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection -= head.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += head.right;
        }
        movementDirection.Normalize();
        rb.velocity = movementDirection*speed;
    }
    public void Rotate()
    {
        //adding mouse movement to pitch and yaw
        xRotation -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        yRotation += Input.GetAxis("Mouse X") * horizontalSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 80);

        //clamps rotation on the z axis to zero; consequently, the player can look only up/down and left/right
        Quaternion q = head.rotation;
        q.eulerAngles = new Vector3(xRotation, yRotation, 0);
        head.rotation = q;
    }
}
