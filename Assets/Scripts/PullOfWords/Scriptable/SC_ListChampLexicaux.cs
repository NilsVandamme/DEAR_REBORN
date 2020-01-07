using UnityEngine;

[CreateAssetMenu(fileName = "ListChampLexicaux.asset", menuName = "Custom/ListChampLexicaux", order = 1)]
public class SC_ListChampLexicaux : ScriptableObject
{
    public SC_ChampLexical[] listChampLexical = null;

    [HideInInspector]
    public string[] listNameChampLexical;


    [HideInInspector]
    public string titre;
    [HideInInspector]
    public string[] listOfGrammarCritere;
    [HideInInspector]
    public string[] listOfPerso;

}
