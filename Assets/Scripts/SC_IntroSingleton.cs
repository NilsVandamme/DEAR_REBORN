using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_IntroSingleton : MonoBehaviour
{
    public static SC_IntroSingleton instance;

    private void Awake()
    {
        if (SC_IntroSingleton.instance == null)
            SC_IntroSingleton.instance = this;
        else
            Destroy(this.gameObject);
    }
}
