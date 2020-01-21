using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PullOfWordPreparatory : MonoBehaviour
{
    // Object de la fenetre
    public GameObject GO_champsLexicaux;
    public GameObject next;
    public TextMeshProUGUI vs;

    // Liste des Sprites des buttons en fct de s'il contiennent ou non un mot
    public Sprite hasWord;
    public Sprite hasNotWord;

    // Info sur le CL
    private int posElemCl = 4;

    // Liste des CL et leurs Words
    private LayoutGroup[] champsLexicaux;
    private TextMeshProUGUI[][] champLexical;
    private Image[][] champLexicalImage;

    // Liste des nombres d'elem possible pour les CL
    private List<int> versus2 = new List<int> { 2, 4, 6, 8 };
    private List<int> versus3 = new List<int> { 3, 5, 7, 9, 10 };

    // Liste des CL afficher pendant le choix
    private List<SC_CLInPull> choixCLTemp;
    private List<SC_CLInPull> copieCLTemp;
    

    //##############################################################################################################################################################
    //########################################################################         INIT           ##############################################################
    //##############################################################################################################################################################

    /*
     * Init la preparatory phase
     */
    public void InitPreparatory()
    {
        ChangeOfListe();
        InitCL();
        InitView();
    }

    /*
     * Pass les elem de la collect a la pull
     */
    private void ChangeOfListe()
    {
        bool find, contain;
        foreach (SC_CLInPull elem in SC_GM_Local.gm.wordsInCollect)
        {
            find = false;

            for (int i = 0; i < SC_GM_Master.gm.wordsInPull.Count; i++)
                if (SC_GM_Master.gm.wordsInPull[i].GetCL() == elem.GetCL())
                {
                    find = true;

                    foreach (SC_Word word in elem.GetListWord())
                    {
                        contain = false;

                        foreach (SC_Word word2 in SC_GM_Master.gm.wordsInPull[i].GetListWord())
                            if (word.titre.Equals(word2.titre))
                            {
                                contain = true;
                                break;
                            }

                        if (!contain)
                            SC_GM_Master.gm.wordsInPull[i].GetListWord().Add(word);
                    }

                    break;
                }

            if (!find)
                SC_GM_Master.gm.wordsInPull.Add(elem);
        }

        copieCLTemp = new List<SC_CLInPull>(SC_GM_Master.gm.wordsInPull);

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
            foreach (TextMeshProUGUI elem in champLexical[i])
                elem.text = "";

            champLexicalImage[i] = RemoveAT(champsLexicaux[i].GetComponentsInChildren<Image>(true), 0);
        }

    }

    /*
     * Generique fct de removeAt pour des tab
     */
    private T[] RemoveAT<T>(T[] tab, int index)
    {
        if (index < 0 || tab.Length < 1)
            return tab;

        T[] newTab = new T[tab.Length - 1];
        int pos = 0;

        for (int i = 0; i < tab.Length; i++)
            if (i != index)
                newTab[pos++] = tab[i];

        return newTab;
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
        int nbCLRestant = copieCLTemp.Count;

        if (nbCLRestant <= 0)
        {
            next.gameObject.SetActive(true);
            vs.gameObject.SetActive(false);

            foreach (LayoutGroup elem in champsLexicaux)
                elem.gameObject.SetActive(false);
        }
        else if (nbCLRestant == 1)
        {
            Debug.LogError("1 seul CL !!!");
        }
        else
        {
            int x = Random.Range(0, nbCLRestant), y, z;

            do
            {
                y = Random.Range(0, nbCLRestant);
            }
            while (y == x);

            if (versus2.Contains(nbCLRestant))
            {
                Swap(x, y);
            }
            else if (versus3.Contains(nbCLRestant))
            {
                do
                {
                    z = Random.Range(0, nbCLRestant);
                }
                while (z == x || z == y);

                Swap(x, y, z);
            }
        }

    }

    /*
     * S'occupe du swap des CL d'1 list a une autre
     */
    private void Swap(int x, int y, int z = -1)
    {
        SC_CLInPull temp;

        temp = copieCLTemp[x];
        choixCLTemp.Add(temp);

        temp = copieCLTemp[y];
        choixCLTemp.Add(temp);

        if (z != -1)
        {
            temp = copieCLTemp[z];
            choixCLTemp.Add(temp);
        }

        foreach(SC_CLInPull elem in choixCLTemp)
            copieCLTemp.Remove(elem);

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
                {
                    if (nbMot > 0)
                    {
                        nbMot--;
                        champLexicalImage[i][j].sprite = hasWord;
                    }
                    else
                        champLexicalImage[i][j].sprite = hasNotWord;
                }
        }
    }

    /*
     * Remet l'affichage à zéro
     */
    private void Clear()
    {
        for (int i = 0; i < choixCLTemp.Count; i++)
            champsLexicaux[i].gameObject.SetActive(false);
    }

    /*
     * Ajoute le Cl clicker dans la preparatory de Word et recommence le Versus
     */
    public void OnClickCL(TextMeshProUGUI tmp)
    {
        SC_CLInPull temp = GetCLInClicker(tmp);
        Clear();

        choixCLTemp.Clear();

        SC_GM_Local.gm.wordsInPreparatory.Add(temp);
        
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