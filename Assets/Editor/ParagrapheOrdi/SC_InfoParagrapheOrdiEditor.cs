using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_InfoParagrapheOrdi))]
public class SC_InfoParagrapheOrdiEditor : Editor
{
    private SC_InfoParagrapheOrdi infoParagrapheOrdi;

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

        if (infoParagrapheOrdi.listCL != null)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Champs Lexical");
            infoParagrapheOrdi.cl = EditorGUILayout.Popup(actualCL, infoParagrapheOrdi.listCL.listNameChampLexical);

            EditorGUILayout.EndHorizontal();


            List<string> temp = new List<string>();
            foreach (SC_Word elem in infoParagrapheOrdi.listCL.listChampLexical[infoParagrapheOrdi.cl].listOfWords)
                temp.Add(elem.titre);

            if (actualCL != infoParagrapheOrdi.cl)
                infoParagrapheOrdi.word = 0;


            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Word");
            infoParagrapheOrdi.word = EditorGUILayout.Popup(infoParagrapheOrdi.word, temp.ToArray());

            EditorGUILayout.EndHorizontal();

        }

        base.OnInspectorGUI();
        EditorUtility.SetDirty(infoParagrapheOrdi);
        serializedObject.ApplyModifiedProperties();
    }
}
