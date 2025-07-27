using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    [SerializeField, Header("Audio Clips")]
    public AudioClip backgroundDay;
    public AudioClip backgroundNight;
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;
    public AudioClip sfx6;

    void Awake()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundDay)
        {
            musicSource.clip = backgroundDay;
            musicSource.Play();
        }
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void changeBackgroundMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
