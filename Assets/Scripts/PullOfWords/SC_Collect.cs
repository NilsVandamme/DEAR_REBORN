using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage the collect panel on the computer

public class SC_Collect : MonoBehaviour
{
    public TextMeshProUGUI ratioText; // Text of showing how many CL have been collected on the buttonCL (X/Y)
    public GameObject buttonCL; // Button openning or closing the panel
    public Animator arboAnim; // Animator
 

    private Image[] images; // All buttons showing collected CLs
    private TextMeshProUGUI[] listOfButtons; // Text of the buttons showing CLs

    public bool isHighlighted; // Is currently highlighted ?

    void Start()
    {
        // Print the first value of ratioText
        ratioText.text = 0 + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        // Get the lists
        images = buttonCL.GetComponentsInChildren<Image>(true);
        listOfButtons = new TextMeshProUGUI[images.Length];
        for (int i = 0; i < images.Length; i++)
            listOfButtons[i] = images[i].GetComponentInChildren<TextMeshProUGUI>(true);

        // Activate the buttons according to the number of CLs to collect
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Update the ratioText according to the recovered CLs
        ratioText.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();



        // Update the listOfButtons texts to the collected CLs      
        for (int i = 0; i < SC_GM_Local.gm.wordsInCollect.Count; i++)
            listOfButtons[i].text = SC_GM_Local.gm.wordsInCollect[i].GetCL();

        // Open the panel if all CLs have been collected
        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {

            // Activate the used buttons only
            for(int i =0; i< SC_GM_Local.gm.numberOfCLRecoverable; i++)
            {
                images[i].gameObject.SetActive(true);
            }
        }

        if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable && SC_GM_Local.gm.numberOfCLRecover > 0 && isHighlighted == false)
        {
            //arboAnim.SetTrigger("Highlight");
        }
        if(SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable && isHighlighted == false)
        {
            //arboAnim.ResetTrigger("Highlight");
            GetComponentInChildren<SC_AnimCollect>().SetCollectAnimBool();
            isHighlighted = true;
        }
    }
}
