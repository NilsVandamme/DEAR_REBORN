using TMPro;
using UnityEngine;

public class SC_GM_WheelToLetter : MonoBehaviour
{
    public static SC_GM_WheelToLetter instance;

    private SC_Word currentWord;

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
    public void OnClickButtonAutoComplete(TextMeshProUGUI text)
    {
        foreach (SC_Word word in SC_GM_Local.gm.wheelOfWords)
            if (word.titre == text.text)
            {
                currentWord = word;
                return;
            }
                
    }
}
