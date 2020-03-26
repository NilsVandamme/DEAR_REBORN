using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_MainMenuAudio : MonoBehaviour
{
    public AudioSource asourceMusic;
    public AudioSource asourceAmbient;
    public AudioSource asourceTest;

    [Space]

    public Slider sliderMusic;
    public Slider sliderSound;


    private void Awake()
    {
        // Get the saved setting from the playerprefs
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            Debug.Log("Sound volume playerprefs = " + PlayerPrefs.GetFloat("SoundVolume").ToString());
            asourceAmbient.volume = PlayerPrefs.GetFloat("SoundVolume");
            asourceTest.volume = PlayerPrefs.GetFloat("SoundVolume");
            sliderSound.value = PlayerPrefs.GetFloat("SoundVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SoundVolume", 0.5f);
            asourceAmbient.volume = 0.5f;
            asourceTest.volume = 0.5f;
            sliderSound.value = 0.5f;
        }

        // Get the saved setting from the playerprefs
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Debug.Log("Music volume playerprefs = " + PlayerPrefs.GetFloat("MusicVolume").ToString());
            asourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
            sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            asourceMusic.volume = 0.5f;
            sliderMusic.value = 0.5f;
        }
    }

    public void ChangeMenuSoundVolume(float sliderVolume)
    {
        asourceAmbient.volume = sliderVolume;
        asourceTest.volume = sliderVolume;
        sliderSound.value = sliderVolume;
        PlayerPrefs.SetFloat("SoundVolume", sliderVolume);
    }

    public void ChangeMenuMusicVolume(float sliderVolume)
    {
        asourceMusic.volume = sliderVolume;
        sliderMusic.value = sliderVolume;
        PlayerPrefs.SetFloat("MusicVolume", sliderVolume);
    }

    public void PlaytestSound()
    {
        asourceTest.Play();
    }
}
