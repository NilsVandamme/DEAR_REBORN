using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_BossSpeech : MonoBehaviour
{
    public TMP_Text BossText;

    [TextArea(5,5)]
    public List<string> Sentences;

    private int currentIndex;
    private bool sentenceFinished;

    private void Start()
    {
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
