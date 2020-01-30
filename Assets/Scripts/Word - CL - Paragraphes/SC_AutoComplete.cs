﻿using TMPro;
using UnityEngine;

public class SC_AutoComplete : MonoBehaviour
{
    // Elements récupérer dans le canvas
    public TextMeshProUGUI myText;
    private string myTextSave;

    private SC_ParagraphType typeParagraphe;
    private float coef;
    private int startIndexRewrite;
    private int endIndexRewrite;

    private SC_Word actualWord;

    [HideInInspector]
    public int grammarCritere;

    void Start()
    {
        typeParagraphe = this.gameObject.GetComponentInParent<SC_ParagraphType>();
        coef = typeParagraphe.multiplicativeScore;
        myTextSave = myText.text;
        actualWord = null;

        GetZoneNewText();
    }

    private void GetZoneNewText()
    {
        startIndexRewrite = myText.text.IndexOf("    ");

        for (int i = startIndexRewrite; i < myText.text.Length; i++)
            if (!myText.text[i].Equals(' '))
                endIndexRewrite = i;
    }


    /*
     * Si on drag sur le button du paragraphe de la lettre
     */
    public void OnClick()
    {
        if (HasWordInListeMaster())
        {
            DeleteWord();
            ChangeWordInText();
        }
    }

    /*
     * Si on click pour remove le paragraphe de la lettre
     */
    public void OnRemove()
    {
        myText.text = myTextSave;

        DeleteWord();
    }



    /*
     * Si le mot actuel est dans la liste des mots deja choisi de la lettre, on le supprime
     */
    private void DeleteWord()
    {
        for (int i = 0; i < SC_GM_Master.gm.choosenWordInLetter.Count; i++)
            if (actualWord != null && actualWord.titre.Equals(SC_GM_Master.gm.choosenWordInLetter[i].Item1.titre))
            {
                SC_GM_Master.gm.choosenWordInLetter.Remove(SC_GM_Master.gm.choosenWordInLetter[i]);
                break;
            }
    }


    /*
     * Actualise le text,
     * recalcule la position de fin du mot ajoute,
     * ajoute le mot a la liste du GM_Master,
     * actualise le mot courrant
     */
    private void ChangeWordInText()
    {
        myText.text = myText.text.Substring(0, startIndexRewrite) + " " +
                        SC_GM_WheelToLetter.instance.getCurrentWord().grammarCritere[grammarCritere] + " " +
                        myText.text.Substring(endIndexRewrite, (myText.text.Length - endIndexRewrite));

        endIndexRewrite = startIndexRewrite + SC_GM_WheelToLetter.instance.getCurrentWord().grammarCritere[grammarCritere].Length + 2;

        SC_GM_Master.gm.choosenWordInLetter.Add((SC_GM_WheelToLetter.instance.getCurrentWord(), coef));

        actualWord = SC_GM_WheelToLetter.instance.getCurrentWord();
    }


    /*
     * Regarde si le mot que l'on veut ajouter est deja utiliser
     */
    private bool HasWordInListeMaster()
    {
        for (int i = 0; i < SC_GM_Master.gm.choosenWordInLetter.Count; i++)
            if (SC_GM_Master.gm.choosenWordInLetter[i].Item1.titre.Equals(SC_GM_WheelToLetter.instance.getCurrentWord().titre))
                return false;

        return true;
    }
}
