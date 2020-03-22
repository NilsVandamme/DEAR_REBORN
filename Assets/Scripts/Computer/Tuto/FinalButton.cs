using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject Stampy;
    public TriggerEndScript trigger;
    private bool Stop;
    private bool Stop2;

    void Update()
    {
        if (SC_GM_Local.gm.numberOfCLRecover == 3)
        {
            if (!trigger)
            {
                if (!Stop)
                {
                    Button.GetComponent<Button>().interactable = false;
                    Stop = true;
                }
            }

            if (trigger)
            {
                if(!Stop2)
                {
                    Button.GetComponent<Button>().interactable = true;
                    Stampy.SetActive(true);

                }
            }
        }
    }
}
