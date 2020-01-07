using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_PullBase))]
public class SC_PullBaseEditor : Editor
{
    private SC_PullBase pullBase;

    private void OnEnable()
    {
        pullBase = target as SC_PullBase;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        pullBase.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", pullBase.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (pullBase.listChampLexicaux != null)
        {
            Init();

            for (int i = 0; i < pullBase.foldoutList.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();

                pullBase.foldoutList[i] = EditorGUILayout.Foldout(pullBase.foldoutList[i], pullBase.listChampLexicaux.listNameChampLexical[i], true);

                EditorGUILayout.EndHorizontal();

                if (pullBase.foldoutList[i])
                {
                    List<SC_Word> listOfWordInActualCL = pullBase.listChampLexicaux.listChampLexical[i].listOfWords;
                    EditorGUI.indentLevel += 1;

                    for (int j = 0; j < listOfWordInActualCL.Count; j++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(listOfWordInActualCL[j].titre);
                        pullBase.motAccepterInCL[i][j] = EditorGUILayout.Toggle(pullBase.motAccepterInCL[i][j]);

                        GeneratePullBase(i, j);

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel -= 1;
                }
            }
        }

        EditorUtility.SetDirty(pullBase);
    }

    private void Init()
    {
        if (pullBase.wordsInPull == null)
        {
            pullBase.wordsInPull = new List<SC_CLInPull>();

            pullBase.foldoutList = new bool[pullBase.listChampLexicaux.listChampLexical.Length];

            pullBase.motAccepterInCL = new bool[pullBase.foldoutList.Length][];
            for (int i = 0; i < pullBase.foldoutList.Length; i++)
                pullBase.motAccepterInCL[i] = new bool[pullBase.listChampLexicaux.listChampLexical[i].listOfWords.Count];
        }
    }

    private void GeneratePullBase(int i, int j)
    {
        string cl = pullBase.listChampLexicaux.listNameChampLexical[i];
        SC_Word word = pullBase.listChampLexicaux.listChampLexical[i].listOfWords[j];

        if (pullBase.motAccepterInCL[i][j])
        {
            for (i = 0; i < pullBase.wordsInPull.Count; i++)
                if (pullBase.wordsInPull[i].GetCL() == cl)
                {
                    if (!pullBase.wordsInPull[i].GetListWord().Contains(word))
                        pullBase.wordsInPull[i].GetListWord().Add(word);

                    break;
                }

            if (i >= pullBase.wordsInPull.Count)
                pullBase.wordsInPull.Add(new SC_CLInPull(cl, word));
        }
        else
        {
            for (i = 0; i < pullBase.wordsInPull.Count; i++)
                if (pullBase.wordsInPull[i].GetCL() == cl)
                {
                    if (pullBase.wordsInPull[i].GetListWord().Contains(word))
                        pullBase.wordsInPull[i].GetListWord().Remove(word);

                    if (pullBase.wordsInPull[i].GetListWord().Count <= 0)
                        pullBase.wordsInPull.RemoveAt(i);
                }
        }

            
    }
}
