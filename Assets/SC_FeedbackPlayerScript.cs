using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_FeedbackPlayerScript : MonoBehaviour
{
    private SC_FeedbackCanvasText fb;
    public GameObject bulleThemeWord1, bulleThemeWord2, bulleThemeWord3;
    public int bulleInt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText()
    {
        fb.updateText();
    }

    public void SpawnBulle()
    {
        //TO DO : Open Word Basket

        if (bulleInt == 1)
        {
            bulleThemeWord1.SetActive(true);
        }
      

        if (bulleInt == 2)
        {
            bulleThemeWord2.SetActive(true);
        }

        if (bulleInt == 3)
        {
            bulleThemeWord3.SetActive(true);
        }

    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
