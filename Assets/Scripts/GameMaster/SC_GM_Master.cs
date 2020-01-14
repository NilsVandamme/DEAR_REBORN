using System.Collections.Generic;
using UnityEngine;

public class SC_GM_Master : MonoBehaviour
{
    // Ensemble des champs lexicaux
    public SC_ListChampLexicaux listChampsLexicaux;
    public SC_PullBase pullBase;
    public SC_BaseTimbre timbres;

    [HideInInspector]
    // Liste des mots choisi par le joueur (CL, Word)
    public List<SC_CLInPull> wordsInPull = new List<SC_CLInPull>();

    public static SC_GM_Master gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }

    public void Test()
    {
        foreach (SC_CLInPull elem in pullBase.wordsInPull)
            if (!wordsInPull.Contains(elem))
                wordsInPull.Add(elem);
    }
}
