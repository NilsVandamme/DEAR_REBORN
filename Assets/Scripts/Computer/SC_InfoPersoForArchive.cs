using TMPro;
using UnityEngine;

public class SC_InfoPersoForArchive : MonoBehaviour
{
    public TextMeshProUGUI info;
    public TextMeshProUGUI desc;

    private void Start()
    {
        SC_GM_Master.gm.descriptionPerso[SC_GM_Local.gm.persoOfCurrentScene] = desc.text;
        SC_GM_Master.gm.infoPerso[SC_GM_Local.gm.persoOfCurrentScene] = info.text.Replace(System.Environment.NewLine, " / ");
    }
}
