using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Word))]
public class SC_WordDrawerEditor : PropertyDrawer
{
    float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

    /*
     * Définie la taille alouer a chache objet de l'inspecteur
     */
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOfLines = 2;

        SerializedProperty score = property.FindPropertyRelative("scorePerso");
        SerializedProperty critere = property.FindPropertyRelative("grammarCritere");

        if (critere.isExpanded && score.isExpanded)
            numberOfLines += score.arraySize + critere.arraySize;
        else if (critere.isExpanded)
            numberOfLines += critere.arraySize;
        else if (score.isExpanded)
            numberOfLines += score.arraySize;

        return lineHeight * numberOfLines;
    }

    /*
     * Display la structure des words créer
     */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int i = 0, j;

        float space = 10f;
        float rectWight = (position.width - space) * 0.6f;


        SerializedProperty grammarCritere = property.FindPropertyRelative("grammarCritere");
        grammarCritere.isExpanded = 
            EditorGUI.Foldout(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
            grammarCritere.isExpanded, "List of criteres", true);

        if (grammarCritere.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            for (j = 0; j < grammarCritere.arraySize; j++)
            {
                EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight * i + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    new GUIContent(SC_GM_Master.gm.listChampsLexicaux.listOfGrammarCritere[j]));

                EditorGUI.LabelField(new Rect(position.x + rectWight, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    grammarCritere.GetArrayElementAtIndex(j).stringValue);
            }

            EditorGUI.indentLevel -= 1;
        }


        SerializedProperty scorePerso = property.FindPropertyRelative("scorePerso");
        scorePerso.isExpanded = 
            EditorGUI.Foldout(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
            scorePerso.isExpanded, "List of scores", true);

        if (scorePerso.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            for (j = 0; j < scorePerso.arraySize; j++)
            {
                EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight * i + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    new GUIContent(SC_GM_Master.gm.listChampsLexicaux.listOfPerso[j]));

                EditorGUI.LabelField(new Rect(position.x + rectWight, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    (scorePerso.GetArrayElementAtIndex(j).intValue).ToString());
            }

            EditorGUI.indentLevel -= 1;
        }
    }
}
