using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public int planetNumber;

    GameObject currentCheckpoint;

    [SerializeField]
    static GameObject playerCharacter;

    public AudioSource deathAudio;
    public AudioSource respawnAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Load player info back in
        playerCharacter = GameObject.FindGameObjectWithTag("PlayerCharacter");
        currentCheckpoint = GameObject.Find(PlayerPrefs.GetString("curCheck"));
        planetNumber = PlayerPrefs.GetInt("planetNumber");

        switch (PlayerPrefs.GetString("hasVerticalGun"))
        {
            case "True":
                GravityGunStatus.hasVerticalGun = true;
                break;
            default:
                GravityGunStatus.hasVerticalGun = false;
                break;
        }
        switch (PlayerPrefs.GetString("hasHorizontalGun"))
        {
            case "True":
                GravityGunStatus.hasHorizontalGun = true;
                break;
            default:
                GravityGunStatus.hasHorizontalGun = false;
                break;
        }
        switch (PlayerPrefs.GetString("hasTimeSlow"))
        {
            case "True":
                GravityGunStatus.hasTimeSlow = true;
                break;
            default:
                GravityGunStatus.hasTimeSlow = false;
                break;
        }
        switch (PlayerPrefs.GetString("hasGravitySuit"))
        {
            case "True":
                GravityGunStatus.hasGravitySuit = true;
                break;
            default:
                GravityGunStatus.hasGravitySuit = false;
                break;
        }

        StartCoroutine(Respawn());
    }


    // Update is called once per frame
    void Update()
    {
        // ==================== Simulate Player Death ====================
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerDeath();
        }

    }


    public void hitCheckPoint(GameObject ch)
    {
        currentCheckpoint = ch;
        Debug.Log("Hit " + currentCheckpoint.name);

        // Logic for adding and removing items
        switch (currentCheckpoint.name)
        {
            case "Checkpoint1":
                GravityGunStatus.hasVerticalGun = true;
                GravityGunStatus.hasHorizontalGun = true;
                GravityGunStatus.hasTimeSlow = true;
                GravityGunStatus.hasGravitySuit = true;
                break;
            default:
                break;
        }


    }

    public void nextPlanet()
    {
        savePlayerInfo();
        SceneManager.LoadScene("Planet" + planetNumber);
    }

    public void playerDeath()
    {
        //SceneManager.LoadScene("Planet" + planetNumber);
        savePlayerInfo();
        deathAudio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void savePlayerInfo()
    {
        PlayerPrefs.SetString("curCheck", currentCheckpoint.name);
        PlayerPrefs.SetString("hasVerticalGun", GravityGunStatus.hasVerticalGun.ToString());
        PlayerPrefs.SetString("hasHorizontalGun", GravityGunStatus.hasHorizontalGun.ToString());
        PlayerPrefs.SetString("hasTimeSlow", GravityGunStatus.hasTimeSlow.ToString());
        PlayerPrefs.SetString("hasGravitySuit", GravityGunStatus.hasGravitySuit.ToString());
        PlayerPrefs.SetInt("planetNumber", planetNumber);
    }

    IEnumerator Respawn()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForEndOfFrame();
            playerCharacter.transform.position = currentCheckpoint.transform.position + new Vector3(0, .5f, 0);
        }
        respawnAudio.Play();
    }
}
