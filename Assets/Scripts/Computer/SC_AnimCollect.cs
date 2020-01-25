using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AnimCollect : MonoBehaviour
{
    public Animator anim;
    public bool CanBeOpenned;
    public bool OpenOrClosed;

    private void Start()
    {
        OpenOrClosed = true;
        CanBeOpenned = true;
        //SetCollectAnimBool();
        CanBeOpenned = false;
    }

    public void SetCollectAnimBool()
    {
        anim.ResetTrigger("Shake");

        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {
            if (OpenOrClosed == true)
            {
                anim.SetTrigger("Close");
                OpenOrClosed = false;
            }
            else
            {
                anim.SetTrigger("Open");
                OpenOrClosed = true;
            }
        }
        else
        {
            anim.SetTrigger("Shake");
        }
    }
}
