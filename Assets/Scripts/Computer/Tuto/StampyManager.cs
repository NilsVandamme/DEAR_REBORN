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
        if (Mails.SetActive == true)
        {
            Stampy.GetComponent<StampyNext>().Bulles[0] = BulleMails;
            Stampy.SetActive = true;
            return;
        }
        if (Daily.SetActive == true)
        {
            Stampy.GetComponent<StampyNext>().Bulles[0] = BulleDaily;
            Stampy.SetActive = true;
            return;
        }
        if (Arbo.SetActive == true)
        {
            Stampy.GetComponent<StampyNext>().Bulles[0] = BulleArbo;
            Stampy.SetActive = true;
            return;
        }
    }
}
