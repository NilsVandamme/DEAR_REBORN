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
        //// Load scene B1 and B2
        //if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt"))
        //    image.sprite = unlockScene;
        //else if (sceneToLoad.Equals("L_B1"))
        //    if (File.Exists(SC_GM_Master.gm.path + "L_B2" + ".txt"))
        //        image.sprite = unlockScene;
        //    else
        //        image.sprite = lockScene;
        //else
        //    image.sprite = lockScene;

        image.sprite = unlockScene;
        button.onClick.AddListener(Load);
    }

    private void Load()
    {
        SC_LoadingScreen.Instance.LoadThisScene(sceneToLoad);

    //    if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt"))
    //    {
    //        string save = File.ReadAllText(SC_GM_Master.gm.path + sceneToLoad + ".txt");
    //        SC_PlayerData saveObject = JsonUtility.FromJson<SC_PlayerData>(save);

    //        SC_GM_Master.gm.namePlayer = saveObject.namePlayer;
    //        SC_GM_Master.gm.wordsInPull = saveObject.wordsInPull;
    //        SC_GM_Master.gm.timbres.timbres = saveObject.timbre;

    //        SC_LoadingScreen.Instance.LoadThisScene(sceneToLoad);

    //    }
    //    else if (sceneToLoad.Equals("L_B1"))
    //        if (File.Exists(SC_GM_Master.gm.path + "L_B2" + ".txt"))
    //        {
    //            string save = File.ReadAllText(SC_GM_Master.gm.path + "L_B2" + ".txt");
    //            SC_PlayerData saveObject = JsonUtility.FromJson<SC_PlayerData>(save);

    //            SC_GM_Master.gm.namePlayer = saveObject.namePlayer;
    //            SC_GM_Master.gm.wordsInPull = saveObject.wordsInPull;
    //            SC_GM_Master.gm.timbres.timbres = saveObject.timbre;

    //            SC_LoadingScreen.Instance.LoadThisScene("L_B2");

    //        }
    }
}
