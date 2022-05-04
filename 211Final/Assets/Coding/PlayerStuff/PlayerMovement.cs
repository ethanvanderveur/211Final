using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameController gameController;

    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.00f;
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

    public float jumpCheckTimer = 0;
    public float jumpCheckMax = 1;

    public GameObject flipOverlay;

    public AudioSource jumpAudioSource;
    public AudioSource landAudioSource;
    public AudioSource stepAudioSource;
    public AudioSource flipAudioSource;
    public AudioSource checkpointAudioSource;
    public AudioSource lavaAudioSource;
    public AudioSource pOneMusic, pTwoMusic, pThreeMusic, pFourMusic, pFiveMusic;

    public Rigidbody camBody;
    public Transform camTran;
    private Quaternion storedRotation;
    public bool wasRot = false;

    //public Animator animator;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        playerCharacter = GameObject.FindGameObjectWithTag("PlayerCharacter");
        gravityGunStatus = playerCharacter.GetComponent<GravityGunStatus>();
        cam = GameObject.Find("PlayerCamera");
        flipOverlay = GameObject.Find("FlipOverlay");
        //GetComponent<Rigidbody>().velocity.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(camTran.rotation);
        if (Input.GetKeyDown(KeyCode.Q) && GravityGunStatus.hasGravitySuit && !hasFlipped)
        {
            hasFlipped = true;
            if (gravMode == 0)
            {
                
                //Debug.Log(camTran.rotation);
                storedRotation = camTran.rotation;
                playerCharacter.transform.Rotate(new Vector3(0, 0, 180));
                cam.transform.Rotate(new Vector3(0, 0, 180));
                velocity.y = 3f;
                //cam.transform.Translate(new Vector3(0, 1.2f, 0));
                StartCoroutine(FlipCam(cam, true));
                gravMode = 1;
                camTran.rotation = storedRotation;
                flipAudioSource.Play();
                //camTran.rotation = Quaternion.Euler(90, 0, 0);
            }
            else
            {
                
                //Debug.Log(camTran.rotation);
                storedRotation = camTran.rotation;
                playerCharacter.transform.Rotate(new Vector3(0, 0, 180));
                cam.transform.Rotate(new Vector3(0, 0, 180));
                velocity.y = -3f;
                //cam.transform.Translate(new Vector3(0, -1.2f, 0));
                StartCoroutine(FlipCam(cam, false));
                gravMode = 0;
                camTran.rotation = storedRotation;
                flipAudioSource.Play();
                //camTran.rotation = Quaternion.Euler(-90, 0, 0);
            }
        }

        if(hasFlipped){
            flipOverlay.SetActive(true);
        } else {
            flipOverlay.SetActive(false);
        }

        if (jumpCheckTimer > 0)
        {
            jumpCheckTimer -= Time.deltaTime;
            isGrounded = false;
        }
        if (jumpCheckTimer <= 0)
        {
            groundCheck.gameObject.SetActive(true);
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        if (!isGrounded && Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            landAudioSource.Play();
            //animator.SetTrigger("landing");
        }

        if (isGrounded && hasFlipped)
        {
            hasFlipped = false;
        }

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
                groundCheck.gameObject.SetActive(false);
                jumpCheckTimer = jumpCheckMax;
            }
            else if (gravMode == 1)
            {
                jumpAudioSource.Play();
                velocity.y = -Mathf.Sqrt(jumpHeight * gravity);
                groundCheck.gameObject.SetActive(false);
                jumpCheckTimer = jumpCheckMax;
            }
            //animator.SetTrigger("jumping");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(wasRot && !isRotatingCamera)
        {
            wasRot = false;
            //camTran.rotation = storedRotation;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Planet1") && !pOneMusic.isPlaying)
        {
            pOneMusic.Play();
        }
        else
        {
            pOneMusic.Stop();
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Planet2") && !pTwoMusic.isPlaying)
        {
            pTwoMusic.Play();
        }
        else
        {
            pTwoMusic.Stop();
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Planet3") && !pThreeMusic.isPlaying)
        {
            pThreeMusic.Play();
        }
        else
        {
            pThreeMusic.Stop();
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Planet4") && !pFourMusic.isPlaying)
        {
            pFourMusic.Play();
        }
        else
        {
            pFourMusic.Stop();
        }


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Planet5") && !lavaAudioSource.isPlaying)
        {
            pFiveMusic.Play();
            lavaAudioSource.Play();
        } else
        {
            pFiveMusic.Play();
            lavaAudioSource.Stop();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            gameController.hitCheckPoint(other.gameObject);
            checkpointAudioSource.Play();
        }
        else if (other.tag == "DeathTrigger")
        {
            gameController.playerDeath();
        }
        else if (other.tag == "DeathTag")
        {
            gameController.playerDeath();
            Debug.Log("!");
        }
    }

    IEnumerator FlipCam(GameObject cam, bool positive)
    {
        
        isRotatingCamera = true;
        wasRot = true;
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
            
            //camTran.rotation = storedRotation;
            yield return new WaitForSeconds(.002f);
            
            //Debug.Log("rotation: " + camTran.rotation);
        }
        
        isRotatingCamera = false;
       // Debug.Log(camTran.rotation);
        yield return null;
        //camTran.rotation = Quaternion.Euler(-camTran.rotation.x, 0, 0);
        //camTran.rotation = storedRotation;
    }
}
