using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassText1 : MonoBehaviour
{
    public string[] TextTuto;
    public Text ActualText;
    public int NumberText;
    public GameObject PrevButton;
    public GameObject NextButton;
    public GameObject Stampy;
    public Animator TextAnimator;
    public StampyManager sm;


    void Start()
    {
        Debug.Log(TextTuto.Length);
        NumberText = 1;
        PrevButton.SetActive(false);
    }

    public void NextText()
    {
        NumberText++;
        PrevButton.SetActive(true);
        if (NumberText <= TextTuto.Length)
        {
            ActualText.GetComponent<Text>().text = TextTuto[NumberText-1];
        }

        if (NumberText > TextTuto.Length)
        {
            PrevButton.SetActive(false);
            sm.StampyButton();
            NumberText = 1;
        }

    }

    public void PrevText()
    {
        NumberText--;
        NextButton.SetActive(true);
        if (NumberText >= 0)
        {
            ActualText.GetComponent<Text>().text = TextTuto[NumberText-1];
        }
        if (NumberText == 1)
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
        NumberText = 1;
        ActualText.GetComponent<Text>().text = TextTuto[NumberText-1];
    }

    public void Disapear()
    {
        gameObject.SetActive(false);
    }

    public void CallTextAnim()
    {
        TextAnimator.Play("");
    }

    public void OpenBackground()
    {

    }

    public void CloseBackground()
    {

    }
}
