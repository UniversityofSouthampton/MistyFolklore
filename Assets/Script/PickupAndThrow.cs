using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndThrow : MonoBehaviour
{
    public static bool isHolding = false;
    bool pickup;

    [SerializeField] private float throwForce = 600f;

    [SerializeField] private float maxDistance = 3f;

    private float distance;

    private TempParent tempParent;

    private Rigidbody rb;
    RaycastHit hit;
    public LayerMask obj_mask;

    private Vector3 startPos ,endPos, objectPos;
    //float rotationSensitivity = 5f;

    public GameObject player;
    private GameObject pickup_obj;
    private float startTime, endTime, timeInterval;

    private MeshRenderer mr;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       
        tempParent = TempParent.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            Hold();
        }
        
        if (pickup)
        {
            PickingUp();
        }
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, obj_mask) )
        {
            mr = hit.collider.gameObject.GetComponent<MeshRenderer>();
            mr.materials[1].SetFloat("_alpha", 1);
            if( Input.GetMouseButtonDown(0))
            {   
                rb = hit.rigidbody;
                pickup_obj = hit.collider.gameObject;
                isHolding = true;
                pickup = true;
                
            }
            else if( Input.GetMouseButtonUp(0))
            {
                mr.materials[1].SetFloat("_alpha", 0);
                Drop();
            }
            
        }
        else if (Input.GetMouseButtonUp(0)){
            
            Drop();
        }
        else if(isHolding == false)
        {
            mr.materials[1].SetFloat("_alpha", 0);
        }
        
    }

    void PickingUp()
    {
         rb.useGravity = false;
         rb.detectCollisions = true;
         pickup_obj.transform.SetParent(tempParent.transform);
         //Hold();
    }
    /*
    public void OnMouseDown()
    {
        //pickup
        if (tempParent.gameObject != null)
        {

            if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, maxDistance, obj_mask))
            {
                isHolding = true;
                Rigidbody rb = hit.rigidbody;
                rb.useGravity = false;
                rb.detectCollisions = true;
                hit.transform.SetParent(tempParent.transform);
            }
            
           
        }
        else
        {
            Debug.Log("Temp Parent item not found in scene");
        }
    }*/

    //public void OnMouseUp()
    //{
        //drop
        //Drop();
    //}

   // private void OnMouseExit()
    //{
        //drop
    //}

    private void Hold()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        mr.materials[1].SetFloat("_alpha", 1);
        if (Input.GetKey(KeyCode.E))
        {
            float XaxisRotation = 1f;
            //float YaxisRotation = 5f;
            pickup_obj.transform.Rotate(Vector3.right , XaxisRotation);
            //transform.Rotate(Vector3.right, YaxisRotation);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            //float XaxisRotation = 1f;
            float YaxisRotation = 1f;
            //transform.Rotate(Vector3.down , XaxisRotation);
            pickup_obj.transform.Rotate(-Vector3.right, YaxisRotation);
        }
        if (Input.GetKey(KeyCode.X))
        {
            float ZaxisRotation = 1f;
            pickup_obj.transform.Rotate(Vector3.forward, ZaxisRotation);
        }
        anim = pickup_obj.GetComponent<Animator>();
        if (anim == null)
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.speed = 1 / Time.timeScale;
                anim.enabled = true;
                StartCoroutine(Delay());
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            //throw
            Drop();
            rb.AddForce(tempParent.transform.forward * throwForce * 1/Time.timeScale, ForceMode.Force);
            
        }

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.2f);
        anim.enabled = false;
        pickup_obj.transform.localPosition = new Vector3(0,0,3);
    }
        
    

    private void Drop()
    {
       if (isHolding)
       {
           
           pickup = false;
           isHolding = false;
           objectPos = pickup_obj.transform.position;
           pickup_obj.transform.position = objectPos;
           pickup_obj.transform.SetParent(null);
           rb.useGravity = true;
           mr.materials[1].SetFloat("_alpha", 0);
           anim.enabled = false;
       }
           
            
        
    }
}
