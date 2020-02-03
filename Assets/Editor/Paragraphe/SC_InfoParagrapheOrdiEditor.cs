using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_InfoParagrapheOrdi))]
public class SC_InfoParagrapheOrdiEditor : Editor
{
    private SC_InfoParagrapheOrdi infoParagrapheOrdi;
    private string[] choix = {"CL", "Infos"};

    private void OnEnable()
    {
        infoParagrapheOrdi = target as SC_InfoParagrapheOrdi;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        int actualCL = infoParagrapheOrdi.cl;

        base.OnInspectorGUI();

        Recoltable(actualCL);

        EditorUtility.SetDirty(infoParagrapheOrdi);
        serializedObject.ApplyModifiedProperties();
    }

    /*
     * Regarde si oui ou non le paragraphe et recoltable
     */
    private void Recoltable(int actualCL)
    {
        if (infoParagrapheOrdi.listCL != null && infoParagrapheOrdi.recoltable)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Choix :");
            infoParagrapheOrdi.choix = EditorGUILayout.Popup(infoParagrapheOrdi.choix, choix);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUI.indentLevel += 1;

            if (infoParagrapheOrdi.choix == 0)
                ChoixCL(actualCL);
            else
                ChoixInfos();

            EditorGUI.indentLevel -= 1;
        }
    }

    /*
     * Si le paragraphe permet de recup un Cl
     */
    private void ChoixCL(int actualCL)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Champs Lexical :");
        infoParagrapheOrdi.cl = EditorGUILayout.Popup(actualCL, infoParagrapheOrdi.listCL.listNameChampLexical);

        EditorGUILayout.EndHorizontal();


        List<string> temp = new List<string>();
        foreach (SC_Word elem in infoParagrapheOrdi.listCL.listChampLexical[infoParagrapheOrdi.cl].listOfWords)
            temp.Add(elem.titre);

        if (actualCL != infoParagrapheOrdi.cl)
            infoParagrapheOrdi.word = 0;


        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Word :");
        infoParagrapheOrdi.word = EditorGUILayout.Popup(infoParagrapheOrdi.word, temp.ToArray());

        EditorGUILayout.EndHorizontal();
    }

    /*
     * Si le paragraphe permet de recup des infos perso
     */
    private void ChoixInfos()
    {
        infoParagrapheOrdi.textInfos = EditorGUILayout.TextArea(infoParagrapheOrdi.textInfos, GUILayout.Height(150));
    }
}


