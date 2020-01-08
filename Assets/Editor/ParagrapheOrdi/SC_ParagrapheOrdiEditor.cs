using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ParagrapheOrdi))]
public class SC_ParagrapheOrdiEditor : Editor
{
    private SC_ParagrapheOrdi paragrapheOrdi;
    SerializedProperty listOfPartText;
    private bool[] foldoutList;
    private TextAsset saveFileCSV;

    private void OnEnable()
    {
        paragrapheOrdi = target as SC_ParagrapheOrdi;
        listOfPartText = serializedObject.FindProperty("texte");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        paragrapheOrdi.fileCSVTextParagraph = EditorGUILayout.ObjectField("File Text : ", paragrapheOrdi.fileCSVTextParagraph, typeof(TextAsset), false) as TextAsset;
        paragrapheOrdi.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", paragrapheOrdi.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (paragrapheOrdi.fileCSVTextParagraph != null && paragrapheOrdi.listChampLexicaux != null)
        {
            int nbLink = GenerateParagraphe();

            if (paragrapheOrdi.fileCSVTextParagraph != saveFileCSV)
            {
                foldoutList = new bool[nbLink];
                paragrapheOrdi.champLexical = new int[nbLink];
                paragrapheOrdi.motAccepterInCL = new bool[nbLink][];
            }
            saveFileCSV = paragrapheOrdi.fileCSVTextParagraph;

            foreach (SerializedProperty text in listOfPartText)
                EditorGUILayout.PropertyField(text);

            for (int i = 0; i < foldoutList.Length; i++)
            {
                int actualCL = paragrapheOrdi.champLexical[i];

                EditorGUILayout.BeginHorizontal();

                foldoutList[i] = EditorGUILayout.Foldout(foldoutList[i], "Champ Lexical", true);

                paragrapheOrdi.champLexical[i] = EditorGUILayout.Popup(actualCL, paragrapheOrdi.listChampLexicaux.listNameChampLexical);
                List<SC_Word> listOfWordInActualCL = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[i]].listOfWords;

                EditorGUILayout.EndHorizontal();

                if (foldoutList[i])
                {
                    EditorGUI.indentLevel += 1;

                    if (paragrapheOrdi.champLexical[i] != actualCL || paragrapheOrdi.motAccepterInCL[i] == null)
                        paragrapheOrdi.motAccepterInCL[i] = new bool[listOfWordInActualCL.Count];

                    for (int j = 0; j < listOfWordInActualCL.Count; j++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(listOfWordInActualCL[j].titre);
                        paragrapheOrdi.motAccepterInCL[i][j] = EditorGUILayout.Toggle(paragrapheOrdi.motAccepterInCL[i][j]);

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel -= 1;
                }

            }
        }

        EditorUtility.SetDirty(paragrapheOrdi);

        serializedObject.ApplyModifiedProperties();
    }

    /*
     * Lis le csv du paragraphe
     */
    private int GenerateParagraphe()
    {
        int cpt = 0;
        string deb = "<link=\"", middle = "\"><mark=#A7DEFF00><color=#7F4428ff>", fin = "</color></mark></link>";

        string rawContent = paragrapheOrdi.fileCSVTextParagraph.text;
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

            cells = lineList[i].Split(separator, System.StringSplitOptions.None);


            if (cells[1].Substring(0, cells[1].Length - 1) == "link" || cells[1] == "link")
                text.partText = deb + 'B' + (cpt++) + middle + cells[0] + fin;
            else
                text.partText = deb + 'A' + middle + cells[0] + fin;

            textInfo.Add(text);

        }

        paragrapheOrdi.texte = textInfo;
        return cpt;
    }
}


