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
    public bool WaitForPlayer;
    public Animator TextAnimator;
    public bool ActiveObject;
    public bool NewValue;
    public GameObject ObjectsToActivate;
    public GameObject Background;


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
        if (TextTuto[NumberText] == "")
        {
            if (ActiveObject)
            {
                UnlockObject();
            }

            if (WaitForPlayer)
            {
                Stampy.GetComponent<StampyNext>().LeftForNow();
            }
            else
            {
                Stampy.GetComponent<StampyNext>().PlayAnim();
            }
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

    public void CallTextAnim()
    {
        TextAnimator.Play("");
    }

    public void UnlockObject()
    {
        Debug.Log("oui");
        ObjectsToActivate.GetComponent<Button>().interactable = NewValue;
    }

    public void OpenBackground()
    {
        if (Background != null)
        {
            Background.SetActive(true);
        }
        
    }

    public void CloseBackground()
    {
        if (Background != null)
        {
            Background.SetActive(false);
        }
    }

}
