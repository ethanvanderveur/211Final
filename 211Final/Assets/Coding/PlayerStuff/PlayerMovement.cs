using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public float speed = 6f;
    public int gravMode = 0; //0 is down, 1 is up
    public float gravityBase = Physics.gravity.x;
    public float jumpHeight = 4.3f;
    public float gravity;
    public Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(gravMode == 0){
            gravity = gravityBase;
            if(isGrounded && velocity.y < 0){
                velocity.y = -2f;
            }
        } else if (gravMode == 1){
            gravity = -1 * gravityBase;
            if(isGrounded && velocity.y > 0){
                velocity.y = 2f;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        //velocity.x = transform.right.x * x * speed;
       // velocity.z = transform.forward.z *z * speed;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            if(gravMode == 0){
                velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
            } else if (gravMode == 1){
                velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);//this one may need some tuning, not sure if -2 or jumpheight need to be negative
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        // ==================== RESET SCENE ====================
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
