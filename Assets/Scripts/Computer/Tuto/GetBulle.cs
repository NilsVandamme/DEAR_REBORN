using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBulle : MonoBehaviour
{
    public PassText BulleScript;

    public void GenerateNextText()
    {
        BulleScript.NextText();
    }

    public void GeneratePrevText()
    {
        BulleScript.PrevText();
    }
}
