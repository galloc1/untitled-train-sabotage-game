using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using cakeslice;

public class PlayerController : MonoBehaviour
{
    public float horizontalSensitivity, verticalSensitivity;
    public float speed;

    private float xRotation, yRotation;

    private GameObject currentPopup;

    private Component comp;

    //A Transform representing the 'head' of the player (user's viewpoint and associated objects)
    public Transform head;

    public GameObject projectile;
    public GameObject interactionPopup;
    public GameObject cameraMinigame;

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
        movementDirection.y = 0;
        rb.velocity = movementDirection*speed;
    }
    private void Rotate()
    {
        //adding mouse movement to pitch and yaw
        xRotation -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        yRotation += Input.GetAxis("Mouse X") * horizontalSensitivity;

        //clamps player to be able to look straight up and nearly straight down
        xRotation = Mathf.Clamp(xRotation, -90, 80);

        //clamps rotation on the z axis to zero; this prevents the player from tilting to the side
        Quaternion q = head.rotation;
        q.eulerAngles = new Vector3(xRotation, yRotation, 0);
        head.rotation = q;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraInteractionZone")
        {
            currentPopup = Instantiate(interactionPopup);
            comp = other.gameObject.transform.parent.gameObject.AddComponent<Outline>();
            other.gameObject.transform.parent.gameObject.GetComponent<Outline>().color = 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.tag == "CameraInteractionZone")
            {
                Destroy(currentPopup);
                currentPopup = Instantiate(cameraMinigame);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(currentPopup);
        currentPopup = Instantiate(interactionPopup);
        Destroy(comp);
    }
}
