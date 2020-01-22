using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Manage the speech of the boss window

public class SC_BossSpeech : MonoBehaviour
{
    public TMP_Text BossText; // Text where the boss make his speech

    [TextArea(5,5)]
    public List<string> Sentences; // All sentences said by the boss

    private int currentIndex; // Current sentence
    private bool sentenceFinished; // Has the sentence finished from printing ?

    private void Start()
    {
        // Init variables
        BossText.text = "";
        sentenceFinished = true;
    }

    // Display next sentence
    public void DisplayNextSentence()
    {
        if (sentenceFinished)
        {
            if (currentIndex < Sentences.Count)
            {
                //Debug.Log("sentence displayed");
                StartCoroutine("PlayText");
                currentIndex++;
            }
            else
            {
                // Play whatever
            }
        }
    }

    // Print the text with a typewritter effect
    IEnumerator PlayText()
    {
        sentenceFinished = false;
        foreach (char c in Sentences[currentIndex])
        {
            BossText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        BossText.text += "\n";
        sentenceFinished = true;
    }
}
