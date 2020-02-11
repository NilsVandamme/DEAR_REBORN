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
        if (SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton] != null && SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton].Length != 0)
            image.sprite = unlockScene;
        else
            image.sprite = lockScene;

        button.onClick.AddListener(AfficheInfoWindow);
    }

    private void AfficheInfoWindow()
    {
        if (SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton] != null && SC_GM_Master.gm.lastParagrapheLettrePerPerso[persoOfButton].Length != 0)
        {
            PageInfoPerso.SetActive(true);
            ArboPerso.SetActive(false);
            SC_PageInfoPerso.instance.Init(persoOfButton, unlockScene);
        }
    }
}
