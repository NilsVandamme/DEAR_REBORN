using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SC_GM_SoundManager : MonoBehaviour
{
    public static SC_GM_SoundManager instance; // singleton instance

    [Header("Audioclips pour Random")]
    public AudioClip[] AC_ClickEnter;
    public AudioClip[] AC_ClickLeave;
    public AudioClip[] AC_Radio;
    public AudioClip[] AC_MessageBoss;
    public AudioClip[] AC_MessageEmployee;
    public AudioClip[] AC_Hover;



    [Header("AudioSources")]
    public AudioSource ASourceSound; // Audiosource for the sounds
    public AudioSource ASourceRandomSounds;
    public AudioSource ASourceMusic; // Audiosource for the music
    public AudioSource ASourceOffice, ASourceComputer;



    [Header("Liens UI")]
    public Slider soundSlider; // Slider from the main menu (no refs needed in other scenes)
    public Slider musicSlider; // Slider from the main menu (no refs needed in other scenes)
    public Slider radioSlider; // Slider from the main menu (no refs needed in other scenes)

    [Header("Audioclips PlayOnce")]
    public List<AudioClip> audioclips; // All audioclips which can be played

    [Header("Piano Launch Game")]
    public List<AudioClip> pianoSounds;
    private bool showPosition = true;
    public bool clickSoundEnabled = true;

    [Header("Liste musiques")]
    public AudioClip[] radioMusics;
    private int currentTrack = 0;
    private int maximumTrack;
    private bool playCurrentMusic = true;

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
    public void PlayPiano(int pianoNumber)
    {
        ASourceSound.clip = pianoSounds[pianoNumber];
        ASourceSound.Play();
       
    }


    private void Update()
    {
        if (currentTrack < 0)
        {
            currentTrack = 1;
        }

        if (currentTrack > radioMusics.Length)
        {
            currentTrack = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(clickSoundEnabled == true)
            {
                ASourceRandomSounds.clip = AC_ClickEnter[Random.Range(0, AC_ClickEnter.Length)];
                ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
                ASourceRandomSounds.Play();

            }
           
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (clickSoundEnabled == true)
            {
                ASourceRandomSounds.clip = AC_ClickEnter[Random.Range(0, AC_ClickEnter.Length)];
                ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
                ASourceRandomSounds.Play();

            }

        }
        // Change the volume of the radio
    }
    private void Start()
    {
        // Get audiosources from childs
        ASourceSound = transform.GetChild(0).GetComponent<AudioSource>();
        ASourceMusic = transform.GetChild(1).GetComponent<AudioSource>();
        ASourceOffice = transform.GetChild(2).GetComponent<AudioSource>();
        ASourceComputer = transform.GetChild(3).GetComponent<AudioSource>();
        ASourceRandomSounds = transform.GetChild(4).GetComponent<AudioSource>();

        // Get the saved setting from the playerprefs
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            Debug.Log("Sound volume playerprefs = " + PlayerPrefs.GetFloat("SoundVolume").ToString());
            ASourceSound.volume = PlayerPrefs.GetFloat("SoundVolume");
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SoundVolume", 0.5f) ;
            ASourceSound.volume = 0.5f;
            soundSlider.value = 0.5f;
        }

        // Get the saved setting from the playerprefs
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Debug.Log("Music volume playerprefs = " + PlayerPrefs.GetFloat("MusicVolume").ToString());
            ASourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            radioSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            ASourceSound.volume = 0.5f;
            musicSlider.value = 0.5f;
            radioSlider.value = 0.5f;
        }

        // Get the maximum of music for the radio as an index
        maximumTrack = radioMusics.Length - 1;
    }

    // Play the specified sound from audioclips list
    public void PlaySound(string name)
    {
        
            foreach(AudioClip clip in audioclips)
            {
                if (clip.name == name)
                {
                     ASourceSound.pitch = Random.Range(0.9f, 1.1f);
                    
                    
                                            
                    ASourceSound.PlayOneShot(clip);
                }
            }
            
    }

    public void PlayMessageBossSound()
    {
        ASourceSound.clip = AC_MessageBoss[Random.Range(0, AC_MessageBoss.Length)];
        ASourceSound.Play();
    }

    public void PlayMessageEmployeeSound()
    {
        ASourceSound.clip = AC_MessageEmployee[Random.Range(0, AC_MessageBoss.Length)];
        ASourceSound.Play();
    }

    public void PlayHoverOnPhraseSound()
    {
        ASourceRandomSounds.clip = AC_Hover[Random.Range(0, AC_Hover.Length)];
        ASourceRandomSounds.Play();
    }

    public void FadeAmbiance()
    {
      if(ASourceOffice.volume > ASourceComputer.volume)
        {
            ASourceOffice.volume -= 0.2f;
            ASourceComputer.volume += 0.2f;
        }
        if (ASourceComputer.volume > ASourceOffice.volume)
        {
            ASourceOffice.volume += 0.2f;
            ASourceComputer.volume -= 0.2f;
        }

    }



    // Change the volume in main menu
    public void ChangeSoundVolume(float SliderValue)
    {
        ASourceSound.volume = SliderValue;
        PlayerPrefs.SetFloat("SoundVolume", SliderValue);
        Debug.Log("Sound volume current = " + PlayerPrefs.GetFloat("SoundVolume").ToString());
    }

    // Change the volume in main menu
    public void ChangeMusicVolume(float SliderValue)
    {
        ASourceMusic.volume = SliderValue;
        musicSlider.value = SliderValue;
        radioSlider.value = SliderValue;
        PlayerPrefs.SetFloat("MusicVolume", SliderValue);
        Debug.Log("Music volume current = " + PlayerPrefs.GetFloat("MusicVolume").ToString());
    }

 
    public void SkipMusicRadio()
    {
        StartCoroutine(SkipEffect());
    }


    public void PreviousMusicRadio()
    {
        StartCoroutine(PreviousEffect());
    }


    IEnumerator SkipEffect()
    {
        ASourceMusic.Stop();

        ASourceRandomSounds.clip = AC_Radio[Random.Range(0, AC_Radio.Length)];
        ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
        ASourceRandomSounds.Play();
        yield return new WaitWhile(() => ASourceRandomSounds.isPlaying);

        if (currentTrack + 1 > maximumTrack) currentTrack = 0;
        else currentTrack++;

        ASourceMusic.clip = radioMusics[currentTrack];
        ASourceMusic.Play();
    }

    IEnumerator PreviousEffect()
    {
        ASourceMusic.Stop();
        ASourceRandomSounds.clip = AC_Radio[Random.Range(0, AC_Radio.Length)];
        ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
        ASourceRandomSounds.Play();
        yield return new WaitWhile(() => ASourceRandomSounds.isPlaying);

        if (currentTrack - 1 < 0) currentTrack = maximumTrack;
        else currentTrack--;

        ASourceMusic.clip = radioMusics[currentTrack];
        ASourceMusic.Play();
    }

    public void PlayMusic()
    {
        if (ASourceMusic.isPlaying)
        {
            return;
        }
        else
        {
            ASourceMusic.clip= radioMusics[0];
            ASourceMusic.Play();
        }
       

       
    }
    public void StopMusic()
    {
    
        ASourceMusic.Stop();

    }


    // Stop or start the current music
    public void StartStopCurrentMusic()
    {
        if (playCurrentMusic)
        {
            playCurrentMusic = false;
            ASourceMusic.Stop();
        }
        else
        {
            playCurrentMusic = true;
            ASourceMusic.clip = radioMusics[currentTrack];
            ASourceMusic.Play();
        }
    }
}
