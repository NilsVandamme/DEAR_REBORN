using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualPage : MonoBehaviour
{
    public PassText BulleScript;

    void Update()
    {
        GetComponent<Text>().text = BulleScript.NumberText.ToString();
    }
}
