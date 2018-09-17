using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    public Gun CurrentGun;

    public List<Gun> MyGuns;

    private Transform playerCamera;

	void Start ()
    {
        playerCamera = GetComponentInParent<Camera>().transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire(CurrentGun.Damage);
        }
    }

    private void Fire(int damage)
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            hit.collider.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
