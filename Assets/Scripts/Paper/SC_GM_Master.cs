using System.Collections.Generic;
using UnityEngine;

// Game manager master, save important variables between scenes 

public class SC_GM_Master : MonoBehaviour
{
    // Ensemble des champs lexicaux
    public SC_ListChampLexicaux listChampsLexicaux;
    public SC_PullBase pullBase;



    [HideInInspector]
    public string path; // Chemin des sauvegardes

    [HideInInspector]
    public string namePlayer; // Name Player

    [HideInInspector]
    public SC_InfoParagrapheLettreRemplie[][] lastParagrapheLettrePerPerso; // tableau des Listes des mots et texte entre par le joueur

    [HideInInspector]
    public List<SC_InfoParagrapheLettreRemplie> choosenWordInLetter; // Liste des mots entre par le joueur

    [HideInInspector]
    public List<SC_CLInPull> wordsInPull; // Liste des mots choisi par le joueur (CL, Word)

    [HideInInspector]
    public List<string>[] infoPerso; // Liste des infos récolté sur les joueurs



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
            lastParagrapheLettrePerPerso = new SC_InfoParagrapheLettreRemplie[listChampsLexicaux.listOfPerso.Length][];

            infoPerso = new List<string>[listChampsLexicaux.listOfPerso.Length];
            for (int i = 0; i < listChampsLexicaux.listOfPerso.Length; i++)
                infoPerso[i] = new List<string>();

            foreach (SC_CLInPull elem in pullBase.wordsInBasePull)
                gm.wordsInPull.Add(elem);

            choosenWordInLetter = new List<SC_InfoParagrapheLettreRemplie>();
        }
    }
}
