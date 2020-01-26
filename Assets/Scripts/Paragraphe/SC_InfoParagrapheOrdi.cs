using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SC_InfoParagrapheOrdi : MonoBehaviour, IPointerClickHandler
{
    public SC_ParagrapheOrdi paragrapheOrdi;
    public TextMeshProUGUI myText;
    public Color CLRecoltColor;
    public Color timbresRecoltColor;
    public Color textNonRecoltableColor;
    public Camera cam;
    public float wait;

    private Color normalTextColor;
    private SC_ChangeColorText listeTexteChangeColor;

    // Lenght des composants des balises
    private int lenghtBaliseColor = 10;
    private int lenghtBaliseColorVerification = 8;

    private bool oneClick = false;
    private int elemCliquable;
    private bool hover;

    public GameObject fxGood;
    public GameObject fxbad;

    private void Start()
    {
        elemCliquable = 0;
        hover = false;

        foreach (TextPart elem in paragrapheOrdi.texte)
            if (elem.partText.Substring(7, 1).Equals("D"))
                myText.text += SC_GM_Master.gm.namePlayer;
            else
            {
                myText.text += elem.partText;
                elemCliquable++;
            }

        normalTextColor = myText.color;

    }

    private void Update()
    {
        if (listeTexteChangeColor != null)
            if (listeTexteChangeColor.lerp < 1)
            {
                Color temp = Color.Lerp(normalTextColor, listeTexteChangeColor.color, listeTexteChangeColor.lerp);
                listeTexteChangeColor.lerp += Time.deltaTime / wait;
                string textColor = ColorUtility.ToHtmlStringRGBA(temp);
                myText.text = listeTexteChangeColor.start + textColor + listeTexteChangeColor.end;
            }
            else
            {
                listeTexteChangeColor = null;
                elemCliquable--;

                if (elemCliquable > 0) 
                    oneClick = false;

                if (hover && elemCliquable > 0)
                    SC_GM_Cursor.gm.changeToHoverCursor();
                else
                    SC_GM_Cursor.gm.changeToNormalCursor();
            }
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

            Collect(linkIndex);
        }
    }


    /*
     * Regarde si le CL est ajoutable ou non
     */
    private void Collect(int linkIndex)
    {
        if (linkIndex != -1) //Collectable
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];

            SC_GM_Cursor.gm.changeToLoadCursor();

            if (linkInfo.GetLinkID()[0] == 'A') // Texte
            {
                ChangeTextColor(linkInfo, textNonRecoltableColor);
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
                        ChangeTextColor(linkInfo, CLRecoltColor);
                    }

                    Instantiate(fxGood, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -1.35f), Quaternion.identity);
                    SC_GM_SoundManager.instance.PlaySound("ClickWin", false);
                    SC_CollectedCLFeedback.instance.text.text = paragrapheOrdi.listChampLexicaux.listNameChampLexical[paragrapheOrdi.champLexical[id]];
                    SC_CollectedCLFeedback.instance.StartFeedback(SC_CollectedCLFeedback.instance.GetMouseWorldPos());

                }
            }
            else if (linkInfo.GetLinkID()[0] == 'C') // Timbres
            {
                for (int i = 0; i < SC_GM_Master.gm.timbres.timbres.Count; i++)
                    if (SC_GM_Master.gm.timbres.timbres[i].getName() == linkInfo.GetLinkID().Substring(1, linkInfo.GetLinkID().Length - 1))
                    {
                        SC_GM_Master.gm.timbres.timbres[i].setVisible(true);
                        ChangeTextColor(linkInfo, timbresRecoltColor);

                        SC_CollectedTimbresFeedback.instance.image.sprite = SC_GM_Master.gm.timbres.images[i];
                    }

                SC_GM_SoundManager.instance.PlaySound("ClickWin", false);
                Instantiate(fxGood, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -1.35f), Quaternion.identity);
                //  // CHANGER IMAGE TIMBRE
                SC_CollectedTimbresFeedback.instance.StartFeedback(SC_CollectedTimbresFeedback.instance.GetMouseWorldPos());
            }
            else // Non collectable
            {
                Instantiate(fxbad, new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -1.35f), Quaternion.identity);
                SC_GM_SoundManager.instance.PlaySound("ClickFail", false);
                //TO-DO --> feedback de refus de collect
            }
        }

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
     * Change le couleur du texte
     */
    private void ChangeTextColor(TMP_LinkInfo linkInfo, Color color)
    {
        Color actualColor;
        string textColor = ColorUtility.ToHtmlStringRGBA(color);

        int lastIndexPart1 = linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + lenghtBaliseColor;
        int lenghtPart2 = myText.text.Length - (lastIndexPart1 + textColor.Length);

        string start = myText.text.Substring(0, lastIndexPart1);
        string actualColorString = myText.text.Substring(lastIndexPart1, textColor.Length);
        string end = myText.text.Substring(lastIndexPart1 + textColor.Length, lenghtPart2);

        if (ColorUtility.TryParseHtmlString("#" + actualColorString, out actualColor))
            if (!actualColor.Equals(normalTextColor))
                return;

        if (start.Substring(start.Length - lenghtBaliseColorVerification, lenghtBaliseColorVerification).Equals("<color=#") && end[0].Equals('>'))
            listeTexteChangeColor = new SC_ChangeColorText(start, end, 0, color);

    }

    public void TextHover()
    {
        if (elemCliquable > 0)
        {
            SC_GM_Cursor.gm.changeToHoverCursor();
            hover = true;
        }

    }

    public void TextHoverExit()
    {
        hover = false;
        SC_GM_Cursor.gm.changeToNormalCursor();
    }


    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePoint) * -16;
    }
}
