using System.Collections.Generic;
using UnityEngine;

// Game manager local, save important variables for this scene

public class SC_GM_Local : MonoBehaviour
{
    // Score personne courante
    [HideInInspector]
    public int peopleScore;

    // Nombre de CL récupérables
    [HideInInspector]
    public int numberOfCLRecover = 0;
    public int numberOfCLRecoverable;

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
    // Liste des mots entre par le joueur
    public List<SC_Word> choosenWordInLetter;
    [HideInInspector]
    public List<SC_Word> wheelOfWords;
    [HideInInspector]
    public List<SC_CLInPull> wordsInCollect = new List<SC_CLInPull>();
    [HideInInspector]
    public List<SC_CLInPull> wordsInPreparatory = new List<SC_CLInPull>();

    public static SC_GM_Local gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

    }

}
