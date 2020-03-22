using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampyNext : MonoBehaviour
{
    public GameObject[] Bulles;
    private int NumberBulles;
    public Animator StampyAnimator;
    public GameObject Object;
    public bool NewState;

    void Start()
    {
        NumberBulles = 0;
    }

    public void PlayAnim()
    {
        StampyAnimator.Play(NumberBulles.ToString());
    }

    public void NextPos()
    {
        Bulles[NumberBulles-1].GetComponent<Animator>().Play("DisparitionBulle");
        Bulles[NumberBulles].SetActive(true);
        NumberBulles++;
    }

    public void Initialized()
    {
        Bulles[NumberBulles].SetActive(true);
        NumberBulles++;
    }

    public void LeftForNow()
    {
        StampyAnimator.Play("disapear");
        Bulles[NumberBulles - 1].GetComponent<Animator>().Play("DisparitionBulle");
    }

    public void ReApper()
    {
        StampyAnimator.Play("Appear");
    }

    public void NewObject()
    {
        Object.SetActive(NewState);
    }
}
