using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
using UnityEngine.SceneManagement;

public class SC_FeedbackCanvasText : MonoBehaviour
{

    public TMP_Text ThemeWordText, InfoText;
    public GameObject WordGetObject, InfoGetObject;
    public string themeWordName,  characterCurrentNumber, characterMaxNumber;
    private string characterName;
    public static int characterCurrentInt=0;
    public int characterMaxInt;
    public static bool Kat1 =false, Kat2 =false, Kat3=false;
    // Start is called before the first frame update
    void Start()
    {
        
        ThemeWordText.text = themeWordName;

        characterCurrentNumber = characterCurrentInt.ToString();
        characterMaxNumber = characterMaxInt.ToString();
        InfoText.text = characterName+"'s file progress : "+characterCurrentNumber+"/"+characterMaxNumber;

    }

    public void updateText()
    {
        ThemeWordText.text = themeWordName;

        characterCurrentNumber = characterCurrentInt.ToString();
        characterMaxNumber = characterMaxInt.ToString();
        InfoText.text = characterName + "'s file progress : " + characterCurrentNumber + "/" + characterMaxNumber;
    }

    public void updateThemeWord(string themeWord)
    {
        themeWordName = themeWord;
    }

    public void FeedbackCL()
    {
        WordGetObject.SetActive(true);
    }

    public void FeedbackInfo()
    {
        InfoGetObject.SetActive(true);
    }

    public void setInfoBoolTrue()
    {
        Scene scene = SceneManager.GetActiveScene();
        if ((scene.name == "L_A1") && (Kat1 = false))
        {
            Kat1 = true;
            characterCurrentInt++;
            characterName = "prout";
        }

        
        updateText();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
