using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityGunStatus : MonoBehaviour
{
    public static bool hasVerticalGun = false;
    public static bool hasHorizontalGun = false;
    public static bool hasTimeSlow = false;
    public static bool hasGravitySuit = false;

    [SerializeField]
    public enum GravityAxis {xAxis, yAxis, zAxis};
    public GravityAxis gravityAxis;

    public float TIME_SLOW_COOLDOWN_SECONDS = 1.5f;
    public float timeSlowCooldown = 0f;
    public bool slowedAnObject = false;

    Image TimeSlowOverlay;
    GameObject XOverlay, YOverlay, ZOverlay;
    public GameObject XLine, YLine, ZLine;



    // Start is called before the first frame update
    void Start()
    {
        hasVerticalGun = true;
        hasHorizontalGun = true;
        hasTimeSlow = true;
        hasGravitySuit = true;

        TimeSlowOverlay = GameObject.Find("TimeSlowOverlay").GetComponent<Image>();

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
        // Axis control
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

        // Time slow
        if (timeSlowCooldown > 0)
        {
            timeSlowCooldown -= Time.deltaTime;
        } else if (slowedAnObject && hasTimeSlow)
        {
            slowedAnObject = false;
            timeSlowCooldown = TIME_SLOW_COOLDOWN_SECONDS;
        }

        TimeSlowOverlay.fillAmount = timeSlowCooldown / TIME_SLOW_COOLDOWN_SECONDS;
    }
}
