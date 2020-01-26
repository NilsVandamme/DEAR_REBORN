using UnityEngine;
using UnityEngine.UI;

// Gives the theme of the paragraph

public class SC_ParagraphType : MonoBehaviour
{
    public enum ParagraphType { Motivation, Clash, Orientation, WakeUp };
    public ParagraphType Type;

    public float multiplicativeScore;

    private Image TypeIcon;

    // Start is called before the first frame update
    void Start()
    {
        TypeIcon = GetComponentInChildren<Image>();

        if (Type == ParagraphType.Orientation)
        {
            TypeIcon.color = new Color(0.1135636f, 0.6407289f, 0.8301887f,1);
        }
        else if (Type == ParagraphType.WakeUp)
        {
            TypeIcon.color = new Color(0.9245283f, 0.7347588f, 0.1962442f, 1);
        }
        else if (Type == ParagraphType.Motivation)
        {
            TypeIcon.color = new Color(0.9622642f, 0.4685875f, 0.2133321f, 1);
        }
        else if (Type == ParagraphType.Clash)
        {
            TypeIcon.color = new Color(0.8584906f, 0.2634961f, 0.1903257f, 1);
        }
    }
}
