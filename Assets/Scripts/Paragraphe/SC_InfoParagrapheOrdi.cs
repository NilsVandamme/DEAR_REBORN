using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SC_InfoParagrapheOrdi : MonoBehaviour, IPointerClickHandler
{
    public SC_ParagrapheOrdi paragraphOrdi;
    public TextMeshProUGUI myText;
    public Color takenColor;
    public Color highlightColor;
    public Camera cam;
    public float wait;

    private int lenghtMark = 9;
    private string highlight;
    private bool oneClick = false;

    private void Start()
    {
        highlight = ColorUtility.ToHtmlStringRGBA(highlightColor);
        
        foreach (TextPart elem in paragraphOrdi.texte)
            myText.text += elem.partText + " ";
    }
    
    /*
     * Lors du click wait
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!oneClick)
        {
            oneClick = true;

            int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

            StartCoroutine(Collect(linkIndex));
        }
    }

    /*
     * Regarde si le CL est ajoutable ou non
     */
    private IEnumerator Collect(int linkIndex)
    {
        SC_GM_Cursor.gm.changeToLoadCursor();

        yield return new WaitForSeconds(wait);

        if (linkIndex != -1) //Collectable
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];
            int id = int.Parse(linkInfo.GetLinkID());

            if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
            {
                SC_GM_Local.gm.numberOfCLRecover++;
                Highlight(linkInfo);

                for (int i = 0; i < paragraphOrdi.motAccepterInCL[id].Length; i++)
                    if (paragraphOrdi.motAccepterInCL[id][i])
                        AddWordInCollect(id, i);

            }
        }
        else // Non collectable
        {
            //TO-DO --> feedback de refus de collect
        }

        SC_GM_Cursor.gm.changeToNormalCursor();
        oneClick = false;
    }

    /*
     * Ajoute le CL a la collect
     */
    private void AddWordInCollect(int link, int word)
    {
        string cl = paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical[link]].fileCSVChampLexical.name;

        foreach (SC_CLInPull elem in SC_GM_Master.gm.wordsInCollect)
            if (elem.GetCL() == cl) // Si le CL est deja present dans la collect
            {
                SC_Word mot = paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical[link]].listOfWords[word];

                foreach (SC_Word val in elem.GetListWord())
                    if (val.titre == mot.titre)
                        return;

                elem.GetListWord().Add(mot);
                return;
            }

        SC_GM_Master.gm.wordsInCollect.Add(new SC_CLInPull(cl, paragraphOrdi.listChampLexicaux.listChampLexical[paragraphOrdi.champLexical[link]].listOfWords[word]));
    }

    /*
     * Highlight la partie CL récupéré
     */
    private void Highlight(TMP_LinkInfo linkInfo)
    {
        int lastIndexPart1 = linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + lenghtMark;
        int lenghtPart2 = myText.text.Length - (lastIndexPart1 + highlight.Length);
        myText.text = myText.text.Substring(0, lastIndexPart1) + highlight + myText.text.Substring(lastIndexPart1 + highlight.Length, lenghtPart2);
    }
}
