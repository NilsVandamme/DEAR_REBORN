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
            #region Player sounds

            case "playerDeath":
                float rand0 = Random.value;
                if (rand0 <= 0.33f)
                {
                    generalAudioSource.PlayOneShot(playerDeath, 5);
                }
                else if (rand0 <= 0.66f)
                {
                    generalAudioSource.PlayOneShot(playerDeath, 5);
                }
                else if (rand0 <= 1f)
                {
                    generalAudioSource.PlayOneShot(playerDeath, 5);
                }
                break;

            case "playerJump":
                float rand = Random.value;
                if (rand <= 0.33f)
                {
                    generalAudioSource.PlayOneShot(playerJump, 5);
                }
                else if (rand <= 0.66f)
                {
                    generalAudioSource.PlayOneShot(playerJump2, 5);
                }
                else if (rand <= 1f)
                {
                    generalAudioSource.PlayOneShot(playerJump3, 5);
                }
                break;

            case "playerLanding":
                generalAudioSource.PlayOneShot(playerLanding, 5);
                break;

            case "playerWalk":
                float rand1 = Random.value;
                if (rand1 <= 0.25f)
                {
                    generalAudioSource.PlayOneShot(playerWalk, 15);
                }
                else if (rand1 <= 0.5f)
                {
                    generalAudioSource.PlayOneShot(playerWalk2, 15);
                }
                else if (rand1 <= 0.75f)
                {
                    generalAudioSource.PlayOneShot(playerWalk3, 15);
                }
                else if (rand1 <= 1)
                {
                    generalAudioSource.PlayOneShot(playerWalk4, 15);
                }

                break;

            #endregion Player sounds

            #region Sounds made when the player interact with objects

            case "activateLever":
                generalAudioSource.PlayOneShot(activateLever, 1f);
                break;

            case "electricShock":
                generalAudioSource.PlayOneShot(electricShock, 1f);
                break;

            case "getKey":
                generalAudioSource.PlayOneShot(getKey, 10f);
                break;

            case "objectPush":
                repeatableAudioSource.Stop();
                repeatableAudioSource.PlayOneShot(objectPush, 5f);
                break;
            case "objectPull":
                repeatableAudioSource.Stop();
                repeatableAudioSource.PlayOneShot(objectPull, 5f);
                break;

            case "unlockDoor":
                generalAudioSource.PlayOneShot(unlockDoor, 3f);
                break;

            case "throwObject":
                generalAudioSource.PlayOneShot(throwObject, 5f);
                break;

            case "launchFan":
                generalAudioSource.PlayOneShot(launchFan, 1f);
                break;

            case "launchVacuum":
                generalAudioSource.PlayOneShot(launchVacuum, 1f);
                break;

            case "swapWorld":
                generalAudioSource.PlayOneShot(swapWorld, 5f);
                break;

            #endregion Sounds made when the player interact with objects

            #region Sounds made by the objects

            case "ambientFan":
                generalAudioSource.PlayOneShot(ambientFan, 1f);
                break;

            case "ambientVacuum":
                generalAudioSource.PlayOneShot(ambientVacuum, 1f);
                break;

            case "breakBlock":
                generalAudioSource.PlayOneShot(breakBlock, 1f);
                break;

            case "resizableObject":
                generalAudioSource.PlayOneShot(resizableObject, 7f);
                break;

            case "openDoor":
                generalAudioSource.PlayOneShot(openDoor, 3f);
                break;

            case "electricVibration":
                //generalAudioSource.PlayOneShot(electricVibration, 1f);
                break;

            #endregion Sounds made by the objects

            default:
                break;
        }
    }

}
