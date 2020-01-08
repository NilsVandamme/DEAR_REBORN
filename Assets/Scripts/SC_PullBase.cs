using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCLAndWordsInPull.asset", menuName = "Custom/BaseCLAndWordsInPull", order = 1)]
public class SC_PullBase : ScriptableObject
{
    public List<SC_CLInPull> wordsInPull;
    public SC_ListChampLexicaux listChampLexicaux;
    public bool[] foldoutList;
    public bool[][] motAccepterInCL;
}
