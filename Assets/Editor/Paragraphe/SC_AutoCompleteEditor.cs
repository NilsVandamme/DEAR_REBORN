using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_AutoComplete))]
public class SC_AutoCompleteEditor : Editor
{
    private SC_AutoComplete autocomplete;
    SC_Base baseInfo;

    private void OnEnable()
    {
        autocomplete = target as SC_AutoComplete;
        baseInfo = (SC_Base)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("Base", new[] { "Assets/ScriptableObjects" })[0]), typeof(SC_Base));
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Champs Lexical");
        autocomplete.grammarCritere = EditorGUILayout.Popup(autocomplete.grammarCritere, baseInfo.listOfGrammarCritere);

        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
        EditorUtility.SetDirty(autocomplete);
        serializedObject.ApplyModifiedProperties();
    }
}
