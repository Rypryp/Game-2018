using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MovementSpeed = 5f;
    public float JumpForce = 2f;
    public float MouseSensitivityX = 5f;
    public float MouseSensitivityY = 5f;

    private Transform playerCamera;
    private Rigidbody playerRigidbody;
    private CapsuleCollider capsuleCollider;

    private bool isGrounded;
    private float currentMovementSpeed;
    private float xAxisClamp = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>().transform;
        playerRigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Rotation();

        Inputs();
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    private void Inputs()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(Physics.Raycast(transform.position, Vector3.down, capsuleCollider.height / 2))
            {
                playerRigidbody.AddForce(Vector3.up * Mathf.Sqrt(JumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);

            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMovementSpeed = MovementSpeed * 2;
        }
        else
        {
            currentMovementSpeed = MovementSpeed;
        }
    }

    private void Movement()
    {
        isGrounded = playerRigidbody.velocity.y == 0 ? true : false;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * horizontal) + (transform.forward * vertical);

        playerRigidbody.MovePosition(transform.position + movement * currentMovementSpeed * Time.deltaTime);

    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationX = mouseX * MouseSensitivityX;
        float rotationY = mouseY * MouseSensitivityY;

        xAxisClamp -= rotationY;

        Vector3 cameraRotation = playerCamera.localEulerAngles;
        Vector3 bodyRotation = transform.eulerAngles;

        cameraRotation.x -= rotationY;
        cameraRotation.z = 0;
        bodyRotation.y += rotationX;

        if(xAxisClamp > 90)
        {
            xAxisClamp = 90;
            cameraRotation.x = 90;
        }
        else if(xAxisClamp < -90)
        {
            xAxisClamp = -90;
            cameraRotation.x = 270;
        }

        transform.rotation = Quaternion.Euler(bodyRotation);
        playerCamera.localRotation = Quaternion.Euler(cameraRotation);
    }
}
