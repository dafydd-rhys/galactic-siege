using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;

    private float timeScale;

    public void Pause() {
        pausePanel.SetActive(true);
        timeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Continue() {
        pausePanel.SetActive(false);
        Time.timeScale = timeScale;
    }

    public void Exit() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
