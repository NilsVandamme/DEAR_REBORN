using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manage the collect panel on the computer

public class SC_Collect : MonoBehaviour
{
    public TextMeshProUGUI ratioText; // Text of showing how many CL have been collected on the buttonCL (X/Y)
    public GameObject buttonCL; // Button openning or closing the panel
    public Animator arboAnim; // Animator
 

    private Button[] buttons; // All buttons showing collected CLs
    private TextMeshProUGUI[] listOfButtons; // Text of the buttons showing CLs

    public bool isHighlighted; // Is currently highlighted ?

    void Start()
    {
        // Print the first value of ratioText
        ratioText.text = 0 + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        // Get the lists
        buttons = buttonCL.GetComponentsInChildren<Button>(true);
        listOfButtons = new TextMeshProUGUI[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
            listOfButtons[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>(true);

        // Activate the buttons according to the number of CLs to collect
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
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
            transform.GetChild(2).GetComponent<Button>().interactable = true;

            // Activate the used buttons only
            for(int i =0; i< SC_GM_Local.gm.numberOfCLRecoverable; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
        }

        if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable && SC_GM_Local.gm.numberOfCLRecover > 0 && isHighlighted == false)
        {
            Debug.Log("yare yare daze");
            arboAnim.SetTrigger("Highlight");
        }

        if(SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable && isHighlighted == false)
        {
            Debug.Log("reeee");
            arboAnim.ResetTrigger("Highlight");
            arboAnim.SetTrigger("Open");
            isHighlighted = true;
        }
    }
}
