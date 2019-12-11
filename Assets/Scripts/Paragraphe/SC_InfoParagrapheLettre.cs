using TMPro;
using UnityEngine;

public class SC_InfoParagrapheLettre : MonoBehaviour
{
    public SC_ParagrapheLettre textParagraphe;
    public TextMeshProUGUI myText;

    void Start()
    {
        string text = "";

        foreach (TextPart elem in textParagraphe.texte)
            text += elem.partText + " ";

        myText.text = text;
    }
}
