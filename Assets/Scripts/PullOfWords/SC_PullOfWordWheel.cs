using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_PullOfWordWheel : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_wheelWord;
    public GameObject GO_champsLexicaux;
    public GameObject GO_listParagrapheLettres;
    public Button startWrittingButton;

    // Info sur le CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private TextMeshProUGUI[][] champLexical;

    // Liste des mots de la wheel
    private TextMeshProUGUI[] listOfWheel;

    // Paragraphe d'autoComplete
    private SC_AutoComplete[] autoComplete;

    //##############################################################################################################################################################
    //########################################################################        INIT           ###############################################################
    //##############################################################################################################################################################

    /*
     * Init le CL, la Wheel et les paragraphes de la lettre
     */
    public void Collect()
    {
        InitWheel();
        InitParagrapheLettre();
    }

    /*
     * Init la partie de la Wheel
     */
    private void InitWheel()
    {
        InitRightSide();
        InitChampsLexicauxWheel();
    }

    /*
     * Recupere la liste des critere et des perso. Met a jour les perso
     */
    private void InitRightSide()
    {
        listOfWheel = GO_wheelWord.GetComponentsInChildren<TextMeshProUGUI>(true);
        BossHelp();
    }

    /*
     * Récupère la liste des CL et la liste des Word de chaque CL
     */
    private void InitChampsLexicauxWheel()
    {
        champsLexicaux = GO_champsLexicaux.GetComponentsInChildren<LayoutGroup>(true);
        champLexical = new TextMeshProUGUI[champsLexicaux.Length][];
        for (int i = 0; i < champsLexicaux.Length; i++)
            champLexical[i] = champsLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);

        WriteWordAndCLForWheel();
    }

    /*
     * Ecrit le nom des CL et des Words qu'ils contiennent
     */
    private void WriteWordAndCLForWheel()
    {
        for (int i = 0; i < SC_GM_Master.gm.wordsInCollect.Count; i++)
        {
            SC_CLInPull cl = SC_GM_Master.gm.wordsInCollect[i];
            champLexical[i][posElemCl].text = cl.GetCL();

            foreach (SC_Word word in cl.GetListWord())
                champLexical[i][GetFirstCLWordFreeForWheel(i)].text = word.titre;
        }
    }

    /*
    * Trouve le premier champ non utilisé du CL et retoune sa position
    */
    private int GetFirstCLWordFreeForWheel(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && champLexical[index][i].text == "")
                return i;

        return -1;
    }

    /*
     * Récupère l'ensemble des paragraphes disponibles pour écrire la lettre.
     */
    private void InitParagrapheLettre()
    {
        autoComplete = GO_listParagrapheLettres.GetComponentsInChildren<SC_AutoComplete>(true);
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

            listOfWheel[GetFirstWordInWheelFree()].text = word.titre;
            SC_GM_Local.gm.wheelOfWords.Add(word);

            BossHelp(1);
        }
    }


    /*
     * Trouve le mot des collects correspondant au TMP_UGUI (mot)
     */
    private SC_Word GetWordInCollect(string mot)
    {
        foreach (SC_CLInPull cl in SC_GM_Master.gm.wordsInCollect)
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
            {
                if (i == listOfWheel.Length - 1)
                    startWrittingButton.interactable = true;
                return i;
            }

        return -1;
    }

    /*
     * Remove le mot de la wheel
     */
     public void OnClickRemoveWordInWheel(TextMeshProUGUI tmp)
    {
        for (int i = 0; i < listOfWheel.Length; i++)
            if (listOfWheel[i] == tmp)
            {
                BossHelp();

                SC_GM_Local.gm.wheelOfWords.Remove(GetWordInCollect(tmp.text));
                listOfWheel[i].text = "";
            }

        startWrittingButton.interactable = false;
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
        foreach (SC_AutoComplete elem in autoComplete)
            elem.Init();

    }

    /*
     * Gère le tuto à la première scène
     */
    private void BossHelp(int val = 0)
    {
        if (SC_GM_Local.gm.activeBonus)
            if (SceneManager.GetActiveScene().name == "L_A1")
            {
                //SC_BossHelp.instance.CloseBossHelp(3);
                //SC_BossHelp.instance.OpenBossBubble(3);
            }
    }
}