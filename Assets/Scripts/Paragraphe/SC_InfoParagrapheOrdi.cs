using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi paragraphOrdi;
    public TextMeshProUGUI collect;
    public TextMeshProUGUI pullTextRatio;
    public Image imageFondTextOnClick;
    public Animator arboAnim;
    public Color takenColor;

    private bool validate = false;

    private void Start()
    {
        pullTextRatio.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();
    }

    public void OnClickParagrapheOrdi()
    {
        if (!validate)
        {
            imageFondTextOnClick.gameObject.SetActive(!imageFondTextOnClick.IsActive());
            collect.gameObject.SetActive(!collect.IsActive());

        }
    }

    public void OnClickButtonConfirm()
    {
        if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
        {
            GetWordOfCLInPull();

            bool[] tabBool = new bool[SC_GM_Master.gm.listChampsLexicaux.listOfPerso.Length];
            for (int i = 0; i < paragraphOrdi.motAccepterInCL.Length; i++)
            {
                if (paragraphOrdi.motAccepterInCL[i])
                {
                    SC_WordInPull elem = 
                        new SC_WordInPull(paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical].fileCSVChampLexical.name,
                        paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical].listOfWords[i], tabBool);

                    foreach (SC_WordInPull wordPull in SC_GM_Master.gm.wordsInPull)
                        if (wordPull.GetWord().titre == elem.GetWord().titre)
                            return;

                    SC_GM_Master.gm.wordsInPull.Add(elem);
                }
            }

        }
    }

    private void GetWordOfCLInPull ()
    {
        SC_GM_Local.gm.numberOfCLRecover++;
        pullTextRatio.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();
        validate = true;
        collect.gameObject.SetActive(false);
        imageFondTextOnClick.color = takenColor;

        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {
            arboAnim.SetTrigger("ArboIsFull");
            SC_BossHelp.instance.CloseBossHelp(2);
            SC_BossHelp.instance.OpenBossBubble(2);
        }
    }
}
