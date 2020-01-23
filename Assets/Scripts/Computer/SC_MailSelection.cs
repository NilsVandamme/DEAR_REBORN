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
    public List<Image> mailButtonsList; // List of buttons content

    void Start()
    {
        // Init the lists
        foreach (Transform child in mailTextsListParent.transform)
        {
                mailTextsList.Add(child.gameObject);
        }

        foreach (Transform child in mailButtonsListParent.transform)
        {
                mailButtonsList.Add(child.gameObject.GetComponent<Image>());
        }
    }

    // Open the specified mail and disable all others
    public void OpenMailText(int index)
    {
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
        }

        mailTextsList[index].SetActive(true);

        for (int j = 0; j < mailButtonsList.Count; j++)
        {
            mailButtonsList[j].color = Color.white;
        }
    }
}
