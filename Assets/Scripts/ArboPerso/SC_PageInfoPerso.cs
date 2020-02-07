using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PageInfoPerso : MonoBehaviour
{
    // Left Part
    public Image iconeImage;
    public TextMeshProUGUI namePerso;
    public TextMeshProUGUI infoPerso;

    // Right Part
    public GameObject lettre;
    public Sprite plus;
    public Sprite moins;

    private Image[] plusMoins;
    private TextMeshProUGUI[] paragraphe;

    // info passer depuis SC_ButtonPerso
    [HideInInspector]
    public int perso;
    [HideInInspector]
    public Sprite icone;

    public static SC_PageInfoPerso instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        paragraphe = lettre.GetComponentsInChildren<TextMeshProUGUI>(true);
        plusMoins = lettre.GetComponentsInChildren<Image>(true);

        RightPart();
        LeftPart();
    }


    public void Init(int persoOfButton, Sprite unlockScene)
    {
        perso = persoOfButton;
        icone = unlockScene;
    }

    private void RightPart()
    {
        for (int i = 0; i < plusMoins.Length; i++)
        {
            if (SC_GM_Master.gm.lastParagrapheLettrePerPerso[perso][i].scoreParagraphe > 0)
                plusMoins[i].sprite = plus;
            else
                plusMoins[i].sprite = moins;

            paragraphe[i].text = SC_GM_Master.gm.lastParagrapheLettrePerPerso[perso][i].textParagraphe;
        }
    }

    private void LeftPart ()
    {
        iconeImage.sprite = icone;
        namePerso.text = SC_GM_Master.gm.listChampsLexicaux.listOfPerso[perso];

        infoPerso.text = "";
        for (int i = 0; i < SC_GM_Master.gm.infoPerso[SC_GM_Local.gm.persoOfCurrentScene].Count; i++)
            infoPerso.text += SC_GM_Master.gm.infoPerso[SC_GM_Local.gm.persoOfCurrentScene][i] + "\n\n";

    }


}
