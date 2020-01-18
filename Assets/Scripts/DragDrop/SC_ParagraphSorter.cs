using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ParagraphSorter : MonoBehaviour
{
    public static SC_ParagraphSorter instance;

    public List<GameObject> AllParagraphs;
    public List<GameObject> ParagraphsToSpawn;
    public List<GameObject> SpawnPositions;
    public List<GameObject> SnappedParagraphs;


    [Header("All paragraphs types")]

    public List<GameObject> OrientationParagraphs;
    public List<GameObject> WakeupParagraphs;
    public List<GameObject> MotivationParagraphs;
    public List<GameObject> ClashParagraphs;

    public string CurrentParagraphs;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GetAllParagraphs();

        // Spawn 3 random paragraphs
        ParagraphsToSpawn.Add(OrientationParagraphs[0]);
        ParagraphsToSpawn.Add(WakeupParagraphs[0]);
        ParagraphsToSpawn.Add(MotivationParagraphs[0]);
        SpawnParagraphs();
        //ParagraphsToSpawn.Clear();
    }

    public void GetOrientationParagraphs()
    {
        if (CurrentParagraphs == "Orientation")
            return;
        
        // Add paragrpahs to the list
        ClearSpawnList();
        ActivateSnappedParagraphs();
        ParagraphsToSpawn = new List<GameObject>(OrientationParagraphs);
        CurrentParagraphs = "Orientation";

        // Move paragraphs to the spawn positions
        SpawnParagraphs();
    }

    public void GetWakeUpParagraphs()
    {
        if (CurrentParagraphs == "WakeUp")
            return;

        // Add paragrpahs to the list
        ClearSpawnList();
        ActivateSnappedParagraphs();
        ParagraphsToSpawn = new List<GameObject>(WakeupParagraphs);
        CurrentParagraphs = "WakeUp";

        // Move paragraphs to the spawn positions
        SpawnParagraphs();
    }

    public void GetMotivationalParagraphs()
    {
        if (CurrentParagraphs == "Motivation")
            return;

        // Add paragrpahs to the list
        ClearSpawnList();
        ActivateSnappedParagraphs();
        ParagraphsToSpawn = new List<GameObject>(MotivationParagraphs);
        CurrentParagraphs = "Motivation";

        // Move paragraphs to the spawn positions
        SpawnParagraphs();
    }

    public void GetClashParagraphs()
    {
        if (CurrentParagraphs == "Clash")
            return;

        // Add paragrpahs to the list
        ClearSpawnList();
        ActivateSnappedParagraphs();
        ParagraphsToSpawn = new List<GameObject>(ClashParagraphs);
        CurrentParagraphs = "Clash";

        // Move paragraphs to the spawn positions
        SpawnParagraphs();
    }


    // SYSTEM

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paragraph")
        {
            SnappedParagraphs.Add(other.gameObject);
            ParagraphsToSpawn.Remove(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Paragraph")
        {
            SnappedParagraphs.Remove(other.gameObject);
            ParagraphsToSpawn.Add(other.gameObject);
        }
    }

    public void ClearSpawnList()
    {
        for (int i = 0; i < ParagraphsToSpawn.Count; i++)
        {
            ParagraphsToSpawn[i].gameObject.SetActive(false);
        }
    }

    public void SpawnParagraphs()
    {
        for (int i = 0; i < ParagraphsToSpawn.Count; i++)
        {
            if (!SnappedParagraphs.Contains(ParagraphsToSpawn[i].gameObject))
            {
                ParagraphsToSpawn[i].transform.position = SpawnPositions[i].transform.position;
                ParagraphsToSpawn[i].GetComponent<SC_DragDropControls>().GetOriginalSnapPosition();
                ParagraphsToSpawn[i].gameObject.SetActive(true);
            }
        }
    }

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
                MotivationParagraphs.Add(AllParagraphs[i]);
            }
            else if (AllParagraphs[i].GetComponent<SC_ParagraphType>().Type == SC_ParagraphType.ParagraphType.Clash)
            {
                ClashParagraphs.Add(AllParagraphs[i]);
            }
        }
    }

    public void ActivateSnappedParagraphs()
    {
        for(int i =0; i<SnappedParagraphs.Count; i++)
        {
            SnappedParagraphs[i].gameObject.SetActive(true);
        }
    }

    public void DisableSnappedParagraphs()
    {
        for (int i = 0; i < SnappedParagraphs.Count; i++)
        {
            SnappedParagraphs[i].gameObject.SetActive(false);
        }
    }
}
