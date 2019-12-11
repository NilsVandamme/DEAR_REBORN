using System.Collections.Generic;
using System.Linq;
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
        paragrapheOrdi.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", paragrapheOrdi.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (paragrapheOrdi.listChampLexicaux != null)
        {
            int actualCL = paragrapheOrdi.champLexical;
            List<Word> listOfWordInActualCL = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical].listOfWords;


            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("Champ Lexical");
            paragrapheOrdi.champLexical = EditorGUILayout.Popup(paragrapheOrdi.champLexical, paragrapheOrdi.listChampLexicaux.listNameChampLexical);

            EditorGUILayout.EndHorizontal();


            if (paragrapheOrdi.champLexical != actualCL || paragrapheOrdi.motAccepterInCL == null)
                paragrapheOrdi.motAccepterInCL = new bool[listOfWordInActualCL.Count];


            for (int i = 0; i < listOfWordInActualCL.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(listOfWordInActualCL[i].titre);
                paragrapheOrdi.motAccepterInCL[i] = EditorGUILayout.Toggle(paragrapheOrdi.motAccepterInCL[i]);

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorUtility.SetDirty(paragrapheOrdi);
    }
}
