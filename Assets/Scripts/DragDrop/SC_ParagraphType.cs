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
            TypeIcon.color = Color.blue;
        }
        else if (Type == ParagraphType.WakeUp)
        {
            TypeIcon.color = Color.yellow;
        }
        else if (Type == ParagraphType.Motivation)
        {
            TypeIcon.color = Color.green;
        }
        else if (Type == ParagraphType.Clash)
        {
            TypeIcon.color = Color.red;
        }
    }
}
