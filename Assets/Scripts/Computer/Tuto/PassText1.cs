using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassText1 : MonoBehaviour
{
    public string[] TextTuto;
    public Text ActualText;
    private int NumberText;
    public GameObject PrevButton;
    public GameObject NextButton;
    public GameObject Stampy;
    public Animator TextAnimator;


    void Start()
    {
        Debug.Log(TextTuto.Length);
        NumberText = 0;
        PrevButton.SetActive(false);
    }

    public void NextText()
    {
        NumberText++;
        PrevButton.SetActive(true);
        if (NumberText <= TextTuto.Length-1)
        {
            ActualText.GetComponent<Text>().text = TextTuto[NumberText];
        }

        if (NumberText == TextTuto.Length-1)
        {
            NextButton.SetActive(false);
        }

        if (NumberText >= TextTuto.Length-1)
        {
            NextButton.SetActive(false);
            NumberText = TextTuto.Length-1;
        }

    }

    public void PrevText()
    {
        NumberText--;
        NextButton.SetActive(true);
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

    public void OpenBackground()
    {

    }

    public void CloseBackground()
    {

    }
}
