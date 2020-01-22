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
        if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt"))
            image.sprite = unlockScene;
        else
            image.sprite = lockScene;

        button.onClick.AddListener(Load);
    }

    private void Load()
    {
        if (File.Exists(SC_GM_Master.gm.path + sceneToLoad + ".txt"))
        {
            string save = File.ReadAllText(SC_GM_Master.gm.path + sceneToLoad + ".txt");
            SC_PlayerData saveObject = JsonUtility.FromJson<SC_PlayerData>(save);

            SC_GM_Master.gm.namePlayer = saveObject.namePlayer;
            SC_GM_Master.gm.wordsInPull = saveObject.wordsInPull;
            SC_GM_Master.gm.timbres.timbres = saveObject.timbre;

            SC_LoadingScreen.Instance.LoadThisScene(sceneToLoad);

        }
    }
}
