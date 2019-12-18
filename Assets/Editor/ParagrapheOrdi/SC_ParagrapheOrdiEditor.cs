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
    private bool init;

    private void OnEnable()
    {
        paragrapheOrdi = target as SC_ParagrapheOrdi;
        listOfPartText = serializedObject.FindProperty("texte");
        init = true;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        paragrapheOrdi.fileCSVTextParagraph = EditorGUILayout.ObjectField("File Text : ", paragrapheOrdi.fileCSVTextParagraph, typeof(TextAsset), false) as TextAsset;
        paragrapheOrdi.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", paragrapheOrdi.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (init && paragrapheOrdi.fileCSVTextParagraph != null && paragrapheOrdi.listChampLexicaux != null)
        {
            init = false;

            int nbLink = GenerateParagraphe();
            foldoutList = new bool[nbLink];
            paragrapheOrdi.champLexical = new int[nbLink];
            paragrapheOrdi.motAccepterInCL = new bool[nbLink][];
        }
        if (!init)
        { 
            foreach (SerializedProperty text in listOfPartText)
                EditorGUILayout.PropertyField(text);
            
            for (int i = 0; i < foldoutList.Length; i++)
            {
                int actualCL = paragrapheOrdi.champLexical[i];
                List<SC_Word> listOfWordInActualCL = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[i]].listOfWords;

                EditorGUILayout.BeginHorizontal();

                foldoutList[i] = EditorGUILayout.Foldout(foldoutList[i], "Champ Lexical", true);
                paragrapheOrdi.champLexical[i] = EditorGUILayout.Popup(paragrapheOrdi.champLexical[i], paragrapheOrdi.listChampLexicaux.listNameChampLexical);

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

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(paragrapheOrdi);
    }

    /*
     * Lis le csv du paragraphe
     */
    private int GenerateParagraphe()
    {
        int cpt = 0;
        string deb = "<link=\"", middle = "\"><mark=#A7DEFF00>", fin = "</mark></link>";

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
                text.partText = deb + (cpt++) + middle + cells[0] + fin;
            else
                text.partText = cells[0];

            textInfo.Add(text);

        }

        paragrapheOrdi.texte = textInfo;
        return cpt;
    }
}


