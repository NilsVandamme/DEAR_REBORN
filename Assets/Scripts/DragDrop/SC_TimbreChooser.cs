using System.Collections.Generic;
using UnityEngine;

// Manage the choice of the stamp on the envelope 

public class SC_TimbreChooser : MonoBehaviour
{
    public static SC_TimbreChooser instance; // singleton instance

    public List<GameObject> stamps; // all stamps
    public GameObject selectedStamp; // The stamp the player selected
    public GameObject StoryArbo; // Story treeview screen
    public bool StampAlreadySelected; // Has a stamp been selected ?
    [HideInInspector]
    public Animator anim; // Animator

    void Start()
    {
        // Singleton
        instance = this;
        anim = GetComponent<Animator>();

        // Add all stamps to the list
        for(int i=0;i< transform.GetChild(2).childCount; i++)
        {
            stamps.Add(transform.GetChild(2).GetChild(i).gameObject);
        }
    }

    // Get the stamp the player choosed
    public void ChooseStamp()
    {
        if (!StampAlreadySelected)
        {
            StampAlreadySelected = true;

            for(int i=0; i<stamps.Count; i++)
            {
                stamps[i].SetActive(false);
            }
            selectedStamp.SetActive(true);
        }
    }

    public void OpenStoryArbo()
    {
        StoryArbo.SetActive(true);
    }
}
