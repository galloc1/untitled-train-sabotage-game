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
    private Quaternion rotationDefault;

    //Game flow variables
    private bool inAGame;
    private bool readyForNewCarriage;
    private bool waitingForPlayerToExitCarriage;

    //Outline
    private Component outline;

    //Public references
    public Transform head;

    public GameObject popup;


    private GameObject popupInstance;

    //Rigidbody attached to the player
    Rigidbody rb;

    //Train carriage & minigame GameObjects
    //  Public references
    public GameObject carriage;
    public List<GameObject> carriageElements = new List<GameObject>();

    //  World elements
    private List<GameObject> carriageElementsInstances = new List<GameObject>();
    private List<GameObject> previousCarriageElements = new List<GameObject>();
    private List<bool> completionStatus = new List<bool>();


    // Start is called before the first frame update
    void Start()
    {
        rotationDefault = transform.Find("/Player/Head/Main Camera").localRotation;
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
        else
        {
            FinishCarriage();
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
        completionStatus = new List<bool>();
        carriageElementsInstances.Add(Instantiate(carriage));
        AddMinigames();
        readyForNewCarriage = false;
    }

    //For when the player finishes a carriage
    private void FinishCarriage()
    {
        if (!completionStatus.Contains(true))
        {
            Debug.Log("rhut rho");
            previousCarriageElements = carriageElementsInstances;
            transform.position -= new Vector3(29.0f, 0);
            for (int i = 0; i < previousCarriageElements.Count; i++)
            {
                previousCarriageElements[i].transform.position -= new Vector3(29.0f, 0.0f);
            }
            readyForNewCarriage = true;
        }
    }

    //Adds each minigame to the carriage
    private void AddMinigames()
    {
        for (int i = 0; i < carriageElements.Count; i++)
        {
            if (Random.Range(0, 1) == 0)
            {
                carriageElementsInstances.Add(Instantiate(carriageElements[i]));
                completionStatus.Add(true);
            }
            else
            {
                carriageElementsInstances.Add(null);
                completionStatus.Add(false);
            }
        }
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
            //If starting the Camera minigame
            if (other.gameObject.tag == "CameraInteractionZone")
            {
                inAGame = true;
                Cursor.visible = true;
                Destroy(popupInstance);
                rb.velocity = Vector3.zero;

                Transform cameraButtons = carriageElementsInstances[1].transform.Find("/Security Camera(Clone)/Camera Buttons");
                Debug.Log(cameraButtons);
                cameraButtons.GetComponent<ButtonsMinigame>().addingToSequence = true;
                transform.Find("/Player/Head/Main Camera").position = cameraButtons.position - (cameraButtons.transform.forward * 0.12f);
                transform.Find("/Player/Head/Main Camera").LookAt(cameraButtons.position);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(popupInstance);
        Destroy(outline);
    }
    public void ExitGame(GameObject obj)
    {
        inAGame = false;
        Cursor.visible = false;
        Debug.Log(carriageElementsInstances.IndexOf(obj));
        completionStatus[carriageElementsInstances.IndexOf(obj) - 1] = false;
        transform.Find("/Player/Head/Main Camera").localRotation = rotationDefault;
        transform.Find("/Player/Head/Main Camera").localPosition = new Vector3(0.0f, 0.6f, 0.0f);
    }
}
