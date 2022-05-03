using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField]
    bool lineOfSight;
    GameObject turret;
    [SerializeField]
    GameObject turretLaser;
    GameObject player;
    int deathCounter;
    GameController controllerScript;
    Flippable flipScript;

    // Start is called before the first frame update
    void Start()
    {
        lineOfSight = false;
        GameObject turret = GameObject.Find("Body");
        GameObject turretLaser = turret.transform.GetChild(4).gameObject;
        turretLaser.SetActive(false);

        player = GameObject.FindGameObjectWithTag("PlayerCharacter");
        controllerScript = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayDirection;
        GameObject target;
        target = GameObject.FindGameObjectWithTag("PlayerCharacter");
        rayDirection = target.transform.position - transform.position;
        var ray = new Ray(this.transform.position, rayDirection);
        RaycastHit hit;
        Debug.DrawRay(transform.position, rayDirection, Color.red);
        if (Physics.Raycast(ray, out hit, Vector3.Distance(player.transform.position, this.transform.position), layer))
        {
            lineOfSight = false;
        }
        else
        {
            lineOfSight = true;
        }



        if (lineOfSight)
        {
            this.transform.LookAt(target.transform);
            turretLaser.SetActive(true);
            deathCounter += 1;
            if (deathCounter >= 300)
            {
                deathCounter = 0;
                controllerScript.playerDeath();
            }
        }
        else
        {
            turretLaser.SetActive(false);
            deathCounter = 0;
        }

    }
}

