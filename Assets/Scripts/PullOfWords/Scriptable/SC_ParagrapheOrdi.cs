using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrdiParagraphe.asset", menuName = "Custom/ParagrapheOrdi", order = 1)]
public class SC_ParagrapheOrdi : ScriptableObject
{
    public TextAsset fileCSVTextParagraph;
    public List<TextPart> texte;

    public SC_ListChampLexicaux listChampLexicaux;
    public int[] champLexical = null;
    public bool[][] motAccepterInCL = null;

}
