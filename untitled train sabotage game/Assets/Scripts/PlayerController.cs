using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float horizontalSensitivity, verticalSensitivity;
    public float speed;
    public float inputBuffer;
    public float shotCooldownLength;
    public float projectileSpeed;

    private float xRotation, yRotation;
    private float cooldownTimer;

    private bool shotQueued;

    //A Transform representing the 'head' of the player (user's viewpoint and associated objects)
    public Transform head;
    public Transform gun;

    public GameObject projectile;

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
        FireWeapon();
        Cursor.visible = false;
    }
    private void FireWeapon()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) || shotQueued)
        {
            if(cooldownTimer > shotCooldownLength)
            {
                shotQueued = false;
                cooldownTimer = 0.0f;
                GameObject projectileInstance = Instantiate(projectile, gun.position, head.rotation);
                projectileInstance.GetComponent<Rigidbody>().velocity = projectileInstance.transform.forward.normalized * projectileSpeed;
            }
            else if(shotCooldownLength-cooldownTimer<inputBuffer)
            {
                shotQueued = true;
            }
        }
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
}
