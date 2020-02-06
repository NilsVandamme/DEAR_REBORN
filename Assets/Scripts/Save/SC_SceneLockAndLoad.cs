using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SC_SceneLockAndLoad : MonoBehaviour
{
    public Button button;
    public Image image;
    public Sprite lockScene;
    public Sprite unlockScene;
    public string sceneToLoad;

    void Start()
    {
        // Load scene B1 and B2
        if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt"))
            image.sprite = unlockScene;
        else if (sceneToLoad.Equals("L_B1") && File.Exists(SC_GM_Master.gm.path + "L_B2" + ".txt"))
            image.sprite = unlockScene;
        else
            image.sprite = lockScene;

        button.onClick.AddListener(Load);
    }

    private void Load()
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
        SC_GM_Master.gm.infoPerso = saveObject.infoPerso;

        if (saveObject.infoParagrapheLettre != null)
            SC_GM_Master.gm.lastParagrapheLettrePerPerso = saveObject.infoParagrapheLettre;

        SC_LoadingScreen.Instance.LoadThisScene(sceneToLoad);
    }
}
