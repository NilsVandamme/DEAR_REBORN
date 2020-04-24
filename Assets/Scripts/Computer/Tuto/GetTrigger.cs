using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTrigger : MonoBehaviour
{
    public Animator anim;

    public void SetTrigger()
    {
        anim.SetTrigger("Normal");
    }
}
