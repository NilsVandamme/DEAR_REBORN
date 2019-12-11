using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ChampLexical))]
public class SC_ChampLexicalEditor : Editor
{
    private SC_ChampLexical champLexical;
    SerializedProperty listOfWords;
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

            foldoutListOfWord = EditorGUILayout.Foldout(foldoutListOfWord, "List for " + champLexical.fileCSVChampLexical.name, true);
            if (foldoutListOfWord)
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

        List<Word> wordInfos = new List<Word>();
        Word word;
        
        for (int i = 1; i < lineList.Length; i++)
        {
            cells = lineList[i].Split(separator, System.StringSplitOptions.None);
            word = new Word();

            word.titre = cells[0];

            word.grammarCritere = new string[numberOfCritere];
            for (int j = 0; j < numberOfCritere; j++)
                word.grammarCritere[j] = cells[j + 1];

            word.scorePerso = new int[cells.Length - (numberOfCritere + 1)];
            for (int j = (numberOfCritere + 1); j < cells.Length; j++)
                int.TryParse(cells[j], out word.scorePerso[j - (numberOfCritere + 1)]);
            
            wordInfos.Add(word);
        }

        champLexical.listOfWords = wordInfos;
    }
}
