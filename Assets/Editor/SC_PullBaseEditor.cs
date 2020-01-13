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
        serializedObject.Update();

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

                    int pos = 0;
                    for (int h = 0; h < i; h++)
                        pos += pullBase.listChampLexicaux.listChampLexical[h].listOfWords.Count;

                    for (int j = 0; j < listOfWordInActualCL.Count; j++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(listOfWordInActualCL[j].titre);
                        pullBase.motAccepterInCL[pos + j] = EditorGUILayout.Toggle(pullBase.motAccepterInCL[pos + j]);

                        GeneratePullBase(i, j, pos);

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel -= 1;
                }
            }
        }

        EditorUtility.SetDirty(pullBase);

        serializedObject.ApplyModifiedProperties();
    }

    private void Init()
    {
        if (pullBase.wordsInPull == null)
        {
            pullBase.wordsInPull = new List<SC_CLInPull>();

            pullBase.foldoutList = new bool[pullBase.listChampLexicaux.listChampLexical.Length];

            int lenght = 0;
            foreach (SC_ChampLexical elem in pullBase.listChampLexicaux.listChampLexical)
                lenght += elem.listOfWords.Count;

            pullBase.motAccepterInCL = new bool[lenght];
        }
    }

    private void GeneratePullBase(int i, int j, int pos)
    {
        string cl = pullBase.listChampLexicaux.listNameChampLexical[i];
        SC_Word word = pullBase.listChampLexicaux.listChampLexical[i].listOfWords[j];

        

        if (pullBase.motAccepterInCL[pos + j])
        {
            for (i = 0; i < pullBase.wordsInPull.Count; i++)
                if (pullBase.wordsInPull[i].GetCL() == cl)
                {
                    if (!pullBase.wordsInPull[i].GetListWord().Contains(word))
                    {
                        pullBase.wordsInPull[i].GetListWord().Add(word);
                    }

                    break;
                }

            if (i >= pullBase.wordsInPull.Count)
            {
                pullBase.wordsInPull.Add(new SC_CLInPull(cl, word));
            }
        }
        else
        {
            for (i = 0; i < pullBase.wordsInPull.Count; i++)
                if (pullBase.wordsInPull[i].GetCL() == cl)
                {
                    if (pullBase.wordsInPull[i].GetListWord().Contains(word))
                    {
                        pullBase.wordsInPull[i].GetListWord().Remove(word);
                    }

                    if (pullBase.wordsInPull[i].GetListWord().Count <= 0)
                    {
                        pullBase.wordsInPull.RemoveAt(i);
                    }
                }
        }  
    }
}
