using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunStatus : MonoBehaviour
{
    public bool hasVerticalGun = false;
    public bool hasHorizontalGun = false;

    [SerializeField]
    public enum GravityAxis {xAxis, yAxis, zAxis};
    public GravityAxis gravityAxis;

    // Start is called before the first frame update
    void Start()
    {
        hasVerticalGun = true;
        hasHorizontalGun = true;
        gravityAxis = GravityAxis.yAxis;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasVerticalGun)
        {
            gravityAxis = GravityAxis.yAxis;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasHorizontalGun)
        {
            gravityAxis = GravityAxis.xAxis;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasHorizontalGun)
        {
            gravityAxis = GravityAxis.zAxis;
        }
    }
}
