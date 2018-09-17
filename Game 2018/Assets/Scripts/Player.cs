using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MovementSpeed = 5f;
    public float JumpForce = 2f;
    public float MouseSensitivityX = 5f;
    public float MouseSensitivityY = 5f;

    private Transform playerCamera;
    private Rigidbody rigidbody;
    private Collider collider;

    private bool isGrounded;
    private float xAxisClamp = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>().transform;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        PlayerRotation();

        PlayerInputs();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }
    
    private void PlayerInputs()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(Vector3.up * Mathf.Sqrt(JumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
        //print(Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x, collider.bounds.min.y - 1f, collider.bounds.center.z), 0.5f));
    }

    private void PlayerMovement()
    {
        isGrounded = rigidbody.velocity.y == 0 ? true : false;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * horizontal) + (transform.forward * vertical);
        
        rigidbody.MovePosition(transform.position + movement * MovementSpeed * Time.deltaTime);

    }

    private void PlayerRotation()
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
