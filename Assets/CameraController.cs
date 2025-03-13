using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camearObject;

    private Vector2 rotation;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x -= Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -80, 80);

        transform.eulerAngles = new Vector2(0, rotation.y);
        camearObject.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
    }
}
