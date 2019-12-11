using System;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ListChampLexicaux))]
public class SC_ListChampLexicauxEditor : Editor
{
    private SC_ListChampLexicaux listChampLexicaux;

    private void OnEnable()
    {
        listChampLexicaux = target as SC_ListChampLexicaux;

        loadInfo();
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Champs Lexicaux"))
            loadName();

        base.OnInspectorGUI();

    }

    /*
     * Calcul la liste des champs lexicaux dispo
     */
    private void loadName ()
    {
        int nbChampLexical = listChampLexicaux.listChampLexical.Length;

        listChampLexicaux.listNameChampLexical = new string[nbChampLexical];
        for (int i = 0; i < nbChampLexical; i++)
            listChampLexicaux.listNameChampLexical[i] = listChampLexicaux.listChampLexical[i].fileCSVChampLexical.name;

    }

    private void loadInfo()
    {
        listChampLexicaux.titre = "Titre";

        listChampLexicaux.listOfGrammarCritere = new string[] { "Present", "Passe", "Ing", "Noun", "Adjectif" };

        listChampLexicaux.listOfPerso = new string[] { "Granny Donna", "Mr. S" };
}
}
