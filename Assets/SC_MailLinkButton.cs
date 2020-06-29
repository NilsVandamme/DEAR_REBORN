using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MailLinkButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PopUp;
    void Start()
    {
        
    }

    public void SetActivePopUp()
    {
        PopUp.SetActive(true);

    }

    public void ClosePopUp()
    {
        PopUp.SetActive(false);
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
