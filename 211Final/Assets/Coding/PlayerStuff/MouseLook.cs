using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 2f;

    public Transform playerBody;

    PlayerMovement playerMovement;

    private float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = playerMovement.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement.isRotatingCamera && !PauseMenu.GameIsPaused)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
