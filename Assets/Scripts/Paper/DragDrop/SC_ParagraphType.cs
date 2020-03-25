using UnityEngine;
using UnityEngine.UI;

// Gives the theme of the paragraph

public class SC_ParagraphType : MonoBehaviour
{
    public enum ParagraphType { Motivation, Clash, Orientation, WakeUp };
    public ParagraphType Type;

    public Sprite OrientationColor;
    public Sprite WakeupColor;
    public Sprite MotivationColor;
    public Sprite ClashColor;

    public float multiplicativeScore;

    public SpriteRenderer TypeIcon;

    // Start is called before the first frame update
    void Start()
    {

        if (Type == ParagraphType.Orientation)
        {
            TypeIcon.sprite = OrientationColor;
        }
        else if (Type == ParagraphType.WakeUp)
        {
            TypeIcon.sprite = WakeupColor;
        }
        else if (Type == ParagraphType.Motivation)
        {
            TypeIcon.sprite = MotivationColor;
        }
        else if (Type == ParagraphType.Clash)
        {
            TypeIcon.sprite = ClashColor;
        }
    }
}
