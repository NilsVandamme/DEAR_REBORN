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

    void Start()
    {
        ratioText.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        buttons = buttonCL.GetComponentsInChildren<Button>();
        listOfButtons = new TextMeshProUGUI[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
            listOfButtons[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

    }

    void Update()
    {
        for (int i = 0; i < SC_GM_Master.gm.wordsInCollect.Count; i++)
            listOfButtons[i].text = SC_GM_Master.gm.wordsInCollect[i].GetCL();

        foreach (SC_CLInPull elem in SC_GM_Master.gm.wordsInCollect)
            SC_GM_Master.gm.wordsInPull.Add(elem);

        ratioText.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {
            //arboAnim.SetTrigger("ArboIsFull");
            //SC_BossHelp.instance.CloseBossHelp(2);
            //SC_BossHelp.instance.OpenBossBubble(2);
        }
    }
}
