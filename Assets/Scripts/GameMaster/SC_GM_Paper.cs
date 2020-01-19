using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_GM_Paper : MonoBehaviour
{
    [HideInInspector]
    // Score de la scene
    public int score = 0;

    // Asset des mots
    public bool paragraphsConfirmed;
    public SC_PaperSnapGrid[] snapPositions;
    public Button SendButton;
    //public SC_DragDropControls[] ddcontrols;
    [HideInInspector]
    public List<SC_AutoComplete> acompletes;

    private bool DebugMode;

    private void Start()
    {
        snapPositions = FindObjectsOfType<SC_PaperSnapGrid>();
        //ddcontrols = FindObjectsOfType<SC_DragDropControls>();
    }

    public void Update()
    {
        // Activate the send letter button
        if(SC_ParagraphSorter.instance.SnappedParagraphs.Count >= 3)
        {
            SendButton.interactable = true;
        }
        else
        {
            SendButton.interactable = false;
        }
    }

    public void CalculateScore()
    {
        foreach (SC_Word word in SC_GM_Local.gm.choosenWordInLetter)
            score += word.scorePerso[SC_GM_Local.gm.peopleScore];
    }

    public void CalculateScoresAndLoadNextScene()
    {
        Debug.Log("sumbit button - clicked");
       // Debug.Log("sumbit button - snapped paragrpahs = " + SC_ParagraphSorter.instance.SnappedParagraphs.Count);
        //if (paragraphsConfirmed == true)
        if(SC_ParagraphSorter.instance.SnappedParagraphs.Count >= 3)
        {
            //Debug.Log("sumbit button - 3 paragraphs snapped, calculating scores");
            if (!DebugMode)
            {
                CalculateScore();
            }

            Debug.Log("Score = " + score);
            if (score > SC_GM_Local.gm.firstPivotScene)
            {
                Debug.Log("Loaded first scene");
                if(!DebugMode)
                    SC_LoadingScreen.Instance.LoadThisScene(SC_GM_Local.gm.firstScene);
            }
            else if (SC_GM_Local.gm.numberOfScene == 2)
            {
                Debug.Log("Loaded second scene");
                if (!DebugMode)
                    SC_LoadingScreen.Instance.LoadThisScene(SC_GM_Local.gm.secondScene);
            }

            else if (score > SC_GM_Local.gm.secondPivotScene && SC_GM_Local.gm.numberOfScene == 3)
            {
                Debug.Log("Loaded second scene");
                if (!DebugMode)
                    SC_LoadingScreen.Instance.LoadThisScene(SC_GM_Local.gm.secondScene);
            }
            else
            {
                Debug.Log("Loaded third scene");
                if (!DebugMode)
                    SC_LoadingScreen.Instance.LoadThisScene(SC_GM_Local.gm.thirdScene);
            }

        }
    }

    public void OnClickLockButton()
    {
        if (paragraphsConfirmed == false)
        {
            Debug.Log("Blocked paragraphs placement");
            for (int i = 0; i < snapPositions.Length; i++)
                if (snapPositions[i].currentSnappedObject != null && !acompletes.Contains(snapPositions[i].currentSnappedObject.GetComponentInChildren<SC_AutoComplete>()))
                {
                    acompletes.Add(snapPositions[i].currentSnappedObject.GetComponentInChildren<SC_AutoComplete>());

                    for (int m = 0; m < acompletes.Count; m++)
                    {
                        //SC_ConfirmParagraphHighlight.instance.ChangeColor(true);
                        acompletes[m].enabled = true;
                    }

                }

            /*
            for (int k = 0; k < ddcontrols.Length; k++)
                ddcontrols[k].enabled = false;

            if (ddcontrols.Length != 0)
                //Debug.Log("no paragraphs were placed");
                paragraphsConfirmed = true;
                */
        }
        else
        {
            Debug.Log("Unblocked paragraphs placement");

            for (int j = 0; j < acompletes.Count; j++)
                acompletes[j].enabled = false;
            /*
            for (int l = 0; l < ddcontrols.Length; l++)
                ddcontrols[l].enabled = true;
                */
            acompletes.Clear();
            paragraphsConfirmed = false;
            //SC_ConfirmParagraphHighlight.instance.ChangeColor(false);
        }
    }

    public void DebugSubmit(int testScore)
    {
        DebugMode = true;
        paragraphsConfirmed = true;
        score = testScore;
        CalculateScoresAndLoadNextScene();
        DebugMode = false;
    }

    public void DebugScore()
    {
        CalculateScore();
        Debug.Log("Score: " + score);
    }
}
