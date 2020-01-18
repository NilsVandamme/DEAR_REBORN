using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_PullOfWordView : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_champsLexicaux;

    // Images des buttons qui ne contiennent pas de mot
    public Sprite hasNotWord;

    // Info sur le CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private TextMeshProUGUI[][] champLexicalWord;
    private Button[][] champLexicalButton;
    private Image[][] champLexicalImage;
    private bool[] isOpen;

    // Liste des superposition des CL
    private int[][] superpositionCL;

    //##############################################################################################################################################################
    //########################################################################        INIT           ###############################################################
    //##############################################################################################################################################################

    public void myStart()
    {
        InitCL();
        WriteWordAndCL();
        InitSuperposition();
    }

    /*
     * Init l'ensemble des CL
     */
    private void InitCL()
    {
        champsLexicaux = GO_champsLexicaux.GetComponentsInChildren<LayoutGroup>(true);

        champLexicalButton = new Button[champsLexicaux.Length][];
        champLexicalWord = new TextMeshProUGUI[champsLexicaux.Length][];
        champLexicalImage = new Image[champsLexicaux.Length][];
        isOpen = new bool[champsLexicaux.Length];

        for (int i = 0; i < champsLexicaux.Length; i++)
        {
            champLexicalButton[i] = champsLexicaux[i].GetComponentsInChildren<Button>(true);

            champLexicalWord[i] = champsLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI elem in champLexicalWord[i])
                elem.text = "";

            champLexicalImage[i] = champsLexicaux[i].GetComponentsInChildren<Image>(true);
        }
    }

    /*
     * Ecrit le nom des CL et des Words qu'ils contiennent
     */
    private void WriteWordAndCL()
    {
        for (int i = 0; i < SC_GM_Master.gm.wordsInPull.Count; i++)
        {
            SC_CLInPull cl = SC_GM_Master.gm.wordsInPull[i];
            champLexicalWord[i][posElemCl].text = cl.GetCL();

            foreach (SC_Word word in cl.GetListWord())
                champLexicalWord[i][GetFirstCLWordFree(i)].text = word.titre;

            int j = GetFirstCLWordFree(i);
            if (j == -1) return;
            for (; j < numberOfElemInCL; j++)
                if (j != posElemCl)
                    champLexicalImage[i][j].sprite = hasNotWord;
        }
    }

    /*
    * Trouve le premier champ non utilisé du CL et retoune sa position pour la choosen phase
    */
    private int GetFirstCLWordFree(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && champLexicalWord[index][i].text == "")
                return i;

        return -1;
    }

    private void InitSuperposition()
    {
        superpositionCL = new int[champsLexicaux.Length][];
        superpositionCL[0] = new int[] { 2, 3 };
        superpositionCL[1] = new int[] { 3, 4 };
        superpositionCL[2] = new int[] { 0, 3, 5, 6 };
        superpositionCL[3] = new int[] { 0, 1, 2, 4, 5, 6, 7 };
        superpositionCL[4] = new int[] { 1, 3, 6, 7 };
        superpositionCL[5] = new int[] { 2, 3, 6, 8 };
        superpositionCL[6] = new int[] { 2, 3, 4, 5, 7, 8, 9 };
        superpositionCL[7] = new int[] { 3, 4, 6, 9 };
        superpositionCL[8] = new int[] { 5, 6 };
        superpositionCL[9] = new int[] { 6, 7 };
    }

    //##############################################################################################################################################################
    //########################################################################        FONCTION         #############################################################
    //##############################################################################################################################################################

    /*
     * Affiche ou désafiche le CL sur lequel on a clicker
     */
    public void OnClickButtonCL(Button but)
    {
        for (int i = 0; i < champLexicalButton.Length; i++)
            if (champLexicalButton[i][posElemCl] == but)
            {
                isOpen[i] = !champLexicalButton[i][0].IsActive();
                OpenClose(i, isOpen[i]);
                CloseNeighbour(i);
            }
    }

    /*
     * Affiche/Désafiche un CL
     */
    private void OpenClose(int cl, bool openClose)
    {
        for (int i = 0; i < champLexicalButton[cl].Length; i++)
            if (i != posElemCl)
                champLexicalButton[cl][i].gameObject.SetActive(openClose);

    }

    /*
     * Désafiche les CL avoisinant
     */
    private void CloseNeighbour(int cl)
    {
        foreach (int elem in superpositionCL[cl])
        {
            OpenClose(elem, false);
            isOpen[elem] = false;
        }
    }
}