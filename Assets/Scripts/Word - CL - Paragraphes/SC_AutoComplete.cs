using TMPro;
using UnityEngine;

public class SC_AutoComplete : MonoBehaviour
{
    // Elements récupérer dans le canvas
    public TextMeshPro myTextContenue;
    public TextMeshPro myTextPresentation;
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
        myTextSave = myTextContenue.text;
        actualWord = null;

        myTextContenue.gameObject.SetActive(false);

        GetZoneNewText();
    }

    private void GetZoneNewText()
    {
        startIndexRewrite = myTextContenue.text.IndexOf("       ");

        if (startIndexRewrite < 0)
        {
            Debug.LogError("Il faut une zone de texte '      vide        ' pour la place du mot a ajouter");
            return;
        }

        for (int i = startIndexRewrite; i < myTextContenue.text.Length; i++)
            if (!myTextContenue.text[i].Equals(' '))
            {
                endIndexRewrite = i;
                return;
            }
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
        myTextContenue.text = myTextSave;

        DeleteWord();
    }



    /*
     * Si le mot actuel est dans la liste des mots deja choisi de la lettre, on le supprime
     */
    private void DeleteWord()
    {
        for (int i = 0; i < SC_GM_Master.gm.choosenWordInLetter.Count; i++)
            if (actualWord != null && actualWord.titre.Equals(SC_GM_Master.gm.choosenWordInLetter[i].word.titre))
            {
                SC_GM_Master.gm.choosenWordInLetter.Remove(SC_GM_Master.gm.choosenWordInLetter[i]);
                return;
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
        Debug.Log(myTextContenue.text.Substring(0, startIndexRewrite));
        Debug.Log(SC_GM_WheelToLetter.instance.getCurrentWord());
        Debug.Log(myTextContenue.text.Substring(endIndexRewrite, (myTextContenue.text.Length - endIndexRewrite)));

        myTextContenue.text = myTextContenue.text.Substring(0, startIndexRewrite) + " " +
                        SC_GM_WheelToLetter.instance.getCurrentWord().grammarCritere[grammarCritere] + " " +
                        myTextContenue.text.Substring(endIndexRewrite, (myTextContenue.text.Length - endIndexRewrite));

        endIndexRewrite = startIndexRewrite + SC_GM_WheelToLetter.instance.getCurrentWord().grammarCritere[grammarCritere].Length + 2;

        actualWord = SC_GM_WheelToLetter.instance.getCurrentWord();

        SC_GM_Master.gm.choosenWordInLetter.Add(new SC_InfoParagrapheLettreRemplie(SC_GM_WheelToLetter.instance.getCurrentWord(), 
                                                SC_GM_WheelToLetter.instance.getCurrentWord().scorePerso[SC_GM_Local.gm.persoOfCurrentScene] * coef, 
                                                myTextContenue.text));

    }


    /*
     * Regarde si le mot que l'on veut ajouter est deja utiliser
     */
    private bool HasWordInListeMaster()
    {
        for (int i = 0; i < SC_GM_Master.gm.choosenWordInLetter.Count; i++)
            if (SC_GM_Master.gm.choosenWordInLetter[i].word.titre.Equals(SC_GM_WheelToLetter.instance.getCurrentWord().titre))
                return false;

        return true;
    }
}
