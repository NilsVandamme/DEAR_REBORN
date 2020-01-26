﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the click on the envelope stamp

public class SC_TimbreClick : MonoBehaviour
{
    public string triggerName;


    // Tell the main script this stamp was chosen
    private void OnMouseUp()
    {
        Debug.Log(gameObject.name + " has been clicked");
        SC_TimbreChooser.instance.selectedStamp = this.gameObject;
        SC_TimbreChooser.instance.ChooseStamp();
    }


}
