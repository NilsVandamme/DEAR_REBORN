﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// Manage the postprocesses

public class SC_CameraPostProcess : MonoBehaviour
{
    public PostProcessVolume[] pplist; // List of postprocess components on the camera
    public bool retroMode; // Is b&w postproc active ?
    public Toggle tog; // The toggle from the menu options (no ref needed in other scenes) 
    public ColorGrading colorGradingLayer = null;

    void Start()
    {
        // Get all postproc components
        pplist = GetComponents<PostProcessVolume>();

        pplist[0].profile.TryGetSettings(out colorGradingLayer);

        // Get the saved option from the playerprefs
        if (PlayerPrefs.HasKey("retroMode"))
        {
            //Debug.Log("key");
            int intvalue = PlayerPrefs.GetInt("retroMode");

            if (intvalue == 0)
            {
                retroMode = false;
                tog.isOn = false;
            }
            else
            {
                retroMode = true;
                tog.isOn = true;
            }

            ActivateRetroMode();
        }
        else
        {
            //Debug.Log("nokey");
            PlayerPrefs.SetInt("retroMode", 0);
            retroMode = false;
            tog.isOn = false;
        }
    }

    //Activate the retro mode and save in the playerprefs
    public void RetroModeChange(bool value)
    {
        colorGradingLayer.active = value;

        if (value)
        {
            PlayerPrefs.SetInt("retroMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("retroMode", 0);
        }

        //Debug.Log("retro mode " + value);
    }

    // Activate or disable the b&w postproc
    public void ActivateRetroMode()
    {
        if (retroMode)
            colorGradingLayer.active = true;
        else
            colorGradingLayer.active = false;

        //Debug.Log("postprocesss active");
    }
}
