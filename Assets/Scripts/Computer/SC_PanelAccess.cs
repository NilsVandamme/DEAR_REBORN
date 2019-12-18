using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PanelAccess : MonoBehaviour
{
    public GameObject WindowClientsLetter;
    public SC_WindowTopBar ClientsLetterWtb;
    public GameObject WindowClientsInfos;
    public SC_WindowTopBar ClientsInfosWtb;
    public GameObject WindowEmails;
    public SC_WindowTopBar EmailsWtb;

    public void OpenWindowLetter()
    {
        if (WindowClientsLetter.activeSelf == false)
        {
            WindowClientsLetter.SetActive(true);
            WindowClientsLetter.transform.SetAsLastSibling();


        }
        else
        {
            WindowClientsLetter.transform.SetAsLastSibling();

            if (ClientsLetterWtb.IsOpen == false)
            {
                ClientsLetterWtb.MaximizeWindow();
            }
        }
    }

    public void OpenWindowInfos()
    {
        if (WindowClientsInfos.activeSelf == false)
        {
            WindowClientsInfos.SetActive(true);
            WindowClientsInfos.transform.SetAsLastSibling();


        }
        else
        {
            WindowClientsInfos.transform.SetAsLastSibling();

            if (ClientsInfosWtb.IsOpen == false)
            {
                ClientsInfosWtb.MaximizeWindow();
            }
        }
    }

    public void OpenWindowEmails()
    {
        if (WindowEmails.activeSelf == false)
        {
            WindowEmails.SetActive(true);
            WindowEmails.transform.SetAsLastSibling();


        }
        else
        {
            WindowEmails.transform.SetAsLastSibling();

            if (EmailsWtb.IsOpen == false)
            {
                ClientsInfosWtb.MaximizeWindow();
            }
        }
    }
}
