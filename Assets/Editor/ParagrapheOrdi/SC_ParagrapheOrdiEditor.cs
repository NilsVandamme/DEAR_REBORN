using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ParagrapheOrdi))]
public class SC_ParagrapheOrdiEditor : Editor
{
    private SC_ParagrapheOrdi paragrapheOrdi;
    SerializedProperty listOfPartText;

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
            if (!paragrapheOrdi.init)
                Init();

            foreach (SerializedProperty text in listOfPartText)
                EditorGUILayout.PropertyField(text);

            for (int i = 0; i < paragrapheOrdi.foldoutList.Length; i++)
            {
                int actualCL = paragrapheOrdi.champLexical[i];

                EditorGUILayout.BeginHorizontal();

                paragrapheOrdi.foldoutList[i] = EditorGUILayout.Foldout(paragrapheOrdi.foldoutList[i], "Champ Lexical", true);

                paragrapheOrdi.champLexical[i] = EditorGUILayout.Popup(actualCL, paragrapheOrdi.listChampLexicaux.listNameChampLexical);
                List<SC_Word> listOfWordInActualCL = paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[i]].listOfWords;

                EditorGUILayout.EndHorizontal();

                if (paragrapheOrdi.foldoutList[i])
                {
                    EditorGUI.indentLevel += 1;
                    
                    int pos = 0;
                    for (int h = 0; h < i; h++)
                        pos += paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[h]].listOfWords.Count;


                    if (paragrapheOrdi.motAccepterInCL == null)
                        InitMotAccepter();
                    

                    if (paragrapheOrdi.champLexical[i] != actualCL)
                        ReinitAfterChange(i, actualCL);


                    for (int j = 0; j < listOfWordInActualCL.Count; j++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(listOfWordInActualCL[j].titre);
                        paragrapheOrdi.motAccepterInCL[pos + j] = EditorGUILayout.Toggle(paragrapheOrdi.motAccepterInCL[pos + j]);

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel -= 1;
                }

            }
        }

        EditorUtility.SetDirty(paragrapheOrdi);

        serializedObject.ApplyModifiedProperties();
    }

    private void Init()
    {
        int nbLink = GenerateParagraphe();

        paragrapheOrdi.foldoutList = new bool[nbLink];
        paragrapheOrdi.champLexical = new int[nbLink];

        paragrapheOrdi.init = true;

    }

    /*
     * Init motAccepter a la longueur des CL selectionner
     */
    private void InitMotAccepter()
    {
        int lenght = 0;
        for (int h = 0; h < paragrapheOrdi.champLexical.Length; h++)
            lenght += paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[h]].listOfWords.Count;

        paragrapheOrdi.motAccepterInCL = new bool[lenght];

    }

    /*
     * Lors d'un change de CL, reinit le tab des motAccepter à la bonne taille, et lui remet les infos des autre CL non modifie
     */
    private void ReinitAfterChange(int newCL, int oldCL)
    {
        bool[] temp = new bool[paragrapheOrdi.motAccepterInCL.Length];

        for (int i = 0; i < temp.Length; i++)
            temp[i] = paragrapheOrdi.motAccepterInCL[i];

        InitMotAccepter();

        int posNew = 0, posOld = 0;
        for (int i = 0; i < paragrapheOrdi.champLexical.Length; i++)
            if (i != newCL)
                for (int j = 0; j < paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[i]].listOfWords.Count; j++)
                    paragrapheOrdi.motAccepterInCL[posNew++] = temp[posOld++];
            else
            {
                for (int j = 0; j < paragrapheOrdi.listChampLexicaux.listChampLexical[paragrapheOrdi.champLexical[newCL]].listOfWords.Count; j++)
                    paragrapheOrdi.motAccepterInCL[posNew++] = false;

                for (int j = 0; j < paragrapheOrdi.listChampLexicaux.listChampLexical[oldCL].listOfWords.Count; j++)
                    posOld++;
            }
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


