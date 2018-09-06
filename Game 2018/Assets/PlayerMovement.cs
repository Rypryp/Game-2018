using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed = 5f;
    public float LookSensitivityX;
    public float LookSensitivityY;

    public Camera PlayerCamera;

    private Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 bodyRotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * LookSensitivityX;
        transform.localEulerAngles += bodyRotation;


        Vector3 cameraRotation = new Vector3(Input.GetAxisRaw("Mouse Y") * -1, 0, 0) * LookSensitivityY;

        print((PlayerCamera.transform.localEulerAngles.x + cameraRotation.x));

        PlayerCamera.transform.localEulerAngles += cameraRotation;

      /*  if (PlayerCamera.transform.localEulerAngles.x + cameraRotation.x <= 90 && PlayerCamera.transform.localEulerAngles.x + cameraRotation.x >= -90)
        {
        }*/
    }


    void FixedUpdate()
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
}
