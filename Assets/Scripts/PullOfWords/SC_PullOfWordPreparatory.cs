using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PullOfWordPreparatory : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_champsLexicaux;

    // Liste des Images des buttons en fct de s'il contiennent ou non un mot
    public Image hasWord;
    public Image notHasWord;

    // Info sur le CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private TextMeshProUGUI[][] champLexical;
    private Image[][] champLexicalImage;

    // Liste des nombres d'elem possible pour les CL
    private List<int> versus2 = new List<int> { 2, 4, 6, 8 };
    private List<int> versus3 = new List<int> { 7, 9, 10 };

    // Liste des CL afficher pendant le choix
    private List<SC_CLInPull> choixCLTemp;
    

    //##############################################################################################################################################################
    //########################################################################         INIT           ##############################################################
    //##############################################################################################################################################################

    /*
     * Init la preparatory phase
     */
    public void InitPreparatory()
    {
        InitCL();
        InitView();
    }

    /*
     * Init la partie du CL
     */
    private void InitCL()
    {
        champsLexicaux = GO_champsLexicaux.GetComponentsInChildren<LayoutGroup>(true);

        champLexical = new TextMeshProUGUI[champsLexicaux.Length][];
        champLexicalImage = new Image[champsLexicaux.Length][];

        for (int i = 0; i < champsLexicaux.Length; i++)
        {
            champLexical[i] = champsLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
            champLexicalImage[i] = champsLexicaux[i].GetComponentsInChildren<Image>(true);
        }

    }

    /*
     * Init la vue des CL dispo pour le choix
     */
    private void InitView()
    {
        choixCLTemp = new List<SC_CLInPull>();
        MakeVersus();
    }

    //##############################################################################################################################################################
    //########################################################################        FONCTION         #############################################################
    //##############################################################################################################################################################

    /*
    * Regarde s'il faut afficher un conflit entre 2 CL ou entre 3
    */
    private void MakeVersus()
    {
        int x = Random.Range(0, SC_GM_Master.gm.wordsInCollect.Count), y, z;

        do
        {
            y = Random.Range(0, SC_GM_Master.gm.wordsInCollect.Count);
        }
        while (y == x);


        if (versus2.Contains(SC_GM_Master.gm.wordsInCollect.Count))
        {
            Swap(x, y);
        }
        else if (versus3.Contains(SC_GM_Master.gm.wordsInCollect.Count))
        {
            do
            {
                z = Random.Range(0, SC_GM_Master.gm.wordsInCollect.Count);
            }
            while (z == x || z == y);

            Swap(x, y, z);
        }
        else if (SC_GM_Master.gm.wordsInCollect.Count <= 0)
        {
            // TO-DO --> changement de scene vers Wheel
        }
    }

    /*
     * S'occupe du swap des CL d'1 list a une autre
     */
    private void Swap(int x, int y, int z = -1)
    {
        SC_CLInPull temp;

        temp = SC_GM_Master.gm.wordsInCollect[x];
        choixCLTemp.Add(temp);

        temp = SC_GM_Master.gm.wordsInCollect[y];
        choixCLTemp.Add(temp);

        if (z != -1)
        {
            temp = SC_GM_Master.gm.wordsInCollect[z];
            choixCLTemp.Add(temp);
        }

        foreach( SC_CLInPull elem in choixCLTemp)
            SC_GM_Master.gm.wordsInCollect.Remove(elem);

        Affiche();
    }

    /*
     * Gère l'affichage des CL dans la preparatory
     */
    private void Affiche()
    {
        for (int i = 0; i < choixCLTemp.Count; i++)
        {
            champsLexicaux[i].gameObject.SetActive(true);
            champLexical[i][posElemCl].text = choixCLTemp[i].GetCL();

            int nbMot = choixCLTemp[i].GetListWord().Count;
            for (int j = 0; j < champLexical[i].Length; j++)
                if (j != posElemCl)
                    if (nbMot > 0)
                    {
                        nbMot--;
                        champLexicalImage[i][j] = hasWord;
                    }
                    else
                        champLexicalImage[i][j] = notHasWord;
        }
    }

    /*
     * Ajoute le Cl clicker dans le pull de Word et recommence le Versus
     */
    public void OnClickCL(TextMeshProUGUI tmp)
    {
        SC_CLInPull temp = GetCLInClicker(tmp);

        choixCLTemp.Clear();

        foreach (SC_CLInPull elem in SC_GM_Master.gm.wordsInPull)
            if (elem.GetCL() == temp.GetCL()) // Si le CL est deja present dans le pull
            {
                foreach (SC_Word word in temp.GetListWord())
                    if (!elem.GetListWord().Contains(word))
                        elem.GetListWord().Add(word);

                return;
            }

        SC_GM_Master.gm.wordsInPull.Add(temp);

        MakeVersus();
    }



    /*
     * Recupère le SC_CLInPull du Versus sur lequel on a clicker
     */
    private SC_CLInPull GetCLInClicker(TextMeshProUGUI tmp)
    {
        foreach (SC_CLInPull elem in choixCLTemp)
            if (elem.GetCL() == tmp.text)
                return elem;

        return null;
    }
}