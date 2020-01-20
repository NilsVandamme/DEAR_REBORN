using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_GM_Tooltip : MonoBehaviour
{
    public SimpleTooltip[] ttips;
    public bool tooltipActive;
    public Toggle tog;

    // Start is called before the first frame update
    void Start()
    {
        ttips = Resources.FindObjectsOfTypeAll<SimpleTooltip>();

        if (PlayerPrefs.HasKey("tooltip"))
        {
            //Debug.Log("key");
            int intvalue = PlayerPrefs.GetInt("tooltip");

            if (intvalue == 0)
            {
                tooltipActive = false;
                if (SceneManager.GetActiveScene().name == "L_00Menu")
                    tog.isOn = false;
            }
            else
            {
                tooltipActive = true;
                if (SceneManager.GetActiveScene().name == "L_00Menu")
                    tog.isOn = true;
            }
        }
        else
        {
            //Debug.Log("nokey");
            PlayerPrefs.SetInt("tooltip", 0);
            tooltipActive = false;
            if (SceneManager.GetActiveScene().name == "L_00Menu")
                tog.isOn = false;
        }

        if (tooltipActive)
        {
            for (int i = 0; i < ttips.Length; i++)
                ttips[i].enabled = true;
        }
        else
        {
            for (int i = 0; i < ttips.Length; i++)
                ttips[i].enabled = false;
        }
    }


    public void ActivateToolTip(bool value)
    {
        if (value)
            PlayerPrefs.SetInt("tooltip", 1);
        else
            PlayerPrefs.SetInt("tooltip", 0);
    }
}
