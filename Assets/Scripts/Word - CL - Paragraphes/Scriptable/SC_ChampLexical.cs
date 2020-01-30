using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WordAndBase
{
    public SC_Word word;
    public ScriptableObject baseInfo;
}

[CreateAssetMenu(fileName = "ChampLexical.asset", menuName = "Custom/ChampLexical", order = 1)]
public class SC_ChampLexical : ScriptableObject
{
    public TextAsset fileCSVChampLexical;
    public ScriptableObject fileBase;
    public List<SC_Word> listOfWords;
}
