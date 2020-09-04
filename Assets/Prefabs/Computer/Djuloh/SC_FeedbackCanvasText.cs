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
    private string characterName="[CHARA]";
    public static int characterCurrentInt=0;
    public int characterMaxInt=3;
    private float randomfloat = 0f;
    public static bool Cyrus1 = false, Cyrus2 = false, Cyrus3 = false, Donna1 = false, Donna2 = false, Donna3 = false, MrS1 = false, MrS2 = false, MrS3 = false, Ash1 = false, Ash2 = false, Ash3 = false, Kat1 = false, Kat2 = false, Kat3 = false, Lyle1 = false, Lyle2 = false, Lyle3 = false , Kosong1 = false, Kosong2 = false, Kosong3 = false;
    
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
        randomfloat = Random.Range(0, 10);
        if (randomfloat <= 5)
        {
            WordGetObject.GetComponent<Animator>().Play("WordGetAppear");
        }
        else
        {
            WordGetObject.GetComponent<Animator>().Play("WordGetAppear2");
        }
       
    }

    public void FeedbackInfo()
    {
        InfoGetObject.SetActive(true);
        WordGetObject.GetComponent<Animator>().Play("WordGet");
    }

    public void setInfoBoolTrue(string charName)
    {
        Scene scene = SceneManager.GetActiveScene();
        if ((scene.name == "L_A1") && (Donna1 = false))
        {
            //ok
            Donna1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Donna2 = false))
        {
            Donna2 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Donna3 = false))
        {
            Donna3 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Kat1 = false))
        {
            Kat1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Kat2 = false))
        {
            Kat2 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Kat3 = false))
        {
            Kat3 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_D1") && (Lyle1 = false))
        {
            //ok
            Lyle1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Lyle2 = false))
        {
            Lyle2 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Lyle3 = false))
        {
            Lyle3 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Ash1 = false))
        {
            Ash1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Ash2 = false))
        {
            Ash2 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Ash3 = false))
        {
            Ash3 = true;
            characterCurrentInt++;
            return;
        }
        if (((scene.name == "L_B1")||(scene.name=="L_B2")) && (MrS1 = false))
        {
            //ok
            MrS1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (MrS2 = false))
        {
            MrS2 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (MrS3 = false))
        {
            MrS3 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_F2") && (Kosong1 = false))
        {
            //ok
            Kosong1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_F3") && (Kosong2 = false))
        {
            //ok
            Kosong2 = true;
            characterMaxInt = 3;
            return;
        }
        if ((scene.name == "L_A1") && (Kosong3 = false))
        {
            Kosong3 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_F4") && (Cyrus1 = false))
        {
            //ok
            Cyrus1 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Cyrus2 = false))
        {
            Cyrus2 = true;
            characterCurrentInt++;
            return;
        }
        if ((scene.name == "L_A1") && (Cyrus3 = false))
        {
            Cyrus3= true;
            characterCurrentInt++;
            return;
        }
        characterName = charName;
        updateText();

    }
   
    void Update()
    {
        
    }
}
