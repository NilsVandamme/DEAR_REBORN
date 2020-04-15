using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfPage1 : MonoBehaviour
{
    public PassText1 BulleScript;

    void Update()
    {
        GetComponent<Text>().text = (BulleScript.TextTuto.Length).ToString();
    }
}
