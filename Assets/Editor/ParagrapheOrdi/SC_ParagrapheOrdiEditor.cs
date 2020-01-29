using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ParagrapheOrdi))]
public class SC_ParagrapheOrdiEditor : Editor
{
    private SC_ParagrapheOrdi paragrapheOrdi;

    private void OnEnable()
    {
        paragrapheOrdi = target as SC_ParagrapheOrdi;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        paragrapheOrdi.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", paragrapheOrdi.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (paragrapheOrdi.listChampLexicaux != null)
        {
            int actualCL = paragrapheOrdi.champLexical;

            EditorGUILayout.BeginHorizontal();

            paragrapheOrdi.foldoutList = EditorGUILayout.Foldout(paragrapheOrdi.foldoutList, "Champ Lexical", true);
            paragrapheOrdi.champLexical = EditorGUILayout.Popup(actualCL, paragrapheOrdi.listChampLexicaux.listNameChampLexical);

            EditorGUILayout.EndHorizontal();

            if (paragrapheOrdi.foldoutList)
            {
                EditorGUI.indentLevel += 1;
                    
                if (paragrapheOrdi.motAccepterInCL == null || paragrapheOrdi.champLexical != actualCL)
                    paragrapheOrdi.motAccepterInCL = new bool[paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].listOfWords.Count];

                List<SC_Word> listOfWordInActualCL = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].listOfWords;

                for (int j = 0; j < listOfWordInActualCL.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(listOfWordInActualCL[j].titre);
                    paragrapheOrdi.motAccepterInCL[j] = EditorGUILayout.Toggle(paragrapheOrdi.motAccepterInCL[j]);

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel -= 1;
            }

        }

        EditorUtility.SetDirty(paragrapheOrdi);

        serializedObject.ApplyModifiedProperties();
    }
}


