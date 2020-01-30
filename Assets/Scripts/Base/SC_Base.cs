using UnityEngine;

[CreateAssetMenu(fileName = "Base.asset", menuName = "Custom/BaseInfo", order = 1)]
public class SC_Base : ScriptableObject
{
    public TextAsset fileCSVBase;

    [HideInInspector]
    public string titre;
    [HideInInspector]
    public string[] listOfGrammarCritere;
    [HideInInspector]
    public string[] listOfPerso;
}
