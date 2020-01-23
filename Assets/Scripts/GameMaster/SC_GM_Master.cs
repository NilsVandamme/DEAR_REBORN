using System.Collections.Generic;
using UnityEngine;

// Game manager master, save important variables between scenes 

public class SC_GM_Master : MonoBehaviour
{
    [HideInInspector]
    // Chemin des sauvegardes
    public string path = System.IO.Directory.GetCurrentDirectory() + "/Assets/Save/";

    // Ensemble des champs lexicaux
    public SC_ListChampLexicaux listChampsLexicaux;
    public SC_PullBase pullBase;
    public SC_BaseTimbre timbres;

    [HideInInspector]
    // Name Player
    public string namePlayer;
    [HideInInspector]
    // Liste des mots entre par le joueur
    public List<(SC_Word, float)> choosenWordInLetter;

    [HideInInspector]
    // Liste des mots choisi par le joueur (CL, Word)
    public List<SC_CLInPull> wordsInPull;

    public static SC_GM_Master gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

        if (gm.wordsInPull.Count == 0)
        {
            gm.wordsInPull = new List<SC_CLInPull>();

            foreach (SC_CLInPull elem in pullBase.wordsInBasePull)
                gm.wordsInPull.Add(elem);

           choosenWordInLetter = new List<(SC_Word, float)>();
        }
    }
}
