using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject Stampy;
    public TriggerEndScript trigger;
    public GameObject StampyStartTuto;
    public SC_Messaging_Service BossCall;
    private bool GetBoolTrigger;
    private bool Stop;
    private bool Stop2;
    private bool Stop3;


    void Update()
    {
        Debug.Log("finalbutton update is running | msgsystem status = " + BossCall.CanStartTuto );

        if (Stop && Stop2) return;
        if (Stop3) return;
        
        GetBoolTrigger = trigger.CanFinish;
        
        if (SC_GM_Local.gm.numberOfCLRecover == 3)
        {
            if (!GetBoolTrigger)
            {
                if (!Stop)
                {
                    Button.SetActive(true);
                    Stop = true;
                }
            }

            if (GetBoolTrigger)
            {
                if(!Stop2)
                {
                    Button.SetActive(false);
                    Stampy.SetActive(true);
                    Stop2 = true;
                }
            }
        }

        if (BossCall.CanStartTuto)
        {
            Debug.Log("canstarttuto received");
            if (!Stop3)
            {
                StampyStartTuto.SetActive(true);
                Stop3 = true;
            }
        }
    }
}
