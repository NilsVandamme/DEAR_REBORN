using System.Collections.Generic;
using UnityEngine;

// Game manager master, save important variables between scenes 

public class SC_GM_Master : MonoBehaviour
{
    [HideInInspector]
    // Chemin des sauvegardes
    public string path;

    // Ensemble des champs lexicaux
    public SC_ListChampLexicaux listChampsLexicaux;
    public SC_PullBase pullBase;

    [HideInInspector]
    // Name Player
    public string namePlayer;

    [HideInInspector]
    // tableau des Listes des mots et texte entre par le joueur
    public List<SC_InfoParagrapheLettreRemplie>[] lastParagrapheLettrePerPerso;
    [HideInInspector]
    // Liste des mots entre par le joueur
    public List<SC_InfoParagrapheLettreRemplie> choosenWordInLetter;

    [HideInInspector]
    // Liste des mots choisi par le joueur (CL, Word)
    public List<SC_CLInPull> wordsInPull;

    public static SC_GM_Master gm = null;

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save/";
        if (!System.IO.Directory.Exists(path))
            System.IO.Directory.CreateDirectory(path);

        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

        if (gm.wordsInPull.Count == 0)
        {
            gm.wordsInPull = new List<SC_CLInPull>();
            lastParagrapheLettrePerPerso = new List<SC_InfoParagrapheLettreRemplie>[listChampsLexicaux.listOfPerso.Length];

            foreach (SC_CLInPull elem in pullBase.wordsInBasePull)
                gm.wordsInPull.Add(elem);

            choosenWordInLetter = new List<SC_InfoParagrapheLettreRemplie>();
        }
    }
}
