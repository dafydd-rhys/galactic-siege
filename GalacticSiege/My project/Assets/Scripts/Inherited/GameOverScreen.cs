using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI points;
    public TextMeshProUGUI coins;
    public GameManager game;

    public void Setup(int score, int coin) {
        gameObject.SetActive(true);

        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        if (highscore < score) {
            points.text = "NEW HIGHSCORE " + score.ToString() + " POINTS"; 
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
        } else {
            points.text = score.ToString() + " POINTS"; 
        }
  
        coins.text = "+" + coin + " COINS";
    }

    public void Restart() {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void Exit() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}
