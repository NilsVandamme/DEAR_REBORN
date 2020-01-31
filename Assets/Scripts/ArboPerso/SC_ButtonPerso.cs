using UnityEngine;
using UnityEngine.UI;

public class SC_ButtonPerso : MonoBehaviour
{
    [HideInInspector]
    public int persoOfButton;

    public Button button;
    public Image image;
    public Sprite lockScene;
    public Sprite unlockScene;

    public GameObject ArboPerso;
    public GameObject PageInfoPerso;

    void Start()
    {
        if (SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton] != null && SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton].Count != 0)
            image.sprite = unlockScene;
        else
            image.sprite = lockScene;

        button.onClick.AddListener(AfficheInfoWindow);
    }

    private void AfficheInfoWindow()
    {
        if (SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton] != null && SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton].Count != 0)
        {
            SC_PageInfoPerso.instance.perso = persoOfButton;
            SC_PageInfoPerso.instance.icone = unlockScene;
            SC_PageInfoPerso.instance.Init();
            PageInfoPerso.SetActive(true);
            ArboPerso.SetActive(false);
        }
    }
}
