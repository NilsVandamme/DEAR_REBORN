using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage the collect panel on the computer

public class SC_Collect : MonoBehaviour
{
    public GameObject diode;
    public Sprite diodeON;
    public Sprite diodeOFF;
    public Button GoToPreparatory;

    public GameObject imageCL; // Button openning or closing the panel

    private Image[] imagesDiodes;
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
        imagesCL = imageCL.GetComponentsInChildren<Image>(true);


        listOfTextCL = new TextMeshProUGUI[imagesCL.Length];
        for (int i = 0; i < imagesCL.Length; i++)
            listOfTextCL[i] = imagesCL[i].GetComponentInChildren<TextMeshProUGUI>(true);


        // Activate the buttons according to the number of CLs to collect
        for (int i = 0; i < imagesCL.Length; i++)
            imagesCL[i].gameObject.SetActive(false);

        for (int i = 0; i < imagesDiodes.Length; i++)
            imagesDiodes[i].sprite = diodeOFF;
    }

    public void Recolt ()
    {
        if (SC_GM_Local.gm.numberOfCLRecover == 1)
            PlayRecolt(0);
        else if (SC_GM_Local.gm.numberOfCLRecover == 2)
            PlayRecolt(1);
        else if (SC_GM_Local.gm.numberOfCLRecover == 3)
        {
            PlayRecolt(2);
            GoToPreparatory.interactable = true;
        }
    }

    private void PlayRecolt(int nbCL)
    {
        imagesDiodes[nbCL].sprite = diodeON;
        listOfTextCL[nbCL].text = SC_GM_Local.gm.wordsInCollect[nbCL].word[0].titre;
        imagesCL[nbCL].gameObject.SetActive(true);

    }
}
