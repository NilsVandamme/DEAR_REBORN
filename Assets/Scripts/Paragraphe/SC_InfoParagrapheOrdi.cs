using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SC_InfoParagrapheOrdi : MonoBehaviour, IPointerClickHandler
{
    public SC_ParagrapheOrdi paragrapheOrdi;
    public TextMeshProUGUI myText;
    public Color highlightColor;
    public Color highlightColorTimbres;
    public Color textLoadingColor;
    public Camera cam;
    public float wait;

    private int lengthLink = 10;
    private int lengthLinkEnd = 7;
    private int lenghtMark = 9;
    private int lenghtColor = 9;
    private string highlight;
    private string highlightTimbres;
    private string textNormal;
    private string textLoading;
    private bool oneClick = false;

    private void Start()
    {
        highlight = ColorUtility.ToHtmlStringRGBA(highlightColor);
        textNormal = ColorUtility.ToHtmlStringRGBA(myText.color);
        textLoading = ColorUtility.ToHtmlStringRGBA(textLoadingColor);
        highlightTimbres = ColorUtility.ToHtmlStringRGBA(highlightColorTimbres);
        
        foreach (TextPart elem in paragrapheOrdi.texte)
            if (elem.partText.Substring(7, 1).Equals("D"))
                myText.text += SC_GM_Master.gm.namePlayer;
            else
                myText.text += elem.partText;

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
        if (linkIndex != -1) //Collectable
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];

            SC_GM_Cursor.gm.changeToLoadCursor();

            ChangeTextColor(linkInfo, textLoading);
            yield return new WaitForSeconds(wait);
            ChangeTextColor(linkInfo, textNormal);

            if (linkInfo.GetLinkID()[0] == 'A') // Texte
            {
                Barre(linkInfo);
            }
            else if (linkInfo.GetLinkID()[0] == 'B') // CL
            {
                int id = int.Parse(linkInfo.GetLinkID().Substring(1));

                if (SC_GM_Local.gm.numberOfCLRecover < SC_GM_Local.gm.numberOfCLRecoverable)
                {
                    bool add = false;

                    int pos = 0;
                    for (int i = 0; i < id; i++)
                        pos += paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[i]].listOfWords.Count;

                    for (int i = 0; i < paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[id]].listOfWords.Count; i++, pos++)
                        if (paragrapheOrdi.motAccepterInCL[pos])
                            if (AddWordInCollect(id, i))
                                add = true;

                    if (add)
                    {
                        SC_GM_Local.gm.numberOfCLRecover++;
                        Highlight(linkInfo, highlight);
                    }

                }
            }
            else if (linkInfo.GetLinkID()[0] == 'C') // Timbres
            {
                foreach (SC_Timbres timbres in SC_GM_Master.gm.timbres.timbres)
                    if (timbres.getName() == linkInfo.GetLinkID().Substring(1, linkInfo.GetLinkID().Length - 1))
                    {
                        timbres.setVisible(true);
                        SC_GM_Timbre.gm.Affiche(timbres);
                        Highlight(linkInfo, highlightTimbres);
                    }
            }
            else // Non collectable
            {
                //TO-DO --> feedback de refus de collect
            }
        }

        SC_GM_Cursor.gm.changeToNormalCursor();
        oneClick = false;
    }

    /*
     * Ajoute le CL a la collect
     */
    private bool AddWordInCollect(int link, int word)
    {
        string cl = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[link]].fileCSVChampLexical.name;

        foreach (SC_CLInPull elem in SC_GM_Local.gm.wordsInCollect)
            if (elem.GetCL() == cl) // Si le CL est deja present dans la collect
            {
                SC_Word mot = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[link]].listOfWords[word];

                foreach (SC_Word val in elem.GetListWord())
                    if (val.titre == mot.titre)
                        return false;

                elem.GetListWord().Add(mot);
                return true;
            }

        SC_GM_Local.gm.wordsInCollect.Add(new SC_CLInPull(cl, paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[link]].listOfWords[word]));
        return true;
    }

    /*
     * Highlight la partie CL récupéré
     */
    private void Highlight(TMP_LinkInfo linkInfo, string color)
    {
        int lastIndexPart1 = linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + lenghtMark;
        int lenghtPart2 = myText.text.Length - (lastIndexPart1 + color.Length);
        myText.text = myText.text.Substring(0, lastIndexPart1) + color + myText.text.Substring(lastIndexPart1 + color.Length, lenghtPart2);
    }

    /*
     * Change le couleur du texte
     */
    private void ChangeTextColor(TMP_LinkInfo linkInfo, string color)
    {
        int lastIndexPart1 = linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + lenghtMark + lenghtColor + highlight.Length;
        int lenghtPart2 = myText.text.Length - (lastIndexPart1 + color.Length);
        myText.text = myText.text.Substring(0, lastIndexPart1) + color + myText.text.Substring(lastIndexPart1 + color.Length, lenghtPart2);
    }

    /*
     * Barre le texte sur lequel on a clicker
     */
    private void Barre(TMP_LinkInfo linkInfo)
    {
        int startMiddle = (linkInfo.linkIdFirstCharacterIndex - lengthLinkEnd) + lengthLink;
        int startMiddleLenght = myText.text.IndexOf("</link>", linkInfo.linkIdFirstCharacterIndex) - startMiddle;
        int startEnd = myText.text.IndexOf("</link>", linkInfo.linkIdFirstCharacterIndex);

        myText.text = myText.text.Substring(0, startMiddle) + "<s>" +
                        myText.text.Substring(startMiddle, startMiddleLenght) + "</s>" +
                        myText.text.Substring(startEnd, myText.text.Length - startEnd);
    }
}
