using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassText : MonoBehaviour
{
    public string[] TextTuto;
    public Text ActualText;
    private int NumberText;
    
    
    void Start()
    {
        NumberText = 0;
        
    }

    public void NextText()
    {
        if (TextTuto[NumberText] != null)
        {
            ActualText.GetComponent<Text>().text = TextTuto[NumberText];
            NumberText++;
        }
        else
        {

        }
        
    }
}
