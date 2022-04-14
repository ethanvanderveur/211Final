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

    GameObject XOverlay, YOverlay, ZOverlay;
    public GameObject XLine, YLine, ZLine;

    // Start is called before the first frame update
    void Start()
    {
        hasVerticalGun = true;
        hasHorizontalGun = true;
        gravityAxis = GravityAxis.yAxis;
        XOverlay = GameObject.Find("XOverlay");
        YOverlay = GameObject.Find("YOverlay");
        ZOverlay = GameObject.Find("ZOverlay");

        XOverlay.SetActive(true);
        YOverlay.SetActive(false);
        ZOverlay.SetActive(true);

        XLine = GameObject.Find("XLine");
        YLine = GameObject.Find("YLine");
        ZLine = GameObject.Find("ZLine");
        XLine.SetActive(false);
        YLine.SetActive(true);
        ZLine.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasVerticalGun)
        {
            gravityAxis = GravityAxis.yAxis;
            XOverlay.SetActive(true);
            YOverlay.SetActive(false);
            ZOverlay.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasHorizontalGun)
        {
            gravityAxis = GravityAxis.xAxis;
            XOverlay.SetActive(false);
            YOverlay.SetActive(true);
            ZOverlay.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasHorizontalGun)
        {
            gravityAxis = GravityAxis.zAxis;
            XOverlay.SetActive(true);
            YOverlay.SetActive(true);
            ZOverlay.SetActive(false);
        }
    }
}
