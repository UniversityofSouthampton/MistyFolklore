using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed  = 2;
    public float gravity = -9.81f;
    private bool isGrounded;
    public LayerMask Ground, jump_obj;
    public Transform player;
    float horizontal;
    float vertical;
    Vector3 velocity;
    public float jumpHeight;
    bool timeChange = true;
    public Timeslow timeslow;
    /// Audio
    public AudioSource rain_sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            timeChange = !timeChange;
            if (timeChange == false)
            {
                SlowDownTime();
                
            }
            else {
                NormalTime();
                
            }
        }
        Move();
        if (rain_sound.pitch > 0.1f && timeChange == false)
         {
                 rain_sound.pitch -= 0.01f;
         }
         else if (rain_sound.pitch < 1f && timeChange == true)
         {
             rain_sound.pitch += 0.01f;
         }
    }

    void Move()
    {
       
        isGrounded = Physics.Raycast(player.position, Vector3.down, 0.25f,Ground | jump_obj);
        Debug.Log(isGrounded);
        var characterController = GetComponent<CharacterController>();
        //Vector3 velocity = Vector3.zero;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(movement.normalized * speed * Time.deltaTime * 1/Time.timeScale);
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity );
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        velocity.y += gravity * Time.deltaTime * 1/Time.timeScale;
        
        characterController.Move(velocity * Time.deltaTime * 1/Time.timeScale);
    }

    public void SlowDownTime()
    {
        timeslow.DoSlowmotion();
    }

    public void NormalTime()
    {
        timeslow.TurnBack();
    }

   
}
