using System.Collections.Generic;
using UnityEngine;

public class SC_TimbreChooser : MonoBehaviour
{
    public static SC_TimbreChooser instance;

    public List<GameObject> stamps;
    public GameObject selectedStamp;
    public bool StampAlreadySelected;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //anim = GetComponent<Animator>();

        // Add all stamps to the list
        for(int i=0;i< transform.GetChild(2).childCount; i++)
        {
            stamps.Add(transform.GetChild(2).GetChild(i).gameObject);
        }

    }

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

            // Play anim
            //anim.SetTrigger();

            Debug.Log(selectedStamp.name + " has been snapped to the letter");
        }
    }
}
