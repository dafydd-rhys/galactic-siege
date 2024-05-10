using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new("AudioManager");
                instance = obj.AddComponent<AudioManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    private float sfxVolume = 1f;
    private float musicVolume = 0.5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxAudioSource != null && clip != null)
        {
            sfxAudioSource.PlayOneShot(clip, sfxVolume);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicAudioSource != null && clip != null)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.volume = musicVolume;
            musicAudioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = musicVolume;
        }
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
