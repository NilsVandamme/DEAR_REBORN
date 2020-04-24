using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualPage : MonoBehaviour
{
    public PassText BulleScript;
    private int Number;

    void Update()
    {
        Number = Mathf.Clamp(Number, 0, BulleScript.TextTuto.Length - 1);
        Number = (BulleScript.NumberText + 1);
        GetComponent<Text>().text = Number.ToString();
    }
}
