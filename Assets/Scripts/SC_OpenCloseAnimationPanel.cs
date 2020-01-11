using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_OpenCloseAnimationPanel : MonoBehaviour
{
    public Animator myAnim;

    public void ToggleAnimator(){
        if(myAnim.GetBool("Opened")){
            myAnim.SetBool("Opened", false);
        }
        else{
            myAnim.SetBool("Opened", true);
        }
    }
}
