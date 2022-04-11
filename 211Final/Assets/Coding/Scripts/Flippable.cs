using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippable : MonoBehaviour
{
    [SerializeField]
    public Vector3 gravityMultiplier;
    Rigidbody rb;

    GravityGunStatus gravityGunStatus;

    // Start is called before the first frame update
    void Start()
    {
        gravityMultiplier = new Vector3(0, -1, 0);
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        gravityGunStatus = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<GravityGunStatus>();
    }

    private void OnMouseDown()
    {
        switch (gravityGunStatus.gravityAxis)
        {
            case GravityGunStatus.GravityAxis.xAxis:
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

    // Update is called once per frame
    void Update()
    {
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
