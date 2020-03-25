using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SC_SceneLockAndLoad : MonoBehaviour
{
    public GameObject lockObject;
    public GameObject unlockObject;
    public string sceneToLoad;

    void Start()
    {
        if (SC_GM_Local.gm.firstScene == "L_3DMENU")
        {
            SC_LoadingScreen.Instance.LoadThisScene("L_3DMENU");
        }

        // Load scene B1 and B2
        if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt") || (sceneToLoad.Equals("L_B1") && File.Exists(SC_GM_Master.gm.path + "L_B2" + ".txt")))
        {
            lockObject.SetActive(false);
            unlockObject.SetActive(true);
        }
        else
        {
            lockObject.SetActive(true);
            unlockObject.SetActive(false);
        }
    }

    public void Load()
    {
        if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt"))
            UnParse(sceneToLoad);
        else if (sceneToLoad.Equals("L_B1") && File.Exists(SC_GM_Master.gm.path + "L_B2" + ".txt"))
            UnParse("L_B2");
    }

    private void UnParse(string sceneToLoad)
    {
        string save = File.ReadAllText(SC_GM_Master.gm.path + sceneToLoad + ".txt");
        SC_PlayerData saveObject = JsonUtility.FromJson<SC_PlayerData>(save);

        SC_GM_Master.gm.namePlayer = saveObject.namePlayer;
        SC_GM_Master.gm.wordsInPull = saveObject.wordsInPull;

        SC_GM_Master.gm.infoRecoltPerso = new List<string>[SC_GM_Master.gm.listChampsLexicaux.listOfPerso.Length];
        for (int i = 0; i < saveObject.infoPerso.Count; i++)
            SC_GM_Master.gm.infoRecoltPerso[i] = saveObject.infoPerso[i].infoRecoltPerso;

        if (saveObject.infoParagrapheLettre != null)
        {
            SC_GM_Master.gm.lastParagrapheLettrePerPerso = new SC_InfoParagrapheLettreRemplie[SC_GM_Master.gm.listChampsLexicaux.listOfPerso.Length][];
            for (int i = 0; i < saveObject.infoParagrapheLettre.Count; i++)
                SC_GM_Master.gm.lastParagrapheLettrePerPerso[i] = saveObject.infoParagrapheLettre[i].lettre;
        }

        SC_LoadingScreen.Instance.LoadThisScene(sceneToLoad);
    }
}
