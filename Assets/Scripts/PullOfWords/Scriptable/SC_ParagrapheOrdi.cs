using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrdiParagraphe.asset", menuName = "Custom/ParagrapheOrdi", order = 1)]
public class SC_ParagrapheOrdi : ScriptableObject
{
    public SC_ListChampLexicaux listChampLexicaux;
    public bool foldoutList;
    public int champLexical;
    public bool[] motAccepterInCL = null;
}
