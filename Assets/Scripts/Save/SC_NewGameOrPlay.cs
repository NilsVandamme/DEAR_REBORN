using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_NewGameOrPlay : MonoBehaviour
{
    private string[] files;

    public Button newGamePlay;
    public Button newGame;
    public Button play;
    public TextMeshProUGUI text;

    public static SC_NewGameOrPlay gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

        text.text = "ok";
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = "pass";
        LoadMenu();
    }

    public void LoadMenu()
    {
        text.text = "test";
        //text.text = SC_GM_Master.gm.path;
        files = System.IO.Directory.GetFiles(SC_GM_Master.gm.path);
        text.text = files.Length.ToString();

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
