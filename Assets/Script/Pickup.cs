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
            startTime = Time.time;
            startPos = this.transform.position;
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

        if (Input.GetMouseButtonDown(1))
        {
            //throw
        }

    }

    private void Drop()
    {
        if (isHolding)
        {
            endTime = Time.time;
            timeInterval = endTime - startTime;
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            endPos = this.transform.position;
            rb.useGravity = true;
            rb.AddForce ((endPos-startPos).normalized * throwForce/timeInterval, ForceMode.Impulse);
        }
    }
}
