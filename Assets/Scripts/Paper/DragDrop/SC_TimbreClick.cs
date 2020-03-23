using UnityEngine;

// Manage the click on the envelope stamp

public class SC_TimbreClick : MonoBehaviour
{
    public string triggerName;

    // Tell the main script this stamp was chosen
    private void OnMouseUp()
    {
        if (!SC_TimbreChooser.instance.StampAlreadySelected)
        {
            SC_TimbreChooser.instance.selectedStamp = this.gameObject;
            SC_TimbreChooser.instance.ChooseStamp();
            SC_TimbreChooser.instance.anim.SetTrigger(triggerName);
        }
    }


}
