using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_BaseTimbre))]
public class SC_BaseTimbreEditor : Editor
{
    private SC_BaseTimbre baseTimbre;
    SerializedProperty images;
    private bool loadImage;

    private void OnEnable()
    {
        baseTimbre = target as SC_BaseTimbre;
        images = serializedObject.FindProperty("images");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        baseTimbre.fileCSVBaseTimbre = EditorGUILayout.ObjectField("File Text : ", baseTimbre.fileCSVBaseTimbre, typeof(TextAsset), false) as TextAsset;
        EditorGUILayout.PropertyField(images, new GUIContent("Images"), true);

        loadImage = true;
        foreach (SerializedProperty elem in images)
            if (elem.objectReferenceValue == null)
            {
                loadImage = false;
                break;
            }
        

        if (baseTimbre.fileCSVBaseTimbre != null && loadImage)
        {
            if (GUILayout.Button("Load Base Image"))
                GenerateBaseImage();

        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(baseTimbre);
    }

    /*
     * Lis le csv du paragraphe
     */
    private void GenerateBaseImage()
    {
        string rawContent = baseTimbre.fileCSVBaseTimbre.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        if (lineList.Length != images.arraySize + 1)
            return;

        string[] separator = new string[] { "§" };
        string[] cells;

        baseTimbre.timbres = new List<SC_Timbres>();

        for (int i = 1; i < lineList.Length; i++)
        {
            int sep = lineList[i].LastIndexOf(",");
            lineList[i] = lineList[i].Substring(0, sep) + "§" + lineList[i].Substring(sep + 1, lineList[i].Length - (sep + 1));
            lineList[i] = lineList[i].Replace("\"", "");
            
            cells = lineList[i].Split(separator, System.StringSplitOptions.None);

            int j;
            for (j = 0; j < images.arraySize; j++)
            {
                if (images.GetArrayElementAtIndex(j).objectReferenceValue.name == cells[0])
                    break;
            }

            if (j < images.arraySize)
                baseTimbre.timbres.Add(new SC_Timbres(cells[0], (Texture2D)images.GetArrayElementAtIndex(j).objectReferenceValue, bool.Parse(cells[1])));

        }
    }
}
