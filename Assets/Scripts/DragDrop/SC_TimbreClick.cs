using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TimbreClick : MonoBehaviour
{
    private void OnMouseUp()
    {
        SC_TimbreChooser.instance.selectedStamp = this.gameObject;
        SC_TimbreChooser.instance.ChooseStamp();
    }
}
