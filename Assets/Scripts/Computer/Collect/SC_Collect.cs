using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage the collect panel on the computer

public class SC_Collect : MonoBehaviour
{
    public GameObject diode;
    public Sprite diodeON;
    public Sprite diodeOFF;
    public Sprite startWritingON;
    public Button goToPreparatoryPhaseBut;
    public Image goToPreparatoryPhaseImage;
    public GameObject imageWords; // Button openning or closing the panel

    private Animator anim;

    [HideInInspector]
    public Image[] imagesDiodes;

    private Image[] imagesCL; // All buttons showing collected CLs
    private TextMeshProUGUI[] listOfTextCL; // Text of the buttons showing CLs

    public static SC_Collect instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    void Start()
    {
        // Get the lists
        imagesDiodes = diode.GetComponentsInChildren<Image>(true);
        imagesCL = imageWords.GetComponentsInChildren<Image>(true);
        anim = this.GetComponent<Animator>();


        listOfTextCL = new TextMeshProUGUI[imagesCL.Length];
        for (int i = 0; i < imagesCL.Length; i++)
            listOfTextCL[i] = imagesCL[i].GetComponentInChildren<TextMeshProUGUI>(true);

    }

    public void Recolt (int nbWordCollected)
    {
        if (nbWordCollected == 1)
            PlayRecolt(0);
        else if (nbWordCollected == 2)
            PlayRecolt(1);
        else if (nbWordCollected == 3)
        {
            PlayRecolt(2);
            goToPreparatoryPhaseBut.interactable = true;
            goToPreparatoryPhaseImage.sprite = startWritingON;
        }
    }

    private void PlayRecolt(int nbCL)
    {
        imagesDiodes[nbCL].sprite = diodeON;
        listOfTextCL[nbCL].text = SC_GM_Local.gm.wordsInCollect[nbCL].word[0].titre;
        imagesCL[nbCL].gameObject.SetActive(true);
        anim.Play("Collect2_open");

    }
}
