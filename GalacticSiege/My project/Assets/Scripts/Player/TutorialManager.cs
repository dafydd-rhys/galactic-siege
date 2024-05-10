using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject tutorial;
    public GameObject[] popUps;
    public int index = 0;
    public GameObject spawner;
    public PlayerController player;
    public PlayerController[] playerPrefabs;
    public float waitTime = 2f;
    private bool isWaiting = false;
    private bool inTutorial = true;
    public GameObject tutPowerUp;
    public GameObject tutAsteroid;
    public GameObject tutEnemy;
    public GameManager game;
    public GameObject btnPause;

    void Start() {
        int enabledPlayer = PlayerPrefs.GetInt("Player", 0);
        player = Instantiate(playerPrefabs[enabledPlayer], Vector3.zero, Quaternion.identity);
        player = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++) {
            if (i == index) {
                popUps[i].SetActive(true);
            } else {
                popUps[i].SetActive(false);
            }
        }  

        if (Input.GetKeyDown(KeyCode.Escape)) {
            inTutorial = false;
            tutorial.SetActive(false);
            Destroy(tutEnemy);
            Destroy(tutAsteroid);
            Destroy(tutPowerUp);
            spawner.SetActive(true);
            player.SetPlayerBools(true, true, true, true);
            player.Reset();
            game.SetPlayer(player);
            btnPause.SetActive(true);
        }

        if (inTutorial) {
            if (index == 0) {
                player.SetPlayerBools(true, false, false, false);
            } else if (index == 1) {
                player.SetPlayerBools(false, true, false, false);
            } else if (index == 2) {
                player.SetPlayerBools(false, false, true, false);
            } else if (index == 3) {
                player.SetPlayerBools(false, false, false, true);
            } else {
                player.SetPlayerBools(false, false, false, false);
            }

            //0 = thrust, 1 = rotate, 2 = shoot, 3 == boost, 4 = enemies, 5 = powerups, 6 = asteroids, 7 = boundaries
            if (index >= 0 && index <= 3) {
                HandleInputAction(index == 0 ? "Vertical" : index == 1 ? "Horizontal" : index == 2 ? "Shoot" : "Boost", KeyCode.Space, true);
            } else if (index >= 4 && index <= 6) {
                HandleInputAction("", KeyCode.Space, false);
            }
        }
    }

    void HandleInputAction(string axisName, KeyCode keyCode, bool keys) {
        if (keys && Input.GetAxis(axisName) > 0 && !isWaiting) {
            StartCoroutine(WaitForKeyPress(axisName, keyCode));
        } else if (Input.GetKeyDown(keyCode) && !isWaiting) {
            StartCoroutine(WaitForKeyPress(axisName, keyCode));

            if (index == 4) {
                tutEnemy.SetActive(true);
            } else if (index == 5) {
                tutPowerUp.SetActive(true);
            } else if (index == 6) {
                tutAsteroid.SetActive(true);
            } 
        }
    }

    IEnumerator WaitForKeyPress(string axisName, KeyCode keyCode) {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        index++;
        isWaiting = false;
        stopPlayer();
    }

    void stopPlayer() {
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        player.GetBody().velocity = Vector3.zero;
        player.GetBody().angularVelocity = 0f;
    }

}
