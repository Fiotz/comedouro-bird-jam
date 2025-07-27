using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    public AudioMixer audioMixer;
    public Slider sliderMusic;
    public Slider sliderSFX;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFX();
        }
        else
        {
            SetSFXVolume();
        }
    }

    public void LoadVolume() {
        sliderMusic.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    public void LoadSFX() {
        sliderSFX.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = sliderMusic.value;
        PlayerPrefs.SetFloat("musicVolume", volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = sliderSFX.value;
        PlayerPrefs.SetFloat("sfxVolume", volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume)*20);
    }
}
