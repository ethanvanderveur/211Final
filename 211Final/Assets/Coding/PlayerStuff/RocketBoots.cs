using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoots : MonoBehaviour
{
    public CharacterController controller;
    public PlayerMovement playerMovement;
    private bool hasBoosted;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<PlayerMovement>();
        controller = playerMovement.controller;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasBoosted && !playerMovement.isGrounded)
        {
            if (playerMovement.gravMode == 0)
            {
                playerMovement.velocity.y = Mathf.Sqrt(playerMovement.jumpHeight * -1f * playerMovement.gravity);
            }
            else if (playerMovement.gravMode == 1)
            {
                playerMovement.velocity.y = Mathf.Sqrt(playerMovement.jumpHeight * -1f * playerMovement.gravity);//this one may need some tuning, not sure if -2 or jumpheight need to be negative
            }
            hasBoosted = true;
        }
        if (playerMovement.isGrounded)
        {
            hasBoosted = false;
        }
    }
}
