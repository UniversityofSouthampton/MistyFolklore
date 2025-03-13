using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed  = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var characterController = GetComponent<CharacterController>();
        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += transform.right;
        }
        
        characterController.SimpleMove(velocity * speed);
    }

   
}
