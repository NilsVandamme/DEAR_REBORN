using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ChampLexical))]
public class SC_ChampLexicalEditor : Editor
{
    private SC_ChampLexical champLexical;
    private SerializedProperty listOfWords;
    private bool foldoutListCL;
    private bool foldoutListOfWord;
    
    private void OnEnable()
    {
        champLexical = target as SC_ChampLexical;
        listOfWords = serializedObject.FindProperty("listOfWords");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        champLexical.fileCSVChampLexical = EditorGUILayout.ObjectField("File words : ", champLexical.fileCSVChampLexical, typeof(TextAsset), false) as TextAsset;

        if (champLexical.fileCSVChampLexical != null)
        {
            if (GUILayout.Button("Load Champ Lexical"))
                GenerateCL();

            foldoutListCL = EditorGUILayout.Foldout(foldoutListCL, "List for " + champLexical.fileCSVChampLexical.name, true);
            if (foldoutListCL)
            {
                EditorGUI.indentLevel += 1;

                foreach (SerializedProperty elem in listOfWords)
                {
                    elem.isExpanded = EditorGUILayout.Foldout(elem.isExpanded, elem.FindPropertyRelative("titre").stringValue, true);
                    if (elem.isExpanded)
                    {
                        EditorGUI.indentLevel += 1;
                        EditorGUILayout.PropertyField(elem);
                        EditorGUI.indentLevel -= 1;
                    }
                }

                EditorGUI.indentLevel -= 1;
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(champLexical);
    }

    /*
     * Lis le csv du CL
     */
    private void GenerateCL()
    {
        int numberOfCritere = 5;

        string rawContent = champLexical.fileCSVChampLexical.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { "," };
        string[] cells;

        List<SC_Word> wordInfos = new List<SC_Word>();
        
        for (int i = 1; i < lineList.Length; i++)
        {
            cells = lineList[i].Split(separator, System.StringSplitOptions.None);

           string titre = cells[0];

            string[] critere = new string[numberOfCritere];
            for (int j = 0; j < numberOfCritere; j++)
                critere[j] = cells[j + 1].ToLower();

            int[] score = new int[cells.Length - (numberOfCritere + 1)];
            for (int j = (numberOfCritere + 1); j < cells.Length; j++)
                int.TryParse(cells[j], out score[j - (numberOfCritere + 1)]);
            
            wordInfos.Add(new SC_Word(score, titre, critere));
        }

        champLexical.listOfWords = wordInfos;
    }
}
