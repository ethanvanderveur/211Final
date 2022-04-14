using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameController gameController;

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

    public AudioSource jumpAudioSource;
    public AudioSource landAudioSource;
    public AudioSource stepAudioSource;

    private void Start()
    {
        gameController = GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrounded && Physics.CheckSphere(groundCheck.position, groundDistance, groundMask)){
            landAudioSource.Play();
        }
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

        if((x != 0 || z != 0) && isGrounded && !stepAudioSource.isPlaying){
            stepAudioSource.Play();
        }

        if(((x == 0 && z == 0) || !isGrounded) && stepAudioSource.isPlaying){
            stepAudioSource.Stop();
        }

        Vector3 move = transform.right * x + transform.forward * z;
        //velocity.x = transform.right.x * x * speed;
       // velocity.z = transform.forward.z *z * speed;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            if(gravMode == 0){
                jumpAudioSource.Play();
                velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
            } else if (gravMode == 1){
                jumpAudioSource.Play();
                velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);//this one may need some tuning, not sure if -2 or jumpheight need to be negative
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            gameController.hitCheckPoint(other.gameObject);
        }
    }
}
