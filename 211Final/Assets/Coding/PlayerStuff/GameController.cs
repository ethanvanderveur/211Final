using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public int planetNumber;
    [SerializeField]
    GameObject checkpoint;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        planetNumber = 1;
        checkpoint = null;
        player = GameObject.FindGameObjectWithTag("PlayerCharacter");
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
        checkpoint = ch;
        Debug.Log("Hit Checkpoint");
    }

    public void playerDeath()
    {
        //SceneManager.LoadScene("Planet" + planetNumber);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.transform.position = checkpoint.transform.position;
    }
}
