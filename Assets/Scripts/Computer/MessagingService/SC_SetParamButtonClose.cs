using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SetParamButtonClose : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsClosingButtonShowing", true);
    }
}
