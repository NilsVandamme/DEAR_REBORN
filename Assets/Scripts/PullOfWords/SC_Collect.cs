using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_Collect : MonoBehaviour
{
    public TextMeshProUGUI ratioText;
    public GameObject buttonCL;
    public Animator arboAnim;

    private Button[] buttons;
    private TextMeshProUGUI[] listOfButtons;

    private bool isHighlighted;

    void Start()
    {
        ratioText.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        buttons = buttonCL.GetComponentsInChildren<Button>(true);
        listOfButtons = new TextMeshProUGUI[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
            listOfButtons[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>(true);

    }

    void Update()
    {
        ratioText.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        for (int i = 0; i < SC_GM_Local.gm.wordsInCollect.Count; i++)
            listOfButtons[i].text = SC_GM_Local.gm.wordsInCollect[i].GetCL();

        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {
            transform.GetChild(2).GetComponent<Button>().interactable = true;

            // Activate the used buttons only
            for(int i =0; i< SC_GM_Local.gm.numberOfCLRecoverable; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }

            if (!isHighlighted)
            {
                arboAnim.SetTrigger("Open");
                isHighlighted = true;
            }


        }
    }
}
