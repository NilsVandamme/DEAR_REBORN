using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manage the mails in the mail window

public class SC_MailSelection : MonoBehaviour
{
    public GameObject mailTextsListParent; // Parent of the texts content
    public GameObject mailButtonsListParent; // Parent of the buttons content
    public List<GameObject> mailTextsList; // List of texts content
    public int currentIndex = 0;

    void Start()
    {
        // Init the lists
        foreach (Transform child in mailTextsListParent.transform)
        {
                mailTextsList.Add(child.gameObject);
        }
    }

    // Open the specified mail and disable all others
    public void OpenMailText(int index)
    {
        mailButtonsListParent.SetActive(false);
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
        }

        mailTextsList[index].SetActive(true);
        currentIndex = index;
    }

    public void NextMail()
    {
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
        }

        if (currentIndex != mailTextsList.Count -1)
        {
            mailTextsList[currentIndex + 1].SetActive(true);
            currentIndex += 1;
        }
        else
        {
            mailTextsList[0].SetActive(true);
            currentIndex = 0;
        }
    }

    public void PreviousMail()
    {
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
        }

        if (currentIndex != 0)
        {
            mailTextsList[currentIndex - 1].SetActive(true);
            currentIndex -= 1;
        }
        else
        {
            mailTextsList[mailTextsList.Count-1].SetActive(true);
            currentIndex = mailTextsList.Count-1;
        }
    }

    public void ReturnToMailsList()
    {
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
        }
        mailButtonsListParent.SetActive(true);
    }
}
