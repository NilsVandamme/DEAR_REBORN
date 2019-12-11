using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Word
{
    public int[] scorePerso;
    public string titre;
    public string[] grammarCritere;
}

[CreateAssetMenu(fileName = "ChampLexical.asset", menuName = "Custom/GenerateChampLexical", order = 1)]
public class SC_ChampLexical : ScriptableObject
{
    public TextAsset fileCSVChampLexical;
    public List<Word> listOfWords;
}
