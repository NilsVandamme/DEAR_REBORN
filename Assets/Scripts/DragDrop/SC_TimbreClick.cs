using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the click on the envelope stamp

public class SC_TimbreClick : MonoBehaviour
{
    // Tell the main script this stamp was chosen
    private void OnMouseUp()
    {
        SC_TimbreChooser.instance.selectedStamp = this.gameObject;
        SC_TimbreChooser.instance.ChooseStamp();
    }
}
