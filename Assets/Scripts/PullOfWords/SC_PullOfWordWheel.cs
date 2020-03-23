using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PullOfWordWheel : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_wheelWord;
    public GameObject GO_champsLexicaux;
    public Button startWrittingButton;
    public GameObject GO_wheelToLetterButtons;

    public Color recoltedWord;

    // Info sur le CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private Animator[] animator;
    private TextMeshProUGUI[][] champLexical;
    private Button[][] champLexicalButtons;

    // Liste des mots de la wheel
    private TextMeshProUGUI[] listOfWheel;
    private TextMeshPro[] wheelToLetter;

    //##############################################################################################################################################################
    //########################################################################        INIT           ###############################################################
    //##############################################################################################################################################################

    /*
     * Init le CL, la Wheel et les Button de Word pour la phase d'ecriture de la lettre
     */
    public void InitWheel()
    {
        InitRightSide();
        InitChampsLexicauxWheel();

        wheelToLetter = GO_wheelToLetterButtons.GetComponentsInChildren<TextMeshPro>(true);
    }

    /*
     * Recupere la liste des critere et des perso. Met a jour les perso
     */
    private void InitRightSide()
    {
        listOfWheel = GO_wheelWord.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI elem in listOfWheel)
            elem.text = "";

        startWrittingButton.interactable = false;
    }

    /*
     * Récupère la liste des CL et la liste des Word de chaque CL
     */
    private void InitChampsLexicauxWheel()
    {
        champsLexicaux = GO_champsLexicaux.GetComponentsInChildren<LayoutGroup>(true);
        animator = GO_champsLexicaux.GetComponentsInChildren<Animator>(true);

        champLexical = new TextMeshProUGUI[champsLexicaux.Length][];
        champLexicalButtons = new Button[champsLexicaux.Length][];

        for (int i = 0; i < champsLexicaux.Length; i++)
        {
            champLexical[i] = champsLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI elem in champLexical[i])
                elem.text = "";

            champLexicalButtons[i] = champsLexicaux[i].GetComponentsInChildren<Button>(true);
        }

        WriteWordAndCL();
    }

    /*
     * Ecrit le nom des CL et des Words qu'ils contiennent
     */
    private void WriteWordAndCL()
    {
        int pos;
        for (int i = 0; i < SC_GM_Local.gm.wordsInPreparatory.Count; i++)
        {
            SC_CLInPull cl = SC_GM_Local.gm.wordsInPreparatory[i];
            champLexical[i][posElemCl].text = cl.GetCL();

            for (int j = 0; j < numberOfElemInCL; j++)
            {
                if (j < cl.GetListWord().Count)
                {
                    pos = GetFirstCLWordFree(i);
                    if (pos != -1)
                    {
                        champLexical[i][pos].text = cl.GetListWord()[j].titre;
                    }
                }

                else if (j != posElemCl)
                {
                    champLexicalButtons[i][j].interactable = false;
                }
            }

        }
    }

    /*
    * Trouve le premier champ non utilisé du CL et retoune sa position
    */
    private int GetFirstCLWordFree(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && champLexical[index][i].text == "")
                return i;

        return -1;
    }

    /*
     * Open CL
     */
    public void OpenCloseCL(int index)
    {
        bool openClose = champLexicalButtons[index][0].gameObject.activeSelf;

        if (openClose) return;

        for (int i = 0; i < champLexicalButtons.Length; i++)
            for (int j = 0; j < champLexicalButtons[i].Length; j++)
                if (j != posElemCl)
                {
                    if (i == index)
                    {
                        champLexicalButtons[i][j].gameObject.SetActive(true);
                        animator[i].Play("Open");
                    }

                    else
                    {
                        champLexicalButtons[i][j].gameObject.SetActive(false);
                    }
                }
    }


    //##############################################################################################################################################################
    //########################################################################        FONCTION         #############################################################
    //##############################################################################################################################################################

    /*
     * Ajoute le mot clicker dans la wheel
     */
    public void AddWordInWheel(TextMeshProUGUI tmp)
    {
        SC_Word word = GetWordInCollect(tmp.text);
        if (word != null)
        {
            if (SC_GM_Local.gm.wheelOfWords.Contains(word))
                return;

            int pos = GetFirstWordInWheelFree();
            if (pos != -1)
            {
                listOfWheel[pos].text = word.titre;
                SC_GM_Local.gm.wheelOfWords.Add(word);

                if (SC_GM_Local.gm.wheelOfWords.Count == listOfWheel.Length)
                    startWrittingButton.interactable = true;


                Button but = tmp.GetComponentInParent<Button>();

                ColorBlock colors = but.colors;
                colors.disabledColor = recoltedWord;
                but.colors = colors;

                but.interactable = false;
            }
        }
    }


    /*
     * Trouve le mot des collects correspondant au TMP_UGUI (mot)
     */
    private SC_Word GetWordInCollect(string mot)
    {
        foreach (SC_CLInPull cl in SC_GM_Local.gm.wordsInPreparatory)
            foreach (SC_Word word in cl.GetListWord())
                if (mot == word.titre)
                    return word;

        return null;
    }

    /*
    * Trouve le premier mot de la wheel disponible et retoune sa position.
    * Rend clickable le button start si l'on à remplie la Wheel
    */
    private int GetFirstWordInWheelFree()
    {
        for (int i = 0; i < listOfWheel.Length; i++)
            if (listOfWheel[i].text == "")
                return i;

        return -1;
    }

    /*
     * Remove le mot de la wheel
     * Remet interactable le button word correspondant
     */
    public void OnClickRemoveWordInWheel(TextMeshProUGUI tmp)
    {
        string text = tmp.text;

        for (int i = 0; i < listOfWheel.Length; i++)
            if (listOfWheel[i] == tmp)
            {
                SC_GM_Local.gm.wheelOfWords.Remove(GetWordInCollect(tmp.text));
                listOfWheel[i].text = "";
            }

        startWrittingButton.interactable = false;

        for (int i = 0; i < champLexical.Length; i++)
            for (int j = 0; j < champLexical[i].Length; j++)
                if (champLexical[i][j].text == text)
                {
                    champLexicalButtons[i][j].interactable = true;
                    return;
                }

    }

    //##############################################################################################################################################################
    //##############################################################################################################################################################
    //#######################################################################          AUTRE           #############################################################
    //##############################################################################################################################################################
    //##############################################################################################################################################################

    /*
     * Init les paragraphes de la lettre
     */
    public void StartWritting()
    {
        if (SC_GM_Local.gm.wheelOfWords.Count >= 6)
            for (int i = 0; i < listOfWheel.Length; i++)
                if (i < wheelToLetter.Length)
                    wheelToLetter[i].text = listOfWheel[i].text;

    }
}