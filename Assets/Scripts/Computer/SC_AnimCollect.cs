using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AnimCollect : MonoBehaviour
{
    public Animator anim;
    public bool OpenOrClosed;

    private void Start()
    {
        OpenOrClosed = true;
        SetCollectAnimBool();
    }

    public void SetCollectAnimBool()
    {
        if(OpenOrClosed == true)
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
}
