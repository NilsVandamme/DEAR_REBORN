using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ParagrapheLettre))]
public class SC_ParagrapheLettreEditor : Editor
{
    private SC_ParagrapheLettre paragraphe;
    SerializedProperty listOfPartText;

    private void OnEnable()
    {
        paragraphe = target as SC_ParagrapheLettre;
        listOfPartText = serializedObject.FindProperty("texte");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        paragraphe.fileCSVTextParagraph = EditorGUILayout.ObjectField("File Text : ", paragraphe.fileCSVTextParagraph, typeof(TextAsset), false) as TextAsset;

        if (paragraphe.fileCSVTextParagraph != null)
        {
            if (GUILayout.Button("Load Paragraphe"))
                GenerateParagraphe();

            foreach (SerializedProperty text in listOfPartText)
                EditorGUILayout.PropertyField(text);

        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(paragraphe);
    }

    /*
     * Lis le csv du paragraphe
     */
    public void GenerateParagraphe()
    {
        string deb = "<link=\"", middle = "\">", fin = "</link>";

        string rawContent = paragraphe.fileCSVTextParagraph.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { "§" };
        string[] cells;

        List<TextPart> textInfo = new List<TextPart>();
        TextPart text = new TextPart();

        for (int i = 1; i < lineList.Length; i++)
        {
            int sep = lineList[i].LastIndexOf(",");
            lineList[i] = lineList[i].Substring(0, sep) + "§" + lineList[i].Substring(sep + 1, lineList[i].Length - (sep + 1));
            lineList[i] = lineList[i].Replace("\"", "");
            lineList[i] = lineList[i].Replace("£", "\n");

            cells = lineList[i].Split(separator, System.StringSplitOptions.None);
            

            if (cells[0] == "_____")
                if (i == lineList.Length - 1)
                    text.partText = deb + cells[1] + middle + cells[0] + fin;
                else
                    text.partText = deb + cells[1].Substring(0, cells[1].Length - 1) + middle + cells[0] + fin;
            else
                text.partText = cells[0];

            textInfo.Add(text);

        }

        paragraphe.texte = textInfo;
    }
}
