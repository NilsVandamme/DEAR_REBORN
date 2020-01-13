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
    public TMP_InputField myInputField;
    public RectTransform rect;

    // Object regroupant les informations obtenue lors des clicks
    private SC_ClickObject currentClick;
    private Vector3 newPos;

    /*
     * Récupère les objets nécessaires
     */
    void Start()
    {
        // Init le tab des inputs sauvegardées
        SC_GM_Local.gm.choosenWordInLetter = new List<string>();
    }

    /*
     * Récupère si l'on click sur un lien et permet l'écriture à cette position
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, cam, out newPos);
        myInputField.transform.position = new Vector3(newPos.x, myInputField.transform.position.y, newPos.z);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];

            if (currentClick == null)
                myInputField.gameObject.SetActive(true);
            else
                RewriteTextWithInputField();

            GetClickInfo(linkInfo);
        }
        else if (currentClick != null)
            RewriteAndReinit();
    }

    /*
     * Supprime le text courant et le remplace par le nouveau
     */
    private void RewriteTextWithInputField(string newString = null)
    {
        if (myInputField.text != "" || newString != null)
        {
            myText.text = myText.text.Remove((currentClick.getPosStartText()), currentClick.getMot().Length);
            if (!SC_GM_Local.gm.choosenWordInLetter.Contains(currentClick.getMot()))
                SC_GM_Local.gm.choosenWordInLetter.Remove(currentClick.getMot());

            if (newString == null)
            {
                myText.text = myText.text.Insert(currentClick.getPosStartText(), myInputField.text);
                if (!SC_GM_Local.gm.choosenWordInLetter.Contains(myInputField.text))
                    SC_GM_Local.gm.choosenWordInLetter.Add(myInputField.text);
            }
            else
            {
                myText.text = myText.text.Insert(currentClick.getPosStartText(), newString);
                if (!SC_GM_Local.gm.choosenWordInLetter.Contains(newString))
                    SC_GM_Local.gm.choosenWordInLetter.Add(newString);
            }
        }
    }

    /*
     * Récupère les infos de la balise clicker
     */
    private void GetClickInfo(TMP_LinkInfo linkInfo)
    {
        if (linkInfo.GetLinkText() == "_____")
            myInputField.text = "";
        else
            myInputField.text = linkInfo.GetLinkText();

        currentClick = new SC_ClickObject(myText.text.IndexOf(linkInfo.GetLinkText()),
                                            linkInfo.GetLinkText(),
                                            myText.text.Substring(linkInfo.linkIdFirstCharacterIndex, linkInfo.linkIdLength));
    }

    /*
     * Gère le click d'un Button
     */
    public void OnClickButtonAutoComplete(TextMeshProUGUI text)
    {
        if (currentClick != null)
        {
            foreach (SC_Word word in SC_GM_Local.gm.wheelOfWords)
                if (word.titre == text.text)
                    for (int i = 0; i < SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere.Length; i++)
                    {
                        Debug.Log(SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere[i]);
                        Debug.Log(currentClick.getId());
                        if (SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere[i] == currentClick.getId())
                        {
                            RewriteAndReinit(word.grammarCritere[i]);
                            return;
                        }
                    }
        }
    }

    /*
     * Ecrit le text selectionné et réinit/close les params et les elems
     */
    private void RewriteAndReinit(string text = null)
    {
        RewriteTextWithInputField(text);
        currentClick = null;
        myInputField.gameObject.SetActive(false);
    }
}
