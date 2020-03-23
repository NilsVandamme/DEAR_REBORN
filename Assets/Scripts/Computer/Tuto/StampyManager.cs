using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampyManager : MonoBehaviour
{
    private bool FlipFlop;
    public GameObject Mails;
    public GameObject Daily;
    public GameObject Arbo;
    public GameObject BulleMails;
    public GameObject BulleDaily;
    public GameObject BulleArbo;
    public GameObject Stampy;

    public void StampyButton()
    {
        if (!FlipFlop)
        {
            FlipFlop = true;
            SelectBulle();
            return;
        }
        else
        {
            FlipFlop = false;
            Stampy.GetComponent<StampyNext>().LeftForNow();
            return;
        }
    }

    public void SelectBulle()
    {
        if (Mails.active)
        {
            Stampy.GetComponent<StampyNext>().Bulles[0] = BulleMails;
            Stampy.SetActive(true);
            Stampy.GetComponent<Animator>().Play("0");
            return;
        }
        if (Daily.active)
        {
            Stampy.GetComponent<StampyNext>().Bulles[0] = BulleDaily;
            Stampy.SetActive(true);
            Stampy.GetComponent<Animator>().Play("0");
            return;
        }
        if (Arbo.active)
        {
            Stampy.GetComponent<StampyNext>().Bulles[0] = BulleArbo;
            Stampy.SetActive(true);
            Stampy.GetComponent<Animator>().Play("0");
            return;
        }
    }
}
