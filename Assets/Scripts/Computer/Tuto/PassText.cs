using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassText : MonoBehaviour
{
    public string[] TextTuto;
    public Text ActualText;
    private int NumberText;
    public GameObject PrevButton;
    public GameObject NextButton;
    public GameObject Stampy;
    
    
    void Start()
    {
        NumberText = 0;
        PrevButton.SetActive(false);
    }

    public void NextText()
    {
        NumberText++;
        PrevButton.SetActive(true);
        if (TextTuto[NumberText] != "")
        {
            ActualText.GetComponent<Text>().text = TextTuto[NumberText];
        }
        else
        {
            Stampy.GetComponent<StampyNext>().PlayAnim();
        }
        
    }

    public void PrevText()
    {
        NumberText--;
        if (NumberText >= 0)
        {
            ActualText.GetComponent<Text>().text = TextTuto[NumberText];
        }
        if (NumberText == 0)
        {
            PrevButton.SetActive(false);
        }
        if (NumberText <= 0)
        {
            PrevButton.SetActive(false);
            NumberText = 0;
        }

    }

    public void GenerateText()
    {
        ActualText.GetComponent<Text>().text = TextTuto[NumberText];
    }

    public void Disapear()
    {
        gameObject.SetActive(false);
    }
}
