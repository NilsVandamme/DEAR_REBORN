using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is to demonstrate how SimpleTooltips can be added or changed dynamically.
 * If you don't want to do any programming then don't worry! You can ignore this file.
 */
public class STDynamicExample : MonoBehaviour
{
    public SimpleTooltipStyle dynamicTooltipStyle;
    public string tooltipText = "";
    public string tooltipText2 = "";
    public string[] tooltipTexts;
    int tooltipNB;

    public void AddTooltip()
    {
        // So first lets check if we already have a tooltip
        var tooltip = GetComponent<SimpleTooltip>();
        if (tooltip && tooltipTexts.Length>tooltipNB)
        {
            tooltip.infoLeft = tooltipTexts[tooltipNB];
            tooltipNB++;

            // Forces to start showing instead of waiting for the mouse to enter the collider
            tooltip.ShowTooltip();
        }
        else if (tooltip && tooltipTexts.Length == tooltipNB){
            print("event de fin de texte");
        }
        // We don't have a tooltip so lets add a new one!
        else if(!tooltip)
        {
            tooltip = gameObject.AddComponent<SimpleTooltip>();
            tooltip.infoLeft = tooltipTexts[0];
            tooltipNB++;
            tooltip.ShowTooltip();


            // If you wish, you may also change the style too, or else it will use the default one
            if (dynamicTooltipStyle)
                tooltip.simpleTooltipStyle = dynamicTooltipStyle;
        }
    }
}
