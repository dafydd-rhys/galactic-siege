using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    
    public Slider volume;
    public Slider sfx;
    public float SFXVolume = 0.25f;

    void Start() {
        volume.value = PlayerPrefs.GetFloat("Music", SFXVolume);
        sfx.value = PlayerPrefs.GetFloat("SFX", SFXVolume);
    }

    public void AdjustMusicVolume() {
        PlayerPrefs.SetFloat("Music", volume.value);
        PlayerPrefs.Save();
        AudioServiceLocator.SetMusicVolume(volume.value);
    }

    public void AdjustSFXVolume() {
        PlayerPrefs.SetFloat("SFX", sfx.value);
        PlayerPrefs.Save();
        AudioServiceLocator.SetSFXVolume(sfx.value);
    }

}
