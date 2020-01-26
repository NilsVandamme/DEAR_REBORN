using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Manage the audio of the game

public class SC_GM_SoundManager : MonoBehaviour
{
    public static SC_GM_SoundManager instance; // singleton instance

    [Header("Audioclips pour Random")]
    public AudioClip[] AC_Click;
    public AudioClip[] AC_Radio;


    [Header("AudioSources")]
    private AudioSource ASourceSound; // Audiosource for the sounds
    private AudioSource ASourceRandomSounds;
    private AudioSource ASourceMusic; // Audiosource for the music
    private AudioSource ASourceOffice, ASourceComputer;



    [Header("Liens UI")]
    public Slider soundSlider; // Slider from the main menu (no refs needed in other scenes)
    public Slider musicSlider; // Slider from the main menu (no refs needed in other scenes)

    [Header("Audioclips PlayOnce")]
    public List<AudioClip> audioclips; // All audioclips which can be played

    [Header("Piano Launch Game")]
    public List<AudioClip> pianoSounds;
    private bool showPosition = true;
    public bool clickSoundEnabled = true;

    [Header("Liste musiques")]
    public AudioClip[] radioMusics;
    private int currentTrack = 0;

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
        if (Input.GetMouseButtonDown(0))
        {
            if(clickSoundEnabled == true)
            {
                ASourceRandomSounds.clip = AC_Click[Random.Range(0, AC_Click.Length)];
                ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
                ASourceRandomSounds.Play();

            }
           
        }

       
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
            ASourceSound.volume = PlayerPrefs.GetFloat("SoundVolume");
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
                        ASourceSound.pitch = Random.Range(0.9f, 1.1f);
                    }
                    else
                    {
                        ASourceSound.pitch = 1;
                        
                    }
                                            
                    ASourceSound.PlayOneShot(clip);
                }
            }
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
    }

    // Change the volume in main menu
    public void ChangeMusicVolume(float SliderValue)
    {
        ASourceMusic.volume = SliderValue;
        PlayerPrefs.SetFloat("MusicVolume", SliderValue);
    }

 
    public void SkipMusicRadio()
    {
        if (currentTrack > radioMusics.Length)
        {
            currentTrack = 0;
        }
        StartCoroutine(SkipEffect());
       
        
    }


    public void PreviousMusicRadio()
    {
        if (currentTrack <0 )
        {
            currentTrack = radioMusics.Length;
        }

    }
    IEnumerator SkipEffect()
    {
        ASourceMusic.Stop();
        currentTrack++;
        ASourceRandomSounds.clip = AC_Radio[Random.Range(0, AC_Radio.Length)];
        ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
        ASourceRandomSounds.Play();
        yield return new WaitWhile(() => ASourceRandomSounds.isPlaying);
        ASourceMusic.clip = radioMusics[currentTrack];
        ASourceMusic.Play();
    }

    IEnumerator PreviousEffect()
    {
        ASourceMusic.Stop();
        currentTrack--;
        ASourceRandomSounds.clip = AC_Radio[Random.Range(0, AC_Radio.Length)];
        ASourceRandomSounds.pitch = Random.Range(0.9f, 1f);
        ASourceRandomSounds.Play();
        yield return new WaitWhile(() => ASourceRandomSounds.isPlaying);
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
            ASourceMusic.clip= radioMusics[currentTrack];
            ASourceMusic.Play();
        }
       

       
    }
    public void StopMusic()
    {
    
        ASourceMusic.Stop();

    }


}
