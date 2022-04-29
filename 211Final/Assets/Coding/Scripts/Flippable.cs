using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippable : MonoBehaviour
{
    [SerializeField] private const float FLIP_COOLDOWN_SECONDS = 1f;
    [SerializeField] private const float TIME_SLOW_DURATION_SECONDS = 5f;
    [SerializeField] private const float TIME_SLOW_MULTIPLIER = .1f;

    [SerializeField]
    public Vector3 gravityMultiplier = new Vector3(0, -1, 0);

    Rigidbody rb;
    GravityGunStatus gravityGunStatus;
    Outline outline;

    Vector3 tempVelocityStorage;
    GravityGunStatus.GravityAxis tempAxisStorage;

    public bool hovered, slowed;

    public float flipTimer;
    public float slowTimer;

    public AudioSource gunAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        gravityGunStatus = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<GravityGunStatus>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 8f;
        flipTimer = 0f;
        slowTimer = 0f;
        hovered = false;
        slowed = false;

        gunAudioSource = GameObject.Find("GunAudio").GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
        hovered = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
        gravityGunStatus.XLine.SetActive(false);
        gravityGunStatus.YLine.SetActive(false);
        gravityGunStatus.ZLine.SetActive(false);
        hovered = false;
    }

    // Handles right click
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && flipTimer <= 0)
        {
            switch (gravityGunStatus.gravityAxis)
            {
                case GravityGunStatus.GravityAxis.xAxis:
                    if (gravityMultiplier.x != -1)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                        flipTimer = FLIP_COOLDOWN_SECONDS;
                        gunAudioSource.Play();
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.x = -1;
                    }
                    break;
                case GravityGunStatus.GravityAxis.yAxis:
                    if (gravityMultiplier.y != -1)
                    {
                        rb.velocity = new Vector3(0, rb.velocity.y, 0);
                        flipTimer = FLIP_COOLDOWN_SECONDS;
                        gunAudioSource.Play();
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.y = -1;
                    }
                    break;
                case GravityGunStatus.GravityAxis.zAxis:
                    if (gravityMultiplier.z != -1)
                    {
                        rb.velocity = new Vector3(0, 0, rb.velocity.z);
                        flipTimer = FLIP_COOLDOWN_SECONDS;
                        gunAudioSource.Play();
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.z = -1;
                    }
                    break;
            }
        }
    }

    // Handles left click
    private void OnMouseDown()
    {
        if (flipTimer <= 0)
        {
            switch (gravityGunStatus.gravityAxis)
            {
                case GravityGunStatus.GravityAxis.xAxis:

                    if (gravityMultiplier.x != 1)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                        flipTimer = FLIP_COOLDOWN_SECONDS;
                        gunAudioSource.Play();
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.x = 1;
                    }
                    break;
                case GravityGunStatus.GravityAxis.yAxis:
                    if (gravityMultiplier.y != 1)
                    {
                        rb.velocity = new Vector3(0, rb.velocity.y, 0);
                        flipTimer = FLIP_COOLDOWN_SECONDS;
                        gunAudioSource.Play();
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.y = 1;
                    }
                    break;
                case GravityGunStatus.GravityAxis.zAxis:
                    if (gravityMultiplier.z != 1)
                    {
                        rb.velocity = new Vector3(0, 0, rb.velocity.z);
                        flipTimer = FLIP_COOLDOWN_SECONDS;
                        gunAudioSource.Play();
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.z = 1;
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Flip Timer
        if (flipTimer > 0)
        {
            flipTimer -= Time.deltaTime;
        }

        // For Time Slowing
        if (Input.GetKeyDown(KeyCode.F) && GravityGunStatus.hasTimeSlow && gravityGunStatus.timeSlowCooldown <= 0 && hovered)
        {
            tempVelocityStorage = rb.velocity;
            tempAxisStorage = gravityGunStatus.gravityAxis;
            rb.velocity = Vector3.zero;
            gravityGunStatus.slowedAnObject = true;
            slowed = true;
            slowTimer = TIME_SLOW_DURATION_SECONDS;
            
        }

        // Time slow timer
        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
        } else if (slowed)
        {
            if (gravityGunStatus.gravityAxis == tempAxisStorage)
            {
                rb.velocity = tempVelocityStorage;
            }
            slowed = false;
        }

        // For swapping color and moving demo axis
        switch (gravityGunStatus.gravityAxis)
        {
            case GravityGunStatus.GravityAxis.xAxis:
                outline.OutlineColor = Color.green;
                if (hovered)
                {
                    gravityGunStatus.XLine.SetActive(true);
                    gravityGunStatus.YLine.SetActive(false);
                    gravityGunStatus.ZLine.SetActive(false);
                    gravityGunStatus.XLine.transform.position = rb.position;
                }
                break;
            case GravityGunStatus.GravityAxis.yAxis:
                outline.OutlineColor = Color.red;
                if (hovered)
                {
                    gravityGunStatus.XLine.SetActive(false);
                    gravityGunStatus.YLine.SetActive(true);
                    gravityGunStatus.ZLine.SetActive(false);
                    gravityGunStatus.YLine.transform.position = rb.position;
                }
                break;
            case GravityGunStatus.GravityAxis.zAxis:
                outline.OutlineColor = Color.blue;
                if (hovered)
                {
                    gravityGunStatus.XLine.SetActive(false);
                    gravityGunStatus.YLine.SetActive(false);
                    gravityGunStatus.ZLine.SetActive(true);
                    gravityGunStatus.ZLine.transform.position = rb.position;
                }
                break;
        }

        // For gravity affecting stuff
        switch (gravityMultiplier.x)
        {
            case 0:
                break;
            default:
                if (slowed)
                {
                    rb.AddForce(new Vector3(Physics.gravity.y * -1 * gravityMultiplier.x * TIME_SLOW_MULTIPLIER, 0, 0) * rb.mass);
                }
                else
                {
                    rb.AddForce(new Vector3(Physics.gravity.y * -1 * gravityMultiplier.x, 0, 0) * rb.mass);
                }
                break;
        }
        switch (gravityMultiplier.y)
        {
            case 0:
                break;
            default:
                if (slowed)
                {
                    rb.AddForce(new Vector3(0, Physics.gravity.y * -1 * gravityMultiplier.y * TIME_SLOW_MULTIPLIER, 0) * rb.mass);
                }
                else
                {
                    rb.AddForce(new Vector3(0, Physics.gravity.y * -1 * gravityMultiplier.y, 0) * rb.mass);
                }
                break;
        }
        switch (gravityMultiplier.z)
        {
            case 0:
                break;
            default:
                if (slowed)
                {
                    rb.AddForce(new Vector3(0, 0, Physics.gravity.y * -1 * gravityMultiplier.z * TIME_SLOW_MULTIPLIER) * rb.mass);
                }
                else
                {
                    rb.AddForce(new Vector3(0, 0, Physics.gravity.y * -1 * gravityMultiplier.z) * rb.mass);
                }
                break;
        }

        //moving player on top of it

    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "groundCheck" && !Input.GetButtonDown("Jump"))
        {
            Debug.Log("check hit");
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().velocity.y = rb.velocity.y;
        }
    }
}
