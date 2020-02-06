using TMPro;
using UnityEngine;

public class SC_GM_WheelToLetter : MonoBehaviour
{
    public static SC_GM_WheelToLetter instance;

    private SC_Word currentWord = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    /*
     * Gère le click d'un Button
     */
    public void OnClickButtonAutoComplete(TextMeshPro text)
    {
        foreach (SC_Word word in SC_GM_Local.gm.wheelOfWords)
            if (word.titre == text.text)
            {
                currentWord = word;
                return;
            }
                
    }

    public SC_Word getCurrentWord()
    {
        return currentWord;
    }
}
