using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_PullOfWordWheel : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_wheelWord;
    public GameObject GO_champsLexicaux;
    public Button startWrittingButton;
    public GameObject GO_wheelToLetter;

    // Images des buttons qui ne contiennent pas de mot
    public Sprite hasNotWord;

    // Info sur le CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private TextMeshProUGUI[][] champLexical;
    private Image[][] champLexicalImage;

    // Liste des mots de la wheel
    private TextMeshProUGUI[] listOfWheel;
    public TextMeshProUGUI[] wheelToLetter;

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

        wheelToLetter = GO_wheelToLetter.GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    /*
     * Recupere la liste des critere et des perso. Met a jour les perso
     */
    private void InitRightSide()
    {
        listOfWheel = GO_wheelWord.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI elem in listOfWheel)
            elem.text = "";

        BossHelp();
    }

    /*
     * Récupère la liste des CL et la liste des Word de chaque CL
     */
    private void InitChampsLexicauxWheel()
    {
        champsLexicaux = GO_champsLexicaux.GetComponentsInChildren<LayoutGroup>(true);

        champLexical = new TextMeshProUGUI[champsLexicaux.Length][];
        champLexicalImage = new Image[champsLexicaux.Length][];

        for (int i = 0; i < champsLexicaux.Length; i++)
        {
            champLexical[i] = champsLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI elem in champLexical[i])
                elem.text = "";

            champLexicalImage[i] = champsLexicaux[i].GetComponentsInChildren<Image>(true);
        }

        WriteWordAndCL();
    }

    /*
     * Ecrit le nom des CL et des Words qu'ils contiennent
     */
    private void WriteWordAndCL()
    {
        for (int i = 0; i < SC_GM_Local.gm.wordsInPreparatory.Count; i++)
        {
            SC_CLInPull cl = SC_GM_Local.gm.wordsInPreparatory[i];
            champLexical[i][posElemCl].text = cl.GetCL();

            foreach (SC_Word word in cl.GetListWord())
                champLexical[i][GetFirstCLWordFree(i)].text = word.titre;

            int j = GetFirstCLWordFree(i);
            if (j == -1) return;
            for (; j < numberOfElemInCL; j++)
                champLexicalImage[i][j].sprite = hasNotWord;
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
        for (int i = 0; i < listOfWheel.Length; i++)
            if (i < wheelToLetter.Length)
                wheelToLetter[i].text = listOfWheel[i].text;

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