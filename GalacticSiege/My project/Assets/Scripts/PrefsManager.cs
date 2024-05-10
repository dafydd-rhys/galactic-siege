using TMPro;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public TextMeshProUGUI txtHighscore;
    public AudioClip musicClip;

    void Start()
    {
        AudioServiceLocator.SetMusicVolume(PlayerPrefs.GetFloat("Music", 0.25f));
        AudioServiceLocator.SetSFXVolume(PlayerPrefs.GetFloat("SFX", 0.25f));
        AudioServiceLocator.PlayMusic(musicClip);
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        txtHighscore.text = "HIGHSCORE: " + highscore + " POINTS";
    }

}
