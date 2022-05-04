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

    public GameObject yUI;
    public GameObject xUI;
    public GameObject zUI;
    public GameObject timeUI;
    public GameObject flipUI;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("curCheck") == "")
        {
            PlayerPrefs.SetString("curCheck", "Checkpoint1");
        }
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

        if (GravityGunStatus.hasVerticalGun && !yUI.activeSelf)
        {
            yUI.SetActive(true);
        }
        else if (!GravityGunStatus.hasVerticalGun && yUI.activeSelf)
        {
            yUI.SetActive(false);
        }

        if (GravityGunStatus.hasHorizontalGun && !xUI.activeSelf)
        {
            xUI.SetActive(true);
        }
        else if (!GravityGunStatus.hasHorizontalGun && xUI.activeSelf)
        {
            xUI.SetActive(false);
        }
        if (GravityGunStatus.hasHorizontalGun && !zUI.activeSelf)
        {
            zUI.SetActive(true);
        }
        else if (!GravityGunStatus.hasHorizontalGun && zUI.activeSelf)
        {
            zUI.SetActive(false);
        }

        if (GravityGunStatus.hasTimeSlow && !timeUI.activeSelf)
        {
            timeUI.SetActive(true);
        }
        else if (!GravityGunStatus.hasTimeSlow && timeUI.activeSelf)
        {
            timeUI.SetActive(false);
        }

        if (GravityGunStatus.hasGravitySuit && !flipUI.activeSelf)
        {
            flipUI.SetActive(true);
        }
        else if (!GravityGunStatus.hasGravitySuit && flipUI.activeSelf)
        {
            flipUI.SetActive(false);
        }
    }


    public void hitCheckPoint(GameObject ch)
    {
        currentCheckpoint = ch;
        Debug.Log("Hit " + currentCheckpoint.name);

        Debug.Log("Current Planet: " + planetNumber);

        if (currentCheckpoint.name == "ResetEverything")
        {
            GravityGunStatus.hasVerticalGun = false;
            GravityGunStatus.hasHorizontalGun = false;
            GravityGunStatus.hasTimeSlow = false;
            GravityGunStatus.hasGravitySuit = false;
            planetNumber = 1;
            currentCheckpoint.name = "Checkpoint1";
            savePlayerInfo();
            Debug.Log("RESETING");
        }
        // Logic for adding and removing items
        switch (planetNumber)
        {
            case 1:
                Debug.Log("Planet1!");
                switch (currentCheckpoint.name)
                {
                    case "Checkpoint1":
                        GravityGunStatus.hasVerticalGun = false;
                        GravityGunStatus.hasHorizontalGun = false;
                        GravityGunStatus.hasTimeSlow = false;
                        GravityGunStatus.hasGravitySuit = false;
                        break;
                    case "Planet1Portal":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = false;
                        GravityGunStatus.hasTimeSlow = false;
                        GravityGunStatus.hasGravitySuit = false;
                        currentCheckpoint.name = "Checkpoint1";
                        nextPlanet();
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                Debug.Log("Planet2!");
                switch (currentCheckpoint.name)
                {
                    case "Checkpoint1":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = false;
                        GravityGunStatus.hasTimeSlow = false;
                        GravityGunStatus.hasGravitySuit = false;
                        break;
                    case "Planet2Portal":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = false;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = false;
                        currentCheckpoint.name = "Checkpoint1";
                        nextPlanet();
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                Debug.Log("Planet3!");
                switch (currentCheckpoint.name)
                {
                    case "Checkpoint1":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = false;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = false;
                        break;
                    case "Planet3Portal":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = true;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = false;
                        currentCheckpoint.name = "Checkpoint1";
                        nextPlanet();
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                Debug.Log("Planet4!");
                switch (currentCheckpoint.name)
                {
                    case "Checkpoint1":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = true;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = false;
                        break;
                    case "Planet4Portal":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = true;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = true;
                        currentCheckpoint.name = "Checkpoint1";
                        nextPlanet();
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                Debug.Log("Planet5!");
                switch (currentCheckpoint.name)
                {
                    case "Checkpoint1":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = true;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = true;
                        break;
                    case "Planet5Portal":
                        GravityGunStatus.hasVerticalGun = true;
                        GravityGunStatus.hasHorizontalGun = true;
                        GravityGunStatus.hasTimeSlow = true;
                        GravityGunStatus.hasGravitySuit = true;
                        currentCheckpoint.name = "Checkpoint1";
                        nextPlanet();
                        break;
                    default:
                        break;
                }
                break;
        }


    }

    public void nextPlanet()
    {
        planetNumber++;
        savePlayerInfo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
