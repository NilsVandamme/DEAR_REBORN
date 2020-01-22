﻿using System.IO;
using UnityEngine;

public class SC_SaveGame : MonoBehaviour
{
    public void CalculateScore()
    {
        string nextScene = "Erreur";

        if (SC_ParagraphSorter.instance.SnappedParagraphs.Count >= 3)
        {
            SC_GM_Paper.instance.CalculateScore();

            if (SC_GM_Paper.instance.score > SC_GM_Local.gm.firstPivotScene)
                nextScene = SC_GM_Local.gm.firstScene;
            else if (SC_GM_Local.gm.numberOfScene == 2)
                nextScene = SC_GM_Local.gm.secondScene;
            else if (SC_GM_Paper.instance.score > SC_GM_Local.gm.secondPivotScene && SC_GM_Local.gm.numberOfScene == 3)
                nextScene = SC_GM_Local.gm.secondScene;
            else
                nextScene = SC_GM_Local.gm.thirdScene;

        }

        Save(nextScene);
    }

    public void Save(string nextScene)
    {
        if (nextScene.Equals(""))
            return;

        SC_PlayerData saveObject = new SC_PlayerData(SC_GM_Master.gm.namePlayer, SC_GM_Master.gm.wordsInPull, SC_GM_Master.gm.timbres.timbres);
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SC_GM_Master.gm.path + nextScene + ".txt", json);
    }
}