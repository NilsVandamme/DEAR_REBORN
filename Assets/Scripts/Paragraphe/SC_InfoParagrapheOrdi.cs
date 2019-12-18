using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi paragraphOrdi;
    public TextMeshProUGUI myText;
    public Color takenColor;
    public Color highlightColor;
    public Camera cam;

    private int lenghtMark = 9;
    private string saveHighlight = "A7DEFF00";
    private string highlight;
    private int saveLinkIndex = -1;
    private int lastIndexPart1;
    private int lenghtPart2;

    private void Start()
    {
        highlight = ColorUtility.ToHtmlStringRGBA(highlightColor);
        
        foreach (TextPart elem in paragraphOrdi.texte)
            myText.text += elem.partText + " ";
    }

    private void Update()
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];

            if (saveLinkIndex != linkIndex)
            {
                lastIndexPart1 = linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + lenghtMark;
                lenghtPart2 = myText.text.Length - (lastIndexPart1 + highlight.Length);
                myText.text = myText.text.Substring(0, lastIndexPart1) + highlight + myText.text.Substring(lastIndexPart1 + highlight.Length, lenghtPart2);
            }

            saveLinkIndex = linkIndex;
        }
        else if (saveLinkIndex != -1)
        {
            myText.text = myText.text.Substring(0, lastIndexPart1) + saveHighlight + myText.text.Substring(lastIndexPart1 + highlight.Length, lenghtPart2);
            saveLinkIndex = -1;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];
            int id = int.Parse(linkInfo.GetLinkID());

            if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
            {
                SC_GM_Local.gm.numberOfCLRecover++;

                bool[] tabBool = new bool[SC_GM_Master.gm.listChampsLexicaux.listOfPerso.Length];
                for (int i = 0; i < paragraphOrdi.motAccepterInCL[id].Length; i++)
                    if (paragraphOrdi.motAccepterInCL[id][i])
                        AddWordInPull(i, tabBool);

            }
        }
    }

    private void AddWordInPull(int word, bool[] tabBool)
    {
        string cl = paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical[word]].fileCSVChampLexical.name;

        foreach (SC_CLInPull elem in SC_GM_Master.gm.wordsInPull)
            if (elem.GetCL() == cl)
            {
                SC_Word mot = paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical[word]].listOfWords[word];
                elem.GetListWord().Add(mot);
                elem.GetUsed().Add(mot.titre, tabBool);
                return;
            }

        SC_GM_Master.gm.wordsInPull.Add(new SC_CLInPull(cl, paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical[word]].listOfWords[word], tabBool));
    }

    private void GetWordOfCLInPull ()
    {
        //pullTextRatio.text = SC_GM_Local.gm.numberOfCLRecover.ToString() + "/" + SC_GM_Local.gm.numberOfCLRecoverable.ToString();

        if (SC_GM_Local.gm.numberOfCLRecover == SC_GM_Local.gm.numberOfCLRecoverable)
        {
            //arboAnim.SetTrigger("ArboIsFull");
            //SC_BossHelp.instance.CloseBossHelp(2);
            //SC_BossHelp.instance.OpenBossBubble(2);
        }
    }
}
