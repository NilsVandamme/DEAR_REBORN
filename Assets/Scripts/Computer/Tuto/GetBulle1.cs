using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBulle1 : MonoBehaviour
{
    public PassText1 BulleScript;

    public void GenerateNextText()
    {
        BulleScript.NextText();
    }

    public void GeneratePrevText()
    {
        BulleScript.PrevText();
    }
}
