using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SC_CameraDayDisplay : MonoBehaviour
{


    public TMP_Text DayText, DayNameText, IncidentText;
    private SC_GM_SoundManager GM_Audio;
    private Animator anim;
    private int AnimInt;
    public GameObject stampy;

    void Start()
    {
        GM_Audio = transform.GetChild(1).GetComponent<SC_GM_SoundManager>();
        anim = gameObject.GetComponent<Animator>();
        AnimInt = 0;
        anim.SetBool("tuto", false);
        stampy.SetActive(false);
    }
    public void appearStampy(){

        stampy.SetActive(true);
        }
    public void AnimIntIncr()
    {
        AnimInt++;
        anim.SetInteger("AnimInt", AnimInt);
    }

    public void tutoTrue()
    {
        anim.SetBool("tuto", true);
    }


    public void AnimIntReset()
    {
        anim.SetInteger("AnimInt", 0);
    }
    public void DisplayDayText()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "L_A1")
        {
            DayText.text = "April 8th";
            DayNameText.text = "First day at work";
            IncidentText.text = "37 days before the Incident";
            GM_Audio.PlayPiano(0);
           
        }

        if (scene.name == "L_B1")
        {
            DayText.text = "April 9th";
            DayNameText.text = "The Speech";
            IncidentText.text = "36 days before the Incident";
            GM_Audio.PlayPiano(1);
        }

        if (scene.name == "L_B2")
        {
            DayText.text = "April 9th";
            DayNameText.text = "The Speech";
            IncidentText.text = "36 days before the Incident";
            GM_Audio.PlayPiano(2);
        }

        if (scene.name == "L_C1")
        {
            DayText.text = "April 11th";
            DayNameText.text = "Meeting Katherine";
            IncidentText.text = "34 days before the Incident";
            GM_Audio.PlayPiano(3);
        }

        if (scene.name == "L_C2")
        {
            DayText.text = "April 13th";
            DayNameText.text = "Being for others";
            IncidentText.text = "32 days before the Incident";
            GM_Audio.PlayPiano(4);
        }

        if (scene.name == "L_D1")
        {
            DayText.text = "April 18th";
            DayNameText.text = "Disoriented";
            IncidentText.text = "27 days before the Incident";
            GM_Audio.PlayPiano(5);
        }


        if (scene.name == "L_D2")
        {
            DayText.text = "April 19th";
            DayNameText.text = "Muffled Voice";
            IncidentText.text = "26 days before the Incident";
            GM_Audio.PlayPiano(6);
        }
        if (scene.name == "L_D3")
        {

            DayText.text = "April 21th";
            DayNameText.text = "Wishing for a Golden Era";
            IncidentText.text = "24 days before the Incident";
            GM_Audio.PlayPiano(7);
        }
        if (scene.name == "L_E1")
        {
            DayText.text = "April 26th";
            DayNameText.text = "The Right Words";
            IncidentText.text = "19 days before the Incident";
            GM_Audio.PlayPiano(8);
        }
        if (scene.name == "L_E2")
        {
            DayText.text = "April 25th";
            DayNameText.text = "Disillusions & Sorrow";
            IncidentText.text = "20 days before the Incident";
            GM_Audio.PlayPiano(9);
        }
        if (scene.name == "L_E3")
        {
            DayText.text = "April 22th";
            DayNameText.text = "God's Name";
            IncidentText.text = "23 days before the Incident";
            GM_Audio.PlayPiano(10);
        }
        if (scene.name == "L_F1")
        {
            DayText.text = "May 01st";
            DayNameText.text = "Peer Pressure";
            IncidentText.text = "14 days before the Incident";
            GM_Audio.PlayPiano(11);
        }
        if (scene.name == "L_F2")
        {
            DayText.text = "May 02nd";
            DayNameText.text = "Conflicts";
            IncidentText.text = "13 days before the Incident";
            GM_Audio.PlayPiano(12);
        }
        if (scene.name == "L_F3")
        {
            DayText.text = "May 03rd";
            DayNameText.text = "The Free Will";
            IncidentText.text = "12 days before the Incident";
            GM_Audio.PlayPiano(13);
        }
        if (scene.name == "L_F4")
        {
            DayText.text = "May 04th";
            DayNameText.text = "Glared at";
            IncidentText.text = "11 days before the Incident";
            GM_Audio.PlayPiano(14);
        }
    }

}
