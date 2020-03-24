using System.Collections.Generic;
using UnityEngine;

// Manage the stock of all paragraphs and their display

public class SC_ParagraphSorter : MonoBehaviour
{
    public static SC_ParagraphSorter instance;

    public List<GameObject> AllParagraphs;
    public List<GameObject>[] Paragraphs;
    public List<GameObject> SpawnPositions;
    public List<GameObject> SnappedParagraphs;

    [HideInInspector]
    public int lastParagrapheMove;
    private int[] indexParagrapheAffiche;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        indexParagrapheAffiche = new int[4];
        Paragraphs = new List<GameObject>[4];

        for (int i = 0; i < Paragraphs.Length; i++)
            Paragraphs[i] = new List<GameObject>();

        GetAllParagraphs();
    }

    public void GetAllParagraphs()
    {
        AllParagraphs = new List<GameObject>();
        AllParagraphs.AddRange(GameObject.FindGameObjectsWithTag("Paragraph"));

        for (int i = 0; i < AllParagraphs.Count; i++)
            Paragraphs[(int)AllParagraphs[i].GetComponent<SC_ParagraphType>().Type].Add(AllParagraphs[i]);

        for (int i = 0; i < Paragraphs.Length; i++)
            for (int j = 0; j < Paragraphs[i].Count; j++)
            {
                Paragraphs[i][j].transform.position = SpawnPositions[i].transform.position;
                Paragraphs[i][j].GetComponent<SC_DragDropControls>().GetOriginalSnapPosition();

                if (j == indexParagrapheAffiche[i])
                    Paragraphs[i][j].gameObject.SetActive(true);
                else
                    Paragraphs[i][j].gameObject.SetActive(false);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paragraph")
        {
            SnappedParagraphs.Add(other.gameObject);

            for (int i = 0; i < Paragraphs.Length; i++)
            {
                if (Paragraphs[i].Contains(other.gameObject))
                {
                    Paragraphs[i].Remove(other.gameObject);
                    lastParagrapheMove = i;
                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Paragraph")
        {
            SnappedParagraphs.Remove(other.gameObject);

            int temp = (int)other.gameObject.GetComponent<SC_ParagraphType>().Type;

            Paragraphs[temp].Add(other.gameObject);
        }
    }

    public void Moins(int index)
    {
        if (!Check(index))
            return;

        indexParagrapheAffiche[index]--;

        if (indexParagrapheAffiche[index] < 0)
            indexParagrapheAffiche[index] = Paragraphs[index].Count - 1;

        Affiche(index);
    }

    public void Plus(int index)
    {
        if (!Check(index))
            return;

        indexParagrapheAffiche[index]++;

        if (indexParagrapheAffiche[index] >= Paragraphs[index].Count)
            indexParagrapheAffiche[index] = 0;

        Affiche(index);
    }

    public void Affiche(int index)
    {
        if (!Check(index))
            return;

        if (indexParagrapheAffiche[index] >= Paragraphs[index].Count)
            indexParagrapheAffiche[index] = 0;

        for (int i = 0; i < Paragraphs[index].Count; i++)
            if (i != indexParagrapheAffiche[index])
                Paragraphs[index][i].SetActive(false);

        Paragraphs[index][indexParagrapheAffiche[index]].gameObject.SetActive(true);

        Paragraphs[index][indexParagrapheAffiche[index]].transform.position = SpawnPositions[index].transform.position;
        Paragraphs[index][indexParagrapheAffiche[index]].GetComponent<SC_DragDropControls>().GetOriginalSnapPosition();
    }

    private bool Check(int index)
    {
        if (Paragraphs[index].Count <= 0)
        {
            Debug.LogError("Plus de paragraphes");
            return false;
        }

        return true;
    }
}
