﻿using System.Collections;
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

    private void Start()
    {
        foreach (TextPart elem in paragrapheOrdi.texte)
            if (elem.partText.Substring(7, 1).Equals("D"))
                myText.text += SC_GM_Master.gm.namePlayer;
            else
                myText.text += elem.partText;

        normalTextColor = myText.color;

    }

    private void Update()
    {
        if (listeTexteChangeColor != null && listeTexteChangeColor.lerp < 1)
        {
            Color temp = Color.Lerp(normalTextColor, listeTexteChangeColor.color, listeTexteChangeColor.lerp);
            listeTexteChangeColor.lerp += Time.deltaTime / wait;
            string textColor = ColorUtility.ToHtmlStringRGBA(temp);
            myText.text = listeTexteChangeColor.start + textColor + listeTexteChangeColor.end;
        }
        else
        {
            listeTexteChangeColor = null;
            oneClick = false;
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

                }
            }
            else if (linkInfo.GetLinkID()[0] == 'C') // Timbres
            {
                foreach (SC_Timbres timbres in SC_GM_Master.gm.timbres.timbres)
                    if (timbres.getName() == linkInfo.GetLinkID().Substring(1, linkInfo.GetLinkID().Length - 1))
                    {
                        timbres.setVisible(true);
                        SC_GM_Timbre.gm.Affiche(timbres);
                        ChangeTextColor(linkInfo, timbresRecoltColor);
                    }
            }
            else // Non collectable
            {
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










    //########################################################################################################################################################################
    //########################################################################################################################################################################
    //#################################################################################  PLUS UTILE  #########################################################################
    //########################################################################################################################################################################
    //########################################################################################################################################################################

    ///*
    // * Highlight la partie CL récupéré
    // */
    //private void Highlight(TMP_LinkInfo linkInfo, string color)
    //{
    //    int lastIndexPart1 = linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + lenghtMark;
    //    int lenghtPart2 = myText.text.Length - (lastIndexPart1 + color.Length);
    //    myText.text = myText.text.Substring(0, lastIndexPart1) + color + myText.text.Substring(lastIndexPart1 + color.Length, lenghtPart2);
    //}

    ///*
    // * Barre le texte sur lequel on a clicker
    // */
    //private void Barre(TMP_LinkInfo linkInfo)
    //{
    //    int startMiddle = (linkInfo.linkIdFirstCharacterIndex - lengthLinkEnd) + lengthLink;
    //    int startMiddleLenght = myText.text.IndexOf("</link>", linkInfo.linkIdFirstCharacterIndex) - startMiddle;
    //    int startEnd = myText.text.IndexOf("</link>", linkInfo.linkIdFirstCharacterIndex);

    //    myText.text = myText.text.Substring(0, startMiddle) + "<s>" +
    //                    myText.text.Substring(startMiddle, startMiddleLenght) + "</s>" +
    //                    myText.text.Substring(startEnd, myText.text.Length - startEnd);
    //}
}
