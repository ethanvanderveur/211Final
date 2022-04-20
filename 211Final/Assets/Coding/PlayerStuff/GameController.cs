using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public int planetNumber;

    GameObject currentCheckpoint;

    [SerializeField]
    static GameObject playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("PlayerCharacter");
        currentCheckpoint = GameObject.Find(PlayerPrefs.GetString("curCheck"));
        Debug.Log(currentCheckpoint.name);
        planetNumber = 1;
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
        Debug.Log("Hit Checkpoint");
    }

    public void playerDeath()
    {
        //SceneManager.LoadScene("Planet" + planetNumber);
        PlayerPrefs.SetString("curCheck", currentCheckpoint.name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        playerCharacter.transform.Translate(new Vector3(0, 100, 0));
        Debug.Log("Transform ran");
    }
}
