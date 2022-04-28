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

    GravityGunStatus gravityGunStatus;
    GameObject cam;

    public float speed = 6f;
    public float jumpHeight = 4.3f;
    public float gravity;
    public int gravMode = 0;
    public float gravityBase = 0;
    public Vector3 velocity;
    public bool wasMoving = false;
    public bool isRotatingCamera = false;
    public bool hasFlipped = false;

    GameObject playerCharacter;

    public AudioSource jumpAudioSource;
    public AudioSource landAudioSource;
    public AudioSource stepAudioSource;
    public AudioSource flipAudioSource;

    //public Animator animator;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        playerCharacter = GameObject.FindGameObjectWithTag("PlayerCharacter");
        gravityGunStatus = playerCharacter.GetComponent<GravityGunStatus>();
        cam = GameObject.Find("PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GravityGunStatus.hasGravitySuit && !hasFlipped)
        {
            hasFlipped = true;
            if (gravMode == 0)
            {
                playerCharacter.transform.Rotate(new Vector3(0, 0, 180));
                cam.transform.Rotate(new Vector3(0, 0, 180));
                velocity.y = 3f;
                //cam.transform.Translate(new Vector3(0, 1.2f, 0));
                StartCoroutine(FlipCam(cam, true));
                gravMode = 1;
                flipAudioSource.Play();

            }
            else
            {
                playerCharacter.transform.Rotate(new Vector3(0, 0, 180));
                cam.transform.Rotate(new Vector3(0, 0, 180));
                velocity.y = -3f;
                //cam.transform.Translate(new Vector3(0, -1.2f, 0));
                StartCoroutine(FlipCam(cam, false));
                gravMode = 0;
                flipAudioSource.Play();
            }
        }

        if (!isGrounded && Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            landAudioSource.Play();
            //animator.SetTrigger("landing");
            hasFlipped = false;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (gravMode == 0)
        {
            gravity = gravityBase;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        else if (gravMode == 1)
        {
            gravity = -1 * gravityBase;
            if (isGrounded && velocity.y > 0)
            {
                velocity.y = 2f;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if ((x != 0 || z != 0) && isGrounded && !stepAudioSource.isPlaying)
        {
            stepAudioSource.Play();
        }

        if ((x != 0 || z != 0) && isGrounded)
        {
            wasMoving = true;
            //animator.SetTrigger("walking");
        }

        if (x == 0 && z == 0 && wasMoving)
        {
            wasMoving = false;
            //animator.SetTrigger("stop");
        }

        if (((x == 0 && z == 0) || !isGrounded || PauseMenu.GameIsPaused) && stepAudioSource.isPlaying)
        {
            stepAudioSource.Stop();
        }

        Vector3 move = transform.right * x + transform.forward * z;
        //velocity.x = transform.right.x * x * speed;
        // velocity.z = transform.forward.z *z * speed;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (gravMode == 0)
            {
                jumpAudioSource.Play();
                velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
            }
            else if (gravMode == 1)
            {
                jumpAudioSource.Play();
                velocity.y = -Mathf.Sqrt(jumpHeight * gravity);//this one may need some tuning, not sure if -2 or jumpheight need to be negative
            }
            //animator.SetTrigger("jumping");
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
        else if (other.tag == "DeathTrigger")
        {
            gameController.playerDeath();
        }
    }

    IEnumerator FlipCam(GameObject cam, bool positive)
    {
        isRotatingCamera = true;
        for (int i = 0; i < 180; i++)
        {

            if (positive)
            {
                cam.transform.Rotate(0, 0, 1);
                //cam.transform.Translate(new Vector3(0, -1.2f / 180, 0));
            }
            else
            {
                cam.transform.Rotate(0, 0, 1);
                //cam.transform.Translate(new Vector3(0, 1.2f/180, 0));
            }
            yield return new WaitForSeconds(.002f);
        }
        isRotatingCamera = false;
        yield return null;
    }
}
