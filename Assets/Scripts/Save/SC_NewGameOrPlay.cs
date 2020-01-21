using UnityEngine;
using UnityEngine.UI;

public class SC_NewGameOrPlay : MonoBehaviour
{
    private string[] files;

    public Button newGame;
    public Button play;

    // Start is called before the first frame update
    void Start()
    {
        files = System.IO.Directory.GetFiles(SC_GM_Master.gm.path);

        if (files == null || files.Length == 0)
        {
            newGame.gameObject.SetActive(true);
            play.gameObject.SetActive(false);
        }
        else
        {
            newGame.gameObject.SetActive(false);
            play.gameObject.SetActive(true);
        }
    }

}
