using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using cakeslice;

public class PlayerController : MonoBehaviour
{
    //Player control
    public float horizontalSensitivity, verticalSensitivity;
    public float speed;

    private float xRotation, yRotation;

    //Game flow variables
    private bool inAGame;
    private bool readyForNewCarriage;

    //
    private Component outline;

    //Public references
    public Transform head;

    public GameObject carriage;
    public GameObject popup;
    public GameObject securityCamera;

    //World elements
    private GameObject carriageInstance;
    private GameObject popupInstance;
    private GameObject securityCameraInstance;

    //Rigidbody attached to the player
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inAGame = false;
        readyForNewCarriage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyForNewCarriage)
        {
            GenerateCarriage();
        }
        if (!inAGame)
        {
            Move();
            Rotate();
            Cursor.visible = false;
        }
    }

    //Instantiates a subway carriage with different minigames
    private void GenerateCarriage()
    {
        carriageInstance = Instantiate(carriage);
        if(Random.Range(0, 1) == 0)
        {
            securityCameraInstance = Instantiate(securityCamera);
        }
        readyForNewCarriage = false;
    }
    //Player main movement
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
    //Player main rotation
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
            popupInstance = Instantiate(popup);
            outline = other.gameObject.transform.parent.gameObject.AddComponent<Outline>();
            other.gameObject.transform.parent.gameObject.GetComponent<Outline>().color = 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.tag == "CameraInteractionZone")
            {
                inAGame = true;
                Cursor.visible = true;
                Destroy(popupInstance);
                rb.velocity = Vector3.zero;

                Transform cameraButtons = securityCameraInstance.transform.GetChild(0);
                cameraButtons.GetComponent<ButtonsMinigame>().addingToSequence = true;
                transform.GetChild(1).GetChild(1).position = cameraButtons.position - (cameraButtons.transform.forward * 0.35f);
                transform.GetChild(1).GetChild(1).LookAt(cameraButtons.position);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(popupInstance);
        Destroy(outline);
    }
}
