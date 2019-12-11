using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_GM_Local))]
public class SC_GMLocalEditor : Editor
{
    private SC_GM_Local gmLocal;

    private void OnEnable()
    {
        gmLocal = target as SC_GM_Local;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Perso de la scene");
        gmLocal.peopleScore = EditorGUILayout.Popup(gmLocal.peopleScore, SC_GM_Master.gm.listChampsLexicaux.listOfPerso);

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
