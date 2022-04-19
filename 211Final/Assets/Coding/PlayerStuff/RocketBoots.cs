using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoots : MonoBehaviour
{
    public CharacterController controller;
    public PlayerMovement playerMovement;
    public AudioSource boostSource;
    private bool hasBoosted;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<PlayerMovement>();
        controller = playerMovement.controller;

        boostSource = GameObject.Find("BootAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasBoosted && !playerMovement.isGrounded)
        {
            if (playerMovement.playerGrav == PlayerMovement.PlayerGravity.negative)
            {
                boostSource.Play();
                playerMovement.velocity.y = Mathf.Sqrt(playerMovement.jumpHeight * -1f * playerMovement.gravity);
            }
            else if (playerMovement.playerGrav == PlayerMovement.PlayerGravity.positive)
            {
                boostSource.Play();
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
