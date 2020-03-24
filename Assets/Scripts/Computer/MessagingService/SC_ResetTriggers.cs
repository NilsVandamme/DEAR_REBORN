using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ResetTriggers : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset all state to avoid bugs
        animator.ResetTrigger("Normal");
        animator.ResetTrigger("Normal2");
        animator.ResetTrigger("Normal3");
        animator.ResetTrigger("Normal4");

        animator.ResetTrigger("Highlighted");
        animator.ResetTrigger("Highlighted2");
        animator.ResetTrigger("Highlighted3");
        animator.ResetTrigger("Highlighted4");
    }
}
