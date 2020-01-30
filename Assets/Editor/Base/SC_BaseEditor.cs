using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_Base))]
public class SC_BaseEditor : Editor
{
    private SC_Base baseInfo;

    private void OnEnable()
    {
        baseInfo = target as SC_Base;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        baseInfo.fileCSVBase = EditorGUILayout.ObjectField("File Base : ", baseInfo.fileCSVBase, typeof(TextAsset), false) as TextAsset;

        if (baseInfo.fileCSVBase != null)
        {
            if (GUILayout.Button("Load Base"))
                GenerateBase();

        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(baseInfo);
    }

    /*
     * Lis le csv de la Base
     */
    private void GenerateBase()
    {
        int numberOfCritere = 5;

        string rawContent = baseInfo.fileCSVBase.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { "," };
        string[] cells = lineList[0].Split(separator, System.StringSplitOptions.None);

        baseInfo.titre = cells[0];

        baseInfo.listOfGrammarCritere = new string[numberOfCritere];
        for (int j = 0; j < numberOfCritere; j++)
            baseInfo.listOfGrammarCritere[j] = cells[j + 1];

        baseInfo.listOfPerso = new string[cells.Length - (numberOfCritere + 1)];
        for (int j = (numberOfCritere + 1); j < cells.Length; j++)
            baseInfo.listOfPerso[j - (numberOfCritere + 1)] = cells[j];

    }
}
