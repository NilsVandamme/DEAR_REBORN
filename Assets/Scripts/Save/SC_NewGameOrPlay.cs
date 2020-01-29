using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_NewGameOrPlay : MonoBehaviour
{
    private string[] files;

    public Button newGamePlay;
    public Button newGame;
    public Button play;

    public static SC_NewGameOrPlay gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        files = System.IO.Directory.GetFiles(SC_GM_Master.gm.path);

        if (files == null || files.Length == 0)
        {
            newGame.gameObject.SetActive(true);
            newGamePlay.gameObject.SetActive(false);
            play.gameObject.SetActive(false);
        }
        else
        {
            newGamePlay.gameObject.SetActive(false);
            newGame.gameObject.SetActive(false);
            play.gameObject.SetActive(true);
        }
    }

}
