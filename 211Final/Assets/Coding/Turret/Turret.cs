using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField]
    bool lineOfSight;
    // Start is called before the first frame update
    void Start()
    {
        lineOfSight = false;
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
        if (Physics.Raycast(ray, out hit, 40, layer)) {
            lineOfSight = false;
        }
        else {
            lineOfSight = true;
        }


        if (lineOfSight) {
            //this.transform.rotation = Quaternion.Euler(rayDirection);
            this.transform.LookAt(target.transform);
        }
    }
}
