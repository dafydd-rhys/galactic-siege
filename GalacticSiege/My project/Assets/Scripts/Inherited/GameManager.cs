using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerController player;
    public int lives = 3;
    public ParticleSystem explosion;
    public GameOverScreen GameOverScreen;
    public TextMeshProUGUI notify;

    public AudioClip inGameMusic;
    public AudioClip asteroidClip;
    public AudioClip deathClip;
    public AudioClip enemyClip;
    public AudioClip gameoverClip;
    public AudioClip powerupClip;

    private int score;
    private bool doublePoints = false;

    void Start() {
        AudioServiceLocator.StopMusic();
        AudioServiceLocator.PlayMusic(inGameMusic);
    }

    public void PlayerDied() {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        lives--;

        if (lives <= 0) {
            AudioServiceLocator.PlaySFX(gameoverClip);
            player.gameObject.SetActive(false);
            GameOver();
        } else {
            AudioServiceLocator.PlaySFX(deathClip);
            Notify(lives + " LIVES");
            player.Reset();
            player.transform.position = Vector3.zero;
            gameObject.AddComponent<Invulnerability>().ApplyPowerUp(player, this);
        }    
    }

    private void GameOver() {
        int coins = score / 30;

        int currentCoins = PlayerPrefs.GetInt("Coins", 0);
        PlayerPrefs.SetInt("Coins", currentCoins + coins);
        PlayerPrefs.Save();

        GameOverScreen.Setup(score, coins);
    }

    internal void EnemyDestroyed(EnemyController enemy)
    {
        AudioServiceLocator.PlaySFX(enemyClip);
        if (doublePoints) {
            score += enemy.reward * 2;
        } else {
            score += enemy.reward;
        }
        
        explosion.transform.position = enemy.transform.position;
        explosion.Play();

        Destroy(enemy);
        SetScore();
    }

    public void AsteroidDestroyed(Asteroid asteroid) {
        AudioServiceLocator.PlaySFX(asteroidClip);
        if (doublePoints) {
            score += asteroid.reward * 2;
        } else {
            score += asteroid.reward;
        }
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        Destroy(asteroid);
        SetScore();
    }

    private void SetScore()
    {
        player.GetScore().text = score.ToString();
    }

    public void PowerUpDestroyed(PowerUp power)
    {
        AudioServiceLocator.PlaySFX(powerupClip);
        power.ActivatePowerUp(player, this);
    }

    public void Notify(string message) {
        notify.text = message;
        notify.gameObject.SetActive(true);

        StartCoroutine(Wait(2.5f, 0, message));
    }

    public void SetPlayer(PlayerController newPlayer) {
        player = newPlayer;
    }
    
    public IEnumerator Wait(float delay, int id, string original)
    {
        yield return new WaitForSeconds(delay);

        if (id == 0) {
            if (original == notify.text) {
                notify.gameObject.SetActive(false);
            } 
        } else if (id == 1) {
            doublePoints = false;
        } else if (id == 2) {
            Time.timeScale = 1.0f; 
        } else if (id == 3) {
            player.ResetDelay(); 
        } 
    }

    internal void SetDoublePoints(bool v)
    {
        doublePoints = v;
    }

    internal bool IsDoublePoints()
    {
        return doublePoints;
    }

    internal void AddScore(int points)
    {
        score += points;
        SetScore();
    }

    internal int GetScore()
    {
        return score;
    }

    internal void StartCoroutineTimer(float x, int y, string s)
    {
        StartCoroutine(Wait(x, y, s));
    }

    public EnemyController[] FindEnemies() {
        return FindObjectsOfType<EnemyController>();
    }

    public Asteroid[] FindAsteroids() {
        return FindObjectsOfType<Asteroid>();
    }

    public void DestroyGameObject(GameObject gameObject) {
        Destroy(gameObject);
    }
}
