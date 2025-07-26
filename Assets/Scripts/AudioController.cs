using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    public AudioMixer audioMixer;
    public Slider sliderMusic;

    public AudioMixer sfxMixer;
    public Slider sliderSFX;

    void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = sliderMusic.value;
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sliderSFX.value;
        sfxMixer.SetFloat("MusicVolume", volume);
    }
}
