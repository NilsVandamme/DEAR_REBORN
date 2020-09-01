using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MailLinkButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PopUpCanvas;
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void SetActivePopUp()
    {
        anim.Play("AppearPopUp");
       

    }

    public void ClosePopUp()
    {
        anim.Play("DisappearPopUp");
        
    }
    
   
    public void TextHover()
    {
        
            SC_GM_Cursor.gm.changeToHoverCursor();
    }


    
    public void TextHoverExit()
    {
        SC_GM_Cursor.gm.changeToNormalCursor();
    }


}
