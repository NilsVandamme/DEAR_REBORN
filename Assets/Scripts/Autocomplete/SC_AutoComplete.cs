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

        if (linkIndex != -1)
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
        SC_Word word = null;
        string grammar = myText.text.Substring(linkInfo.linkIdFirstCharacterIndex, linkInfo.linkIdLength);

        foreach (SC_Word elem in SC_GM_Local.gm.wheelOfWords)
            foreach (string mot in elem.grammarCritere)
                if (mot.Equals(linkInfo.GetLinkText()))
                    word = elem;

        for (int i = 0; i < SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere.Length; i++)
            if (SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere[i] == grammar)
                lenght = i;

        currentClick = new SC_ClickObject(myText.text.IndexOf(linkInfo.GetLinkText()), word, lenght);
    }

    /*
     * Supprime le text courant et le remplace par le nouveau
     */
    private void RewriteTextWithInputField()
    {
        myText.text = myText.text.Remove((currentClick.getPosStartText()), currentClick.getMot().grammarCritere[currentClick.getIndex()].Length);
        myText.text = myText.text.Insert(currentClick.getPosStartText(), SC_GM_WheelToLetter.instance.getCurrentWord().grammarCritere[currentClick.getIndex()]);

        if (!SC_GM_Local.gm.choosenWordInLetter.Contains(currentClick.getMot()))
            SC_GM_Local.gm.choosenWordInLetter.Remove(currentClick.getMot());

        if (!SC_GM_Local.gm.choosenWordInLetter.Contains(SC_GM_WheelToLetter.instance.getCurrentWord()))
            SC_GM_Local.gm.choosenWordInLetter.Add(SC_GM_WheelToLetter.instance.getCurrentWord());

    }
}
