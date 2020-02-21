using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GM_Radio : MonoBehaviour
{
    public AudioClip[] listAudioClip;

    AudioSource audioSource;
    int totalMusics;
    int currentMusic;
    bool playCurrentMusic = false;


    // Start is called before the first frame update
    void Start()
    {
        //electricVibration = Resources.Load<AudioClip>("Audio/electricVibration");

        // Give the possibility to control differents audio source on the same object
        audioSource = GetComponent<AudioSource>();

        // Get the number of music for the radio and make it the correct max index
        totalMusics = listAudioClip.Length - 1;
    }

    // Stop or start the current music
    public void StartStopCurentMusic()
    {
        if (playCurrentMusic)
        {
            playCurrentMusic = false;
            audioSource.Stop();
        }
        else
        {
            playCurrentMusic = true;
            audioSource.Play();
        }
    }

    // Play the next song of the list and if there is no next one, use the first of the list
    public void PlayNextMusic()
    {
        Debug.Log("Play next music");
        Debug.Log(currentMusic);


        if (currentMusic + 1 > totalMusics)
        {
            currentMusic = 0;
        }
        else
        {
            currentMusic++;
        }

        audioSource.clip = listAudioClip[currentMusic];  
    }

    // Play the previous song of the list and if there is no next one, use the first of the list
    public void PlayPreviousMusic()
    {
        if (currentMusic - 1 < 0)
        {
            currentMusic = totalMusics;
        }
        else
        {
            currentMusic--;
        }

        audioSource.clip = listAudioClip[currentMusic];
    }

    public void ChangeVolume()
    {
        audioSource.volume = 0;
    }

}
