using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_GM_Local))]
public class SC_GMLocalEditor : Editor
{
    private SC_GM_Local gmLocal;
    SC_Base baseInfo;

    private void OnEnable()
    {
        gmLocal = target as SC_GM_Local;
        baseInfo = (SC_Base)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("Base", new[] { "Assets/ScriptableObjects" })[0]), typeof(SC_Base));
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Perso de la scene");
        gmLocal.persoOfCurrentScene = EditorGUILayout.Popup(gmLocal.persoOfCurrentScene, baseInfo.listOfPerso);

        EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Number of Scene");
        gmLocal.numberOfScene = EditorGUILayout.IntSlider(gmLocal.numberOfScene, 2, 3);

        EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("First Pivot Scene");
        gmLocal.firstPivotScene = EditorGUILayout.IntField(gmLocal.firstPivotScene);

        EditorGUILayout.EndHorizontal();
        if (gmLocal.numberOfScene == 3)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Second Pivot Scene");
            gmLocal.secondPivotScene = EditorGUILayout.IntField(gmLocal.secondPivotScene);

            EditorGUILayout.EndHorizontal();
        }



        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("First Scene");
        gmLocal.firstScene = EditorGUILayout.TextField(gmLocal.firstScene);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Second Scene");
        gmLocal.secondScene = EditorGUILayout.TextField(gmLocal.secondScene);

        EditorGUILayout.EndHorizontal();
        if (gmLocal.numberOfScene == 3)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Third Scene");
            gmLocal.thirdScene = EditorGUILayout.TextField(gmLocal.thirdScene);

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorUtility.SetDirty(gmLocal);
        base.OnInspectorGUI();
    }
}
