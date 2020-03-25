using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PageInfoPerso : MonoBehaviour
{
    // Left Part
    public Image iconeImage;
    public TextMeshProUGUI namePerso;
    public TextMeshProUGUI descPerso;
    public TextMeshProUGUI infoPerso;
    public TextMeshProUGUI infoRecoltPerso;

    // Right Part
    public GameObject lettre;
    
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
        for (int i = 0; i < SC_GM_Master.gm.lastParagrapheLettrePerPerso[perso].Length; i++)
        { 
            paragraphe[i + 1].text = SC_GM_Master.gm.lastParagrapheLettrePerPerso[perso][i].textParagraphe;
            paragraphe[i + 1].color = (SC_GM_Master.gm.lastParagrapheLettrePerPerso[perso][i].scoreParagraphe > 0) ? (Color.green) : (Color.red);
        }
    }

    private void LeftPart ()
    {
        iconeImage.sprite = icone;
        namePerso.text = SC_GM_Master.gm.listChampsLexicaux.listOfPerso[perso];
        descPerso.text = SC_GM_Master.gm.descriptionPerso[perso];
        infoPerso.text = SC_GM_Master.gm.infoPerso[perso];

        infoRecoltPerso.text = "";
        for (int i = 0; i < SC_GM_Master.gm.infoRecoltPerso[perso].Count; i++)
            infoRecoltPerso.text += SC_GM_Master.gm.infoRecoltPerso[perso][i] + "\n\n";

    }


}
