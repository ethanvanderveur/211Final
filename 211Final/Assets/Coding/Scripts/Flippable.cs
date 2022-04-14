using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippable : MonoBehaviour
{
    private const int FLIP_TIME = 500;

    [SerializeField]
    public Vector3 gravityMultiplier;

    Rigidbody rb;
    GravityGunStatus gravityGunStatus;
    Outline outline;

    public bool hovered;

    public int flipTimer;

    // Start is called before the first frame update
    void Start()
    {
        gravityMultiplier = new Vector3(0, -1, 0);
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        gravityGunStatus = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<GravityGunStatus>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 8f;
        flipTimer = 0;
        hovered = false;
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
    private void OnMouseDown()
    {
        if (flipTimer == 0)
        {
            switch (gravityGunStatus.gravityAxis)
            {
                case GravityGunStatus.GravityAxis.xAxis:
                    flipTimer = FLIP_TIME;
                    if (gravityMultiplier.x != 0)
                    {
                        gravityMultiplier.x *= -1;
                    }
                    else
                    {
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.x = 1;
                    }
                    break;
                case GravityGunStatus.GravityAxis.yAxis:
                    flipTimer = FLIP_TIME;
                    if (gravityMultiplier.y != 0)
                    {
                        gravityMultiplier.y *= -1;
                    }
                    else
                    {
                        gravityMultiplier = Vector3.zero;
                        gravityMultiplier.y = 1;
                    }
                    break;
                case GravityGunStatus.GravityAxis.zAxis:
                    flipTimer = FLIP_TIME;
                    if (gravityMultiplier.z != 0)
                    {
                        gravityMultiplier.z *= -1;
                    }
                    else
                    {
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
            flipTimer--;
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
                    gravityGunStatus.XLine.transform.rotation = rb.rotation;
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
                rb.AddForce(new Vector3(Physics.gravity.y*-1 * gravityMultiplier.x, 0, 0) * rb.mass);
                break;
        }
        switch (gravityMultiplier.y)
        {
            case 0:
                break;
            default:
                rb.AddForce(new Vector3(0, Physics.gravity.y*-1 * gravityMultiplier.y, 0) * rb.mass);
                break;
        }
        switch (gravityMultiplier.z)
        {
            case 0:
                break;
            default:
                rb.AddForce(new Vector3(0, 0, Physics.gravity.y*-1 * gravityMultiplier.z) * rb.mass);
                break;
        }
    }
}
