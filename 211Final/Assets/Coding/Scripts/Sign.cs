using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public PauseMenu pause;
    public GameObject signUI;
    public Text uiText;
    public System.String signText = "Placeholder Text";

    private void Start(){
        pause = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<PauseMenu>();
        uiText = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<UnityEngine.UI.Text>();
    }

    private void OnMouseEnter()
    {
       // outline.enabled = true;
    }

    private void OnMouseExit()
    {
       // outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            PauseMenu.GameIsPaused = true;
            uiText.text = signText;
        }
    }
}
