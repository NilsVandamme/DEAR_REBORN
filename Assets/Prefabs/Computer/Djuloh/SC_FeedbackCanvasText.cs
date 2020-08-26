﻿using System.Collections;
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
    private string characterName="[CHARA]";
    public static int characterCurrentInt=0;
    public int characterMaxInt=3;
    public static bool Donna1 = false, Donna2 = false, Donna3 = false, MrS1 = false, MrS2 = false, MrS3 = false, Ash1 = false, Ash2 = false, Ash3 = false, Kat1 = false, Kat2 = false, Kat3 = false, Lyle1 = false, Lyle2 = false, Lyle3 = false , Kosong1 = false, Kosong2 = false, Kosong3 = false;
    
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

    public void setInfoBoolTrue(string charName)
    {
        Scene scene = SceneManager.GetActiveScene();
        if ((scene.name == "L_A1") && (Donna1 = false))
        {
            Donna1 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Donna2 = false))
        {
            Donna2 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Donna3 = false))
        {
            Donna3 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Kat1 = false))
        {
            Kat1 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Kat2 = false))
        {
            Kat2 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Kat3 = false))
        {
            Kat3 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Lyle1 = false))
        {
            Lyle1 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Lyle2 = false))
        {
            Lyle2 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Lyle3 = false))
        {
            Lyle3 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Ash1 = false))
        {
            Ash1 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Ash2 = false))
        {
            Ash2 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Ash3 = false))
        {
            Ash3 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (MrS1 = false))
        {
            MrS1 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (MrS2 = false))
        {
            MrS2 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (MrS3 = false))
        {
            MrS3 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Kosong1 = false))
        {
            Kosong1 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Kosong2 = false))
        {
            Kosong2 = true;
            characterMaxInt = 3;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Kosong3 = false))
        {
            Kosong3 = true;
            characterCurrentInt++;
            characterMaxInt = 3;
            return;
        }
        characterName = charName;
        updateText();

    }
   
    void Update()
    {
        
    }
}
