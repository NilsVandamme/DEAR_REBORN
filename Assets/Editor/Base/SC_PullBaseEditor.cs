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

            if (GUILayout.Button("Load Base Pull"))
                GeneratePullBase();

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
        if (pullBase.wordsInBasePull == null)
        {
            pullBase.wordsInBasePull = new List<SC_CLInPull>();

            pullBase.foldoutList = new bool[pullBase.listChampLexicaux.listChampLexical.Length];

            int lenght = 0;
            foreach (SC_ChampLexical elem in pullBase.listChampLexicaux.listChampLexical)
                lenght += elem.listOfWords.Count;

            pullBase.motAccepterInCL = new bool[lenght];
        }
    }

    private void GeneratePullBase()
    {
        string cl;
        SC_Word word;
        int h, pos = -1;

        pullBase.wordsInBasePull = new List<SC_CLInPull>();

        for (int i = 0; i < pullBase.listChampLexicaux.listChampLexical.Length; i++)
            for (int j = 0; j < pullBase.listChampLexicaux.listChampLexical[i].listOfWords.Count; j++)
            {
                cl = pullBase.listChampLexicaux.listNameChampLexical[i];
                word = pullBase.listChampLexicaux.listChampLexical[i].listOfWords[j];
                pos++;

                if (pullBase.motAccepterInCL[pos]) // Si le mot à ete coche accepter
                {
                    for (h = 0; h < pullBase.wordsInBasePull.Count; h++)
                        if (pullBase.wordsInBasePull[h].GetCL() == cl) // Si le CL existe deja
                        {
                            if (!pullBase.wordsInBasePull[h].GetListWord().Contains(word)) // Si le mot n'est pas encore dans le CL
                            {
                                pullBase.wordsInBasePull[h].GetListWord().Add(word); // On l'ajoute
                            }

                            break; // Sinon on fait rien
                        }

                    if (h >= pullBase.wordsInBasePull.Count) // Si le CL n'a pas été trouvé : il n'existe pas
                    {
                        pullBase.wordsInBasePull.Add(new SC_CLInPull(cl, word)); // On ajoute le CL et son mot
                    }
                }
                else // Si le mot a ete cocher non accepter
                {
                    for (h = 0; h < pullBase.wordsInBasePull.Count; h++)
                        if (pullBase.wordsInBasePull[h].GetCL() == cl) // Si le CL existe deja
                        {
                            if (pullBase.wordsInBasePull[h].GetListWord().Contains(word)) // Si le mot est dans le CL
                            {
                                pullBase.wordsInBasePull[h].GetListWord().Remove(word); // On l'enleve
                            }

                            if (pullBase.wordsInBasePull[h].GetListWord().Count <= 0) // Si le CL n'a plus de mot
                            {
                                pullBase.wordsInBasePull.RemoveAt(h); // On supprime le CL
                            }
                        }
                }
            }
    }
}
