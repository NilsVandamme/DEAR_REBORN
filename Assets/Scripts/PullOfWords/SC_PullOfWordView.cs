using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_PullOfWordView : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_champsLexicaux;

    // Images des buttons qui ne contiennent pas de mot
    public Image hasNotWord;

    // Info sur le CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private TextMeshProUGUI[][] champLexical;
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

        champLexical = new TextMeshProUGUI[champsLexicaux.Length][];
        champLexicalImage = new Image[champsLexicaux.Length][];
        isOpen = new bool[champsLexicaux.Length];

        for (int i = 0; i < champsLexicaux.Length; i++)
        {
            champLexical[i] = champsLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
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
            champLexical[i][posElemCl].text = cl.GetCL();

            foreach (SC_Word word in cl.GetListWord())
                champLexical[i][GetFirstCLWordFree(i)].text = word.titre;

            int j = GetFirstCLWordFree(i);
            if (j == -1) return;
            for (; j < numberOfElemInCL; j++)
                champLexicalImage[i][j] = hasNotWord;
        }
    }

    /*
    * Trouve le premier champ non utilisé du CL et retoune sa position pour la choosen phase
    */
    private int GetFirstCLWordFree(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && champLexical[index][i].text == "")
                return i;

        return -1;
    }

    private void InitSuperposition()
    {
        superpositionCL = new int[champsLexicaux.Length][];
        superpositionCL[0] = new int[] { 1, 3 };
        superpositionCL[1] = new int[] { 0, 2, 3, 4};
        superpositionCL[2] = new int[] { 1, 4 };
        superpositionCL[3] = new int[] { 0, 1, 5, 6 };
        superpositionCL[4] = new int[] { 1, 2, 6, 7 };
        superpositionCL[5] = new int[] { 3, 6, 8 };
        superpositionCL[6] = new int[] { 3, 4, 5, 7, 8, 9 };
        superpositionCL[7] = new int[] { 4, 6, 9 };
        superpositionCL[8] = new int[] { 5, 6 };
        superpositionCL[9] = new int[] { 6, 7 };
    }

    //##############################################################################################################################################################
    //########################################################################        FONCTION         #############################################################
    //##############################################################################################################################################################

    /*
     * Affiche ou désafiche le CL sur lequel on a clicker
     */
    public void OnClickButtonCL(TextMeshProUGUI tmp)
    {
        for (int i = 0; i < champLexical.Length; i++)
            if (champLexical[i][posElemCl] == tmp)
            {
                isOpen[i] = !isOpen[i];
                OpenClose(i, isOpen[i]);
                CloseNeighbour(i);
            }
    }

    /*
     * Affiche/Désafiche un CL
     */
    private void OpenClose(int cl, bool openClose)
    {
        for (int i = 0; i < champLexical[cl].Length; i++)
            if (i != posElemCl)
                champLexical[cl][i].gameObject.SetActive(openClose);

    }

    /*
     * Désafiche les CL avoisinant
     */
    private void CloseNeighbour(int cl)
    {
        foreach (int elem in superpositionCL[cl])
            OpenClose(elem, false);
    }
}