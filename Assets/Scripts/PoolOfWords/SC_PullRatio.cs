using TMPro;
using UnityEngine;

public class SC_PullRatio : MonoBehaviour
{
    public TextMeshProUGUI pullTextRatio;
    public Animator arboAnim;

    private void Start()
    {
        pullTextRatio.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();
    }

    void Update()
    {
        pullTextRatio.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {
            //arboAnim.SetTrigger("ArboIsFull");
            //SC_BossHelp.instance.CloseBossHelp(2);
            //SC_BossHelp.instance.OpenBossBubble(2);
        }
    }
}
