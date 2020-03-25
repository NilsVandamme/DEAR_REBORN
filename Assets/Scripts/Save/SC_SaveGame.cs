using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SC_SaveGame : MonoBehaviour
{
    public void CalculateScore()
    {
        string nextScene = "Erreur";

        if (SC_ParagraphSorter.instance.SnappedParagraphs.Count >= 3)
        {
            SC_GM_Paper.instance.CalculateScore();

            if (SC_GM_Paper.instance.score < SC_GM_Local.gm.firstPivotScene)
            {
                nextScene = SC_GM_Local.gm.firstScene;
                SC_GM_Local.gm.finalAnimator = SC_GM_Local.gm.firstSceneAnimator;
            }
            else if (SC_GM_Local.gm.numberOfScene == 2 || (SC_GM_Paper.instance.score < SC_GM_Local.gm.secondPivotScene && SC_GM_Local.gm.numberOfScene == 3))
            {
                nextScene = SC_GM_Local.gm.secondScene;
                SC_GM_Local.gm.finalAnimator = SC_GM_Local.gm.secondSceneAnimator;
            }
            else
            {
                nextScene = SC_GM_Local.gm.thirdScene;
                SC_GM_Local.gm.finalAnimator = SC_GM_Local.gm.thirdSceneAnimator;
            }

        }

        Save(nextScene);
    }

    public void Save(string nextScene, bool firstSave = false)
    {
        if (nextScene.Equals("")) return;
    
        if (!firstSave) SortParagraphe();            

        SC_PlayerData saveObject = new SC_PlayerData(SC_GM_Master.gm.namePlayer, SC_GM_Master.gm.wordsInPull, SC_GM_Master.gm.lastParagrapheLettrePerPerso, SC_GM_Master.gm.infoPerso);

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SC_GM_Master.gm.path + nextScene + ".txt", json);
    }


    private void SortParagraphe()
    {
        SC_GM_Master.gm.lastParagrapheLettrePerPerso[SC_GM_Local.gm.persoOfCurrentScene] = new SC_InfoParagrapheLettreRemplie[3];

        SC_DragDropControls dragAndDrop;
        SC_AutoComplete autoComplete;

        for (int i = 0; i < SC_ParagraphSorter.instance.SnappedParagraphs.Count; i++)
        {
            dragAndDrop = SC_ParagraphSorter.instance.SnappedParagraphs[i].GetComponent<SC_DragDropControls>();
            autoComplete = SC_ParagraphSorter.instance.SnappedParagraphs[i].GetComponent<SC_AutoComplete>();
            
            for (int j = 0; j < SC_GM_Master.gm.choosenWordInLetter.Count; j++)
            {
                if (SC_GM_Master.gm.choosenWordInLetter[j].word.Equals(autoComplete.actualWord))
                {

                    if (dragAndDrop.top)
                        SC_GM_Master.gm.lastParagrapheLettrePerPerso[SC_GM_Local.gm.persoOfCurrentScene][0] = SC_GM_Master.gm.choosenWordInLetter[j];

                    else if (dragAndDrop.middle)
                        SC_GM_Master.gm.lastParagrapheLettrePerPerso[SC_GM_Local.gm.persoOfCurrentScene][1] = SC_GM_Master.gm.choosenWordInLetter[j];

                    else if (dragAndDrop.down)
                        SC_GM_Master.gm.lastParagrapheLettrePerPerso[SC_GM_Local.gm.persoOfCurrentScene][2] = SC_GM_Master.gm.choosenWordInLetter[j];

                    else
                        Debug.LogError("Probleme sur la position du paragraphe de la lettre");

                    break;
                }
            }
        }
    }
}
