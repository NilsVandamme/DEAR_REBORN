using System;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ListChampLexicaux))]
public class SC_ListChampLexicauxEditor : Editor
{
    private SC_ListChampLexicaux listChampLexicaux;
    SC_Base baseInfo;

    private void OnEnable()
    {
        listChampLexicaux = target as SC_ListChampLexicaux;
        baseInfo = (SC_Base)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("Base", new[] { "Assets/ScriptableObjects" })[0]), typeof(SC_Base));
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
        listChampLexicaux.titre = baseInfo.titre;

        listChampLexicaux.listOfGrammarCritere = baseInfo.listOfGrammarCritere;

        listChampLexicaux.listOfPerso = baseInfo.listOfPerso;
    }
}
