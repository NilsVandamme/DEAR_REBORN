using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manage the audio of the game

public class SC_GM_SoundManager : MonoBehaviour
{
    public static SC_GM_SoundManager instance; // singleton instance

    private AudioSource ASourceSound; // Audiosource for the sounds
    private AudioSource ASourceMusic; // Audiosource for the music

    public Slider soundSlider; // Slider from the main menu (no refs needed in other scenes)
    public Slider musicSlider; // Slider from the main menu (no refs needed in other scenes)

    public List<AudioClip> audioclips; // All audioclips which can be played

    private bool showPosition = true;

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
        // Get audiosources from childs
        ASourceSound = transform.GetChild(0).GetComponent<AudioSource>();
        ASourceMusic = transform.GetChild(1).GetComponent<AudioSource>();

        // Get the saved setting from the playerprefs
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

        // Get the saved setting from the playerprefs
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

    // Play the specified sound from audioclips list
    public void PlaySound(string name, bool RandomPitch)
    {
            foreach(AudioClip clip in audioclips)
            {
                if (clip.name == name)
                {
                    if (RandomPitch)
                    {
                        ASourceSound.pitch = Random.Range(0.95f, 1.05f);
                    }
                    else
                    {
                        ASourceSound.pitch = 1;
                    }

                    ASourceSound.PlayOneShot(clip);
                }
            }
    }

    // Change the volume in main menu
    public void ChangeSoundVolume(float SliderValue)
    {
        ASourceSound.volume = SliderValue;
        PlayerPrefs.SetFloat("SoundVolume", SliderValue);
    }

    // Change the volume in main menu
    public void ChangeMusicVolume(float SliderValue)
    {
        ASourceMusic.volume = SliderValue;
        PlayerPrefs.SetFloat("MusicVolume", SliderValue);
    }
}
