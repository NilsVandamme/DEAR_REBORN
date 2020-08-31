using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ArrowLongMail : MonoBehaviour
{
    public GameObject ArrowUp, ArrowDown;
    private Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
       // Scrollbar scrollbar = gameObject.GetComponent<Scrollbar>();

        if (scrollbar.value >= 0.99f)
        {
            ArrowUp.SetActive(true);
        }
        if (scrollbar.value <= 0.01f)
        {
            ArrowDown.SetActive(true);
        }
        else
        {
            ArrowUp.SetActive(false);
            ArrowDown.SetActive(false);
        }
    }
}
