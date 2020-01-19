using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SC_GM_SoundManager : MonoBehaviour
{
    public static SC_GM_SoundManager instance;

    private AudioSource ASourceSound;
    private AudioSource ASourceMusic;

    public Slider soundSlider;
    public Slider musicSlider;

    public List<AudioClip> audioclips;
        

    private void Awake()
    {
        // Singleton logic
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ASourceSound = transform.GetChild(0).GetComponent<AudioSource>();
        ASourceMusic = transform.GetChild(1).GetComponent<AudioSource>();


        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            ASourceSound.volume = PlayerPrefs.GetFloat("SoundVolume");
            if (SceneManager.GetActiveScene().name == "L_00Menu")
                soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SoundVolume", 1) ;
            ASourceSound.volume = 1;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            
            ASourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
            if (SceneManager.GetActiveScene().name == "L_00Menu")
                musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            ASourceSound.volume = 1;
        }
    }

    public void PlaySound(string name)
    {
        foreach (AudioClip audio in audioclips)
        {
            if (audio.name == name)
            {
                ASourceSound.PlayOneShot(audio);
                //Debug.Log(audio.name + " sound played");
            }
        }
    }

    public void ChangeSoundVolume(float SliderValue)
    {
        ASourceSound.volume = SliderValue;
        PlayerPrefs.SetFloat("SoundVolume", SliderValue);
    }

    public void ChangeMusicVolume(float SliderValue)
    {
        ASourceMusic.volume = SliderValue;
        PlayerPrefs.SetFloat("MusicVolume", SliderValue);
    }
}
