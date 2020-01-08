using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TextPart
{
    public string partText;
}

[CreateAssetMenu(fileName = "Paragraphe.asset", menuName = "Custom/ParagrapheLettre", order = 1)]
public class SC_ParagrapheLettre : ScriptableObject
{
    public TextAsset fileCSVTextParagraph;
    public List<TextPart> texte;
}
