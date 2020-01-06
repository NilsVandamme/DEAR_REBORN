using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ParagraphSorter : MonoBehaviour
{
    public List<GameObject> AllParagraphs;
    public List<GameObject> ParagraphsToSpawn;
    public List<GameObject> SpawnPositions;

    [Header("All paragraphs types")]

    public List<GameObject> OrientationParagraphs;
    public List<GameObject> WakeupParagraphs;
    public List<GameObject> MotivationalParagraphs;
    public List<GameObject> ClashParagraphs;

    // Start is called before the first frame update
    void Start()
    {
        GetAllParagraphs();
    }

    public void GetOrientationParagraphs()
    {
        // Add paragrpahs to the list
        ParagraphsToSpawn = OrientationParagraphs;



        // Move paragraphs to the spawn positions
        for(int i=0; i < ParagraphsToSpawn.Count; i++)
        {
            ParagraphsToSpawn[i].transform.position = SpawnPositions[i].transform.position;
            ParagraphsToSpawn[i].GetComponent<SC_DragDropControls>().GetOriginalSnapPosition();
        }
    }

    public void GetWakeUpParagraphs()
    {

    }

    public void GetMotivationalParagraphs()
    {

    }

    public void GetClashParagraphs()
    {

    }


    // SYSTEM

    public void GetAllParagraphs()
    {
        AllParagraphs = new List<GameObject>();
        AllParagraphs.AddRange(GameObject.FindGameObjectsWithTag("Paragraph"));

        for (int i = 0; i < AllParagraphs.Count; i++)
        {
            if (AllParagraphs[i].GetComponent<SC_ParagraphType>().Type == SC_ParagraphType.ParagraphType.Orientation)
            {
                OrientationParagraphs.Add(AllParagraphs[i]);
            }
            else if (AllParagraphs[i].GetComponent<SC_ParagraphType>().Type == SC_ParagraphType.ParagraphType.WakeUp)
            {
                WakeupParagraphs.Add(AllParagraphs[i]);
            }
            else if (AllParagraphs[i].GetComponent<SC_ParagraphType>().Type == SC_ParagraphType.ParagraphType.Motivation)
            {
                MotivationalParagraphs.Add(AllParagraphs[i]);
            }
            else if (AllParagraphs[i].GetComponent<SC_ParagraphType>().Type == SC_ParagraphType.ParagraphType.Clash)
            {
                ClashParagraphs.Add(AllParagraphs[i]);
            }
        }
    }
}
