using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ButtonPerso))]
public class SC_ButtonPersoEditor : Editor
{
    private SC_ButtonPerso buttonPerso;
    SC_Base baseInfo;

    private void OnEnable()
    {
        buttonPerso = target as SC_ButtonPerso;
        baseInfo = (SC_Base)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("Base", new[] { "Assets/ScriptableObjects" })[0]), typeof(SC_Base));
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Perso de la scene");
        buttonPerso.persoOfButton = EditorGUILayout.Popup(buttonPerso.persoOfButton, baseInfo.listOfPerso);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorUtility.SetDirty(buttonPerso);
        base.OnInspectorGUI();
    }
}
