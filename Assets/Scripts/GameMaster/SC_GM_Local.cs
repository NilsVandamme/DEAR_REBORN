using System.Collections.Generic;
using UnityEngine;

// Game manager local, save important variables for this scene

public class SC_GM_Local : MonoBehaviour
{
    // Score personne courante
    [HideInInspector]
    public int persoOfCurrentScene;

    // Nombre de CL récupérables
    [HideInInspector]
    public int numberOfCLRecover = 0;
    [HideInInspector]
    public int numberOfCLRecoverable;

    // Mail a afficher avec les mots de la scene precedente
    public SC_InfoParagrapheOrdi paragrapheMailCache;
    public List<string> wordsNeedsForPrintMailParagraphes = new List<string>();

    // Prochaines Scenes
    [HideInInspector]
    public int numberOfScene;
    [HideInInspector]
    public int firstPivotScene;
    [HideInInspector]
    public int secondPivotScene;
    [HideInInspector]
    public string firstScene;
    [HideInInspector]
    public string secondScene;
    [HideInInspector]
    public string thirdScene;

    [HideInInspector]
    public List<SC_Word> wheelOfWords;
    [HideInInspector]
    public List<SC_CLInPull> wordsInCollect = new List<SC_CLInPull>();
    [HideInInspector]
    public List<SC_CLInPull> wordsInPreparatory = new List<SC_CLInPull>();
    
    public static SC_GM_Local gm = null;

    private void Awake()
    {
        numberOfCLRecoverable = 3;

        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

    }

    private void Start()
    {
        foreach (SC_InfoParagrapheLettreRemplie elem in SC_GM_Master.gm.choosenWordInLetter)
            foreach (string mot in wordsNeedsForPrintMailParagraphes)
                if (elem.word.titre.Equals(mot))
                    paragrapheMailCache.affichable = true;

        SC_GM_Master.gm.choosenWordInLetter = new List<SC_InfoParagrapheLettreRemplie>();
    }

}
