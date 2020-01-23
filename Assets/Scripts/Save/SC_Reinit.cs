using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SC_Reinit : MonoBehaviour
{
    public Button but;

    void Start()
    {
        but.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        string[] files = System.IO.Directory.GetFiles(SC_GM_Master.gm.path);
        foreach (string path in files)
            File.Delete(path);

        SC_NewGameOrPlay.gm.LoadMenu();
    }
}
