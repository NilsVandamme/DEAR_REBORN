using System.Collections.Generic;
using UnityEngine;

// Manage the mails in the mail window

public class SC_MailSelection : MonoBehaviour
{
    public GameObject mailTextsListParent; // Parent of the texts content
    public GameObject mailTextsParent; // Parent of the texts and background
    public GameObject mailButtonsListParent; // Parent of the buttons content
    public GameObject returnButton; // Return to list button
    public List<GameObject> mailTextsList; // List of texts content
    public List<GameObject> mailButtonsHighlights; // List of all the hightligth images
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
        //mailButtonsListParent.SetActive(false);
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
            mailButtonsHighlights[i].SetActive(false);
        }

        mailTextsList[index].SetActive(true);
        mailButtonsHighlights[index].SetActive(true);
        returnButton.SetActive(true);
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
            mailButtonsHighlights[i].SetActive(false);
        }
        mailTextsParent.SetActive(false);
        //mailButtonsListParent.SetActive(true);
        returnButton.SetActive(false);
    }
}
