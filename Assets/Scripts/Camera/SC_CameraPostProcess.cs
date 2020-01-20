using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SC_CameraPostProcess : MonoBehaviour
{
    public PostProcessVolume[] pplist;
    public bool retroMode;
    public Toggle tog;

    // Start is called before the first frame update
    void Start()
    {
        pplist = GetComponents<PostProcessVolume>();

        if (PlayerPrefs.HasKey("retroMode"))
        {
            //Debug.Log("key");
            int intvalue = PlayerPrefs.GetInt("retroMode");

            if (intvalue == 0)
            {
                retroMode = false;
                if (SceneManager.GetActiveScene().name == "L_00Menu")
                    tog.isOn = false;
            }
            else
            {
                retroMode = true;
                if (SceneManager.GetActiveScene().name == "L_00Menu")
                    tog.isOn = true;
            }

            if (retroMode)
                pplist[0].enabled = true;
            else
                pplist[0].enabled = false;
        }
        else
        {
            //Debug.Log("nokey");
            PlayerPrefs.SetInt("retroMode", 0);
            retroMode = false;
            if (SceneManager.GetActiveScene().name == "L_00Menu")
                tog.isOn = false;
        }
    }

    public void retroModeChange(bool value)
    {
        pplist[0].enabled = value;

        if (value)
            PlayerPrefs.SetInt("retroMode", 1);
        else
            PlayerPrefs.SetInt("retroMode", 0);
    }
}
