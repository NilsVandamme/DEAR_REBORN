using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public static AudioClip[] listAudioClip;

    public static AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //electricVibration = Resources.Load<AudioClip>("Audio/electricVibration");

        // Give the possibility to control differents audio source on the same object
        audioSource = GetComponent<AudioSource>();
    }

    // Pour activer un son depuis une autre classe : SoundManagerScript.PlaySound("nomDuSon");
    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "electricShock":
                //audioSource.PlayOneShot("", 1f);
                break;

            default:
                break;
        }
    }

}
