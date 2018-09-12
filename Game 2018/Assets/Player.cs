using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 5f;
    public float LookSensitivityX;
    public float LookSensitivityY;

    public Camera PlayerCamera;
    private Rigidbody rigidbody;

    private float tempFloat;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerRotation();
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }


    void FixedUpdate()
    {
        PlayerMovement();
    }



    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 movement = rigidbody.position + PlayerCamera.transform.transform.forward * Speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(movement);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 movement = rigidbody.position + PlayerCamera.transform.transform.forward * -1 * Speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(movement);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 movement = rigidbody.position + PlayerCamera.transform.transform.right * Speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(movement);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 movement = rigidbody.position + PlayerCamera.transform.transform.right * -1 * Speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(movement);
        }
    }

    private void PlayerRotation()
    {

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");


       /* Vector3 bodyRotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * LookSensitivityX;
        transform.localEulerAngles += bodyRotation;

        Vector3 cameraRotation = new Vector3(Input.GetAxisRaw("Mouse Y") * -1, 0, 0) * LookSensitivityY;

        PlayerCamera.transform.localEulerAngles += cameraRotation;
        PlayerCamera.transform.localEulerAngles = new Vector3(PlayerCamera.transform.localEulerAngles.x, PlayerCamera.transform.localEulerAngles.y, 0);

        PlayerCamera.transform.Rotate()*/
    }

    private void Fire()
    {
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward,out hit))
        {
            hit.collider.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
        }        
    }
}
