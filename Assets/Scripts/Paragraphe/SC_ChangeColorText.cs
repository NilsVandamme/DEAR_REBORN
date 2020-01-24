using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ChangeColorText 
{
    public string start, end;
    public Color color;
    public float lerp;

    public SC_ChangeColorText (string start, string end, float lerp, Color color)
    {
        this.start = start;
        this.end = end;
        this.color = color;
        this.lerp = lerp;
    }


}
