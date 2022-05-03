using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    public PauseMenu pause;
    public GameObject signUI;
    public TextMeshProUGUI uiText;
    public System.String signText = "Placeholder Text";

    private void Start(){
        pause = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<PauseMenu>();
        //signUI = GameObject.FindGameObjectWithTag("SignMenu");
        //uiText = GameObject.FindGameObjectWithTag("SignText").GetComponent<TextMeshProUGUI>();
        //signUI.SetActive(false);
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
            signUI.SetActive(true);
            uiText.text = signText;
        }
    }
}
