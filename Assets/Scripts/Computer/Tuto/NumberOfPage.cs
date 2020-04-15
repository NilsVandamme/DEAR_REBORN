using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfPage : MonoBehaviour
{
    public PassText BulleScript;

    void Update()
    {
        GetComponent<Text>().text = (BulleScript.TextTuto.Length).ToString();
    }
}
