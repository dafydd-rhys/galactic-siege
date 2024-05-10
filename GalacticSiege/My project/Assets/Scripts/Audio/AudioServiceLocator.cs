using UnityEngine;

public static class AudioServiceLocator
{
    public static AudioManager AudioManagerInstance
    {
        get
        {
            return AudioManager.Instance;
        }
    }

    public static void PlaySFX(AudioClip clip)
    {
        AudioManagerInstance.PlaySFX(clip);
    }

    public static void PlayMusic(AudioClip clip)
    {
        AudioManagerInstance.PlayMusic(clip);
    }

    public static void StopMusic()
    {
        AudioManagerInstance.StopMusic();
    }

    public static void SetSFXVolume(float volume)
    {
        AudioManagerInstance.SetSFXVolume(volume);
    }

    public static void SetMusicVolume(float volume)
    {
        AudioManagerInstance.SetMusicVolume(volume);
    }

    public static float GetSFXVolume()
    {
        return AudioManagerInstance.GetSFXVolume();
    }

    public static float GetMusicVolume()
    {
        return AudioManagerInstance.GetMusicVolume();
    }
}
