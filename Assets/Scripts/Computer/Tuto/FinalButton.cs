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
    private bool GetBoolTrigger;
    private bool Stop;
    private bool Stop2;


    void Update()
    {
        if (Stop && Stop2) return;
        
        GetBoolTrigger = trigger.CanFinish;
        Debug.Log(GetBoolTrigger);
        
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
    }
}
