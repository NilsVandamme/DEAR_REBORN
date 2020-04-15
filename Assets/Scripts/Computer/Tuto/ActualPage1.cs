using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualPage1 : MonoBehaviour
{
    public PassText1 BulleScript;

    void Update()
    {
        GetComponent<Text>().text = BulleScript.NumberText.ToString();
    }
}
