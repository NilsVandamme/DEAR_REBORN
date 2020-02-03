using UnityEngine;
using UnityEngine.UI;

// Calculate and store the score values, control the activation of the send button

public class SC_GM_Paper : MonoBehaviour
{
    public static SC_GM_Paper instance;

    [HideInInspector]
    public float score = 0; // Score result

    // Asset des mots
    public SC_PaperSnapGrid[] snapPositions; // All snap positions on the paper
    public Button SendButton; // The send button on the paper

    // Singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

    }

    // Get all snap positions
    private void Start()
    {
        snapPositions = FindObjectsOfType<SC_PaperSnapGrid>();
    }

    // Activate/Disable the send button
    public void Update()
    {
        // Activate the send letter button
        if(SC_ParagraphSorter.instance.SnappedParagraphs.Count >= 3 && SC_GM_Master.gm.choosenWordInLetter.Count > 0)
        {
            SendButton.interactable = true;
        }
        else
        {
            SendButton.interactable = false;
        }

        if(SC_ParagraphSorter.instance.SnappedParagraphs.Count >= 3)
        {
            SC_StagesAnim.instance.anim.SetTrigger("Paragraphs");
        }

        if(SC_GM_Master.gm.choosenWordInLetter.Count > 0)
        {
            SC_StagesAnim.instance.anim.SetTrigger("Words");
        }
    }

    // Calculate and store the score value
    public void CalculateScore()
    {
        score = 0;
        foreach (SC_InfoParagrapheLettreRemplie elem in SC_GM_Master.gm.choosenWordInLetter)
            score += elem.scoreParagraphe;
    }

    //******************************************************
    //********************  DEBUG  ***************************
    //******************************************************

    // Get and return the score
    public void DebugScore()
    {
        CalculateScore();
        Debug.Log("Score: " + score);
    }
}
