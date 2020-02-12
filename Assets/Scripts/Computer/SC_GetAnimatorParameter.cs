using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GetAnimatorParameter : MonoBehaviour
{
    public Animator animator;
    public string BoolToLookAt;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FlipFlop()
    {
        if (animator.GetBool(BoolToLookAt))
        {
            animator.SetBool(BoolToLookAt, false);
            return;
        }
        else
        {
            animator.SetBool(BoolToLookAt, true);
            return;
        }
    }

}
