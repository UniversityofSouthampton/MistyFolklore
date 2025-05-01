using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool isHolding = false;

    [SerializeField] private float throwForce = 600f;

    [SerializeField] private float maxDistance = 3f;

    private float distance;

    private TempParent tempParent;

    private Rigidbody rb;

    private Vector3 startPos ,endPos, objectPos;
    float rotationSensitivity = 5f;

  

    private float startTime, endTime, timeInterval;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            Hold();
        }
    }

    public void OnMouseDown()
    {
        //pickup
        if (tempParent.gameObject != null)
        {
            isHolding = true;
            rb.useGravity = false;
            rb.detectCollisions = true;
            this.transform.SetParent(tempParent.transform);
           
        }
        else
        {
            Debug.Log("Temp Parent item not found in scene");
        }
    }

    public void OnMouseUp()
    {
        //drop
        Drop();
    }

    private void OnMouseExit()
    {
        //drop
    }

    private void Hold()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.F))
        {
            float XaxisRotation = 1f;
            //float YaxisRotation = 5f;
            transform.Rotate(Vector3.down , XaxisRotation);
            //transform.Rotate(Vector3.right, YaxisRotation);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            //float XaxisRotation = 1f;
            float YaxisRotation = 1f;
            //transform.Rotate(Vector3.down , XaxisRotation);
            transform.Rotate(Vector3.right, YaxisRotation);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //throw
            Drop();
            rb.AddForce(tempParent.transform.forward * throwForce, ForceMode.Force);
            
        }

    }

    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity = true;
            
        }
    }
}
