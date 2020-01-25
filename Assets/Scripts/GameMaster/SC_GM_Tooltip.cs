using UnityEngine;
using UnityEngine.UI;

// Manage the tooltips

public class SC_GM_Tooltip : MonoBehaviour
{
    public SimpleTooltip[] ttips; // All tooltips elements
    public bool tooltipActive; // Are the tooltips active ?
    public Toggle tog; // Toggle from the main menu (no ref needed in game scenes)


    void Start()
    {
        // Get all tooltip components
        ttips = Resources.FindObjectsOfTypeAll<SimpleTooltip>();

        // Find in the playerprefs wether tooltips are turned on or off
        if (PlayerPrefs.HasKey("tooltip"))
        {
            int intvalue = PlayerPrefs.GetInt("tooltip");

            if (intvalue == 0)
            {
                tooltipActive = false;
                tog.isOn = false;
            }
            else
            {
                tooltipActive = true;
                tog.isOn = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("tooltip", 0);
            tooltipActive = false;
            tog.isOn = false;
        }

        TurnOn();
    }

    // Change the state of the tooltips option in the playerprefs
    public void ActivateToolTip(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt("tooltip", 1);
            tooltipActive = true;
        }
        else
        {
            PlayerPrefs.SetInt("tooltip", 0);
            tooltipActive = false;
        }

        TurnOn();
    }

    // Turn or off on all tooltips
    public void TurnOn()
    {
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
}
