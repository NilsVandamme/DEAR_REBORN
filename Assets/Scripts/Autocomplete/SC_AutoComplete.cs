using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SC_AutoComplete : MonoBehaviour, IPointerClickHandler
{
    // Camera de la scène
    public Camera cam;

    // Elements récupérer dans le canvas
    public TextMeshProUGUI myText;

    // Object regroupant les informations obtenue lors des clicks
    private SC_ClickObject currentClick;

    void Start()
    {
        // Init le tab des inputs sauvegardées
        SC_GM_Local.gm.choosenWordInLetter = new List<SC_Word>();
    }

    /*
     * Récupère si l'on click sur un lien et permet l'écriture à cette position
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

        if (linkIndex != -1 && SC_GM_WheelToLetter.instance.getCurrentWord() != null)
        {
            GetClickInfo(myText.textInfo.linkInfo[linkIndex]);

            RewriteTextWithInputField();
        }
    }

    /*
     * Récupère les infos de la balise clicker
     */
    private void GetClickInfo(TMP_LinkInfo linkInfo)
    {
        int lenght = -1;
        SC_Word oldWorld = null;
        string newWord = "", grammar = myText.text.Substring(linkInfo.linkIdFirstCharacterIndex, linkInfo.linkIdLength);

        foreach (SC_Word elem in SC_GM_Local.gm.wheelOfWords)
            foreach (string mot in elem.grammarCritere)
                if (mot.Equals(linkInfo.GetLinkText()))
                    oldWorld = elem;

        for (int i = 0; i < SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere.Length; i++)
            if (SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere[i].Equals(grammar))
            {
                newWord = SC_GM_WheelToLetter.instance.getCurrentWord().grammarCritere[i];
                if (oldWorld != null)
                    lenght = oldWorld.grammarCritere[i].Length;
                else
                    lenght = linkInfo.linkTextLength;
            }

        currentClick = new SC_ClickObject(linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + 2, oldWorld, newWord, lenght);
    }

    /*
     * Supprime le text courant et le remplace par le nouveau
     */
    private void RewriteTextWithInputField()
    {
        myText.text = myText.text.Remove(currentClick.getPosStartText(), currentClick.getLenOldWord());
        myText.text = myText.text.Insert(currentClick.getPosStartText(), currentClick.getNewMot());

        if (!SC_GM_Local.gm.choosenWordInLetter.Contains(currentClick.getOldWord()))
            SC_GM_Local.gm.choosenWordInLetter.Remove(currentClick.getOldWord());

        if (!SC_GM_Local.gm.choosenWordInLetter.Contains(SC_GM_WheelToLetter.instance.getCurrentWord()))
            SC_GM_Local.gm.choosenWordInLetter.Add(SC_GM_WheelToLetter.instance.getCurrentWord());

    }
}
